using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : MonoBehaviour
{
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
        if (other.gameObject.tag == "Plate")
        {
            other.GetComponent<PlateKitchenObject>().ClearIngredientsServerRpc();
        }
        else if(other.gameObject.tag == "Spread")
        {
            other.gameObject.GetComponent<Knife>().SetKitchenObjectSO(other.gameObject.GetComponent<Knife>().defaultKitchenObjectSO);
            other.gameObject.GetComponentInChildren<KnifeCompleteVisual>().ClearSpread();
            other.gameObject.tag = "Knife";
        }
    }
}
