using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class RecipeNote : NetworkBehaviour
{
    [SerializeField] private Image recipeImage;
    [SerializeField] private TextMeshProUGUI recipeText;
    [SerializeField] private RecipeListSO recipeListSO;

    [ClientRpc]
    public void makeRecipeNoteClientRpc(int waitingRecipeSOIndex)
    {
        RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[waitingRecipeSOIndex];
        recipeImage.sprite = waitingRecipeSO.recipeSOImage;
        recipeText.text = waitingRecipeSO.recipeName;
    }
}
