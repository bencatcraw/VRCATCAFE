using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class DrinkMachine : NetworkBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    [SerializeField] private Collider col;
    public void EnableCollider()
    {
        col.enabled = true;
    }
    public void DisableCollider()
    {
        col.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponentInParent<CupKitchenObject>().GetKitchenObjectSO() == other.GetComponentInParent<CupKitchenObject>().defaultKitchenObjectSO)
        {
            other.GetComponentInParent<CupKitchenObject>().AddIngredienteServerRpc(CatCafeMultiplayer.Instance.GetKitchenObjectSOIndex(kitchenObjectSO));
        }
    }

    
}
