using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.VisualScripting;
using TMPro;

public class DeliveryManager : NetworkBehaviour
{
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;
    [SerializeField] private KitchenObjectSO note;
    [SerializeField] private Transform noteSpawn;
    [SerializeField] private TextMeshProUGUI orderText;
    [SerializeField] private TextMeshProUGUI moneyText;
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer = 4f;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;
    private int ordersFulfilled = 0;
    private int money = 0;

    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }
    private void Update()
    {
        if (!IsServer)
        {
            return;
        }

        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (GameManager.Instance.IsGamePlaying() && waitingRecipeSOList.Count < waitingRecipesMax)
            {
                int waitingRecipeSOIndex = UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count);

                SpawnNewWaitingRecipeServerRpc(waitingRecipeSOIndex);
            }
        }
    }
    [ServerRpc]
    private void SpawnNewWaitingRecipeServerRpc(int waitingRecipeSOIndex)
    {
        Transform kitchenObjectTransform = Instantiate(note.prefab, noteSpawn);

        NetworkObject kitchenObjectNetworkObject = kitchenObjectTransform.GetComponent<NetworkObject>();
        kitchenObjectNetworkObject.Spawn(true);
        kitchenObjectNetworkObject.GetComponent<RecipeNote>().makeRecipeNoteClientRpc(waitingRecipeSOIndex);
        SpawnNewWaitingRecipeClientRpc(waitingRecipeSOIndex);
    }

    [ClientRpc]
    private void SpawnNewWaitingRecipeClientRpc(int waitingRecipeSOIndex)
    {
        RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[waitingRecipeSOIndex];
        Debug.Log(waitingRecipeSO.recipeName);
        waitingRecipeSOList.Add(waitingRecipeSO);
    }

    public void DeliverPlate(PlateKitchenObject plateKitchenObject)
    {
        for(int i=0; i< waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenObjSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                bool plateContentsMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjSOList)
                {
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        if(plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        plateContentsMatchesRecipe = false;
                    }
                }
                if (plateContentsMatchesRecipe)
                {
                    //player deliverd correct recipe
                    DeliverCorrectRecipeServerRpc(i);
                    plateKitchenObject.DirtyPlateServerRpc();
                    return;
                }
            }
            
        }

        // no matches found
        // incorrect recipe
        DeliverIncorrectRecipeServerRpc();
        plateKitchenObject.DirtyPlateServerRpc();
    }

    public void DeliverCup(CupKitchenObject cupKitchenObject)
    {
        for (int i = 0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            bool cupContentsMatchesRecipe = false;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjSOList)
                {
                        if (cupKitchenObject.GetKitchenObjectSO() == recipeKitchenObjectSO)
                        {
                            cupContentsMatchesRecipe = true;
                            break;
                        }
                }
                if (cupContentsMatchesRecipe)
                {
                //player deliverd correct recipe
                DeliverCorrectRecipeServerRpc(i);
                cupKitchenObject.ClearDrinksServerRpc();
                    return;
                }
            

        }

        // no matches found
        // incorrect recipe
        DeliverIncorrectRecipeServerRpc();
        cupKitchenObject.ClearDrinksServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void DeliverCorrectRecipeServerRpc(int waitingRecipeSoIndex)
    {
        DeliverCorrectRecipeClientRpc(waitingRecipeSoIndex);
    }

    [ClientRpc]
    private void DeliverCorrectRecipeClientRpc(int waitingRecipeSoIndex)
    {
        Debug.Log("CORRECT " + waitingRecipeSoIndex);
        waitingRecipeSOList.RemoveAt(waitingRecipeSoIndex);
        ordersFulfilled++;
        money += 5;
        updateStats();

    }

    [ServerRpc(RequireOwnership = false)]
    private void DeliverIncorrectRecipeServerRpc()
    {
        DeliverIncorrectRecipeClientRpc();
    }

    [ClientRpc]
    private void DeliverIncorrectRecipeClientRpc()
    {
        Debug.Log("Incorrect ");
        money -= 10;
        updateStats();
    }

    private void updateStats()
    {
        orderText.text = "Orders Fulfilled: " + ordersFulfilled.ToString();
        moneyText.text = "Money: $" + money.ToString();
    }
}

