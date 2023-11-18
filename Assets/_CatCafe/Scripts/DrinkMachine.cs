using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkMachine : MonoBehaviour
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
            other.GetComponentInParent<CupKitchenObject>().SetKitchenObjectSO(kitchenObjectSO);
            other.gameObject.GetComponentInChildren<MugCompleteVisual>().DrinkVisual(kitchenObjectSO);
            
        }
    }
}
