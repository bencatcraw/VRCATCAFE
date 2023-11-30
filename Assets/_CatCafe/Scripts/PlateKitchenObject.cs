using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }
    private List<KitchenObjectSO> kitchenObjectSOList;
    [SerializeField] private KitchenObjectSO dirtyPlateSO;
    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (kitchenObjectSOList.Contains(kitchenObjectSO)){
            return false;
        }
        else
        {
            AddIngredienteServerRpc(CatCafeMultiplayer.Instance.GetKitchenObjectSOIndex(kitchenObjectSO));
            return true;
        }
        
    }
    [ServerRpc(RequireOwnership = false)]
    private void AddIngredienteServerRpc(int kitchenObjectSOIndex)
    {
        AddIngredienteClientRpc(kitchenObjectSOIndex);
    }
    [ClientRpc]
    private void AddIngredienteClientRpc(int kitchenObjectSOIndex)
    {
        KitchenObjectSO kitchenObjectSO = CatCafeMultiplayer.Instance.GetKitchenObjectSOFromIndex(kitchenObjectSOIndex);

        kitchenObjectSOList.Add(kitchenObjectSO);

        OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
        {
            kitchenObjectSO = kitchenObjectSO
        });

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ingredient")
        {
            if (TryAddIngredient(collision.gameObject.GetComponent<KitchenObject>().GetKitchenObjectSO()))
            {
                Destroy(collision.gameObject);
            }

        }
        if (collision.gameObject.tag == "Spread")
        {
            if (TryAddIngredient(collision.gameObject.GetComponent<KitchenObject>().GetKitchenObjectSO()))
            {
                collision.gameObject.GetComponent<Knife>().SetKitchenObjectSO(collision.gameObject.GetComponent<Knife>().defaultKitchenObjectSO);
                collision.gameObject.GetComponentInChildren<KnifeCompleteVisual>().ClearSpread();
                collision.gameObject.GetComponent<AudioSource>().Play();
                collision.gameObject.tag = "Knife";
            }

        }
    }
    [ServerRpc(RequireOwnership = false)]
    public void ClearIngredientsServerRpc()
    {
        ClearIngredientsClientRpc();
    }
    [ClientRpc]
    private void ClearIngredientsClientRpc()
    {
        kitchenObjectSOList.Clear();
        GetComponentInChildren<PlateCompleteVisual>().ClearPlateVisual();
    }
    [ServerRpc(RequireOwnership = false)]
    public void DirtyPlateServerRpc()
    {
        DirtyPlateClientRpc();
    }
    [ClientRpc]
    private void DirtyPlateClientRpc()
    {
        ClearIngredientsClientRpc();
        TryAddIngredient(dirtyPlateSO);
    }
    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
