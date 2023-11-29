using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CupKitchenObject : KitchenObject
{
    public KitchenObjectSO defaultKitchenObjectSO;

    private void Start()
    {
        SetKitchenObjectSO(defaultKitchenObjectSO);
    }

    [ServerRpc(RequireOwnership = false)]
    public void AddIngredienteServerRpc(int kitchenObjectSOIndex)
    {
        AddIngredienteClientRpc(kitchenObjectSOIndex);
    }
    [ClientRpc]
    private void AddIngredienteClientRpc(int kitchenObjectSOIndex)
    {
        KitchenObjectSO kitchenObjectSO = CatCafeMultiplayer.Instance.GetKitchenObjectSOFromIndex(kitchenObjectSOIndex);

        SetKitchenObjectSO(kitchenObjectSO);
        this.GetComponentInChildren<MugCompleteVisual>().DrinkVisual(kitchenObjectSO);
    }

    [ServerRpc(RequireOwnership = false)]
    public void ClearDrinksServerRpc()
    {
        ClearDrinksClientRpc();
    }
    [ClientRpc]
    private void ClearDrinksClientRpc()
    {
        SetKitchenObjectSO(defaultKitchenObjectSO);
        this.GetComponentInChildren<MugCompleteVisual>().ClearDrinks();
    }
}


