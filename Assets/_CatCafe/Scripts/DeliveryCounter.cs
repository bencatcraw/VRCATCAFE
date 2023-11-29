using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : MonoBehaviour
{
    [SerializeField] private Collider col;
    [SerializeField] private Transform dishReturn;

    public void EnableCollider()
    {
        col.enabled = true;
    }
    public void DisableCollider()
    {
        col.enabled = false;
    }
    private void OnTriggerStay(Collider collision)
    {   
        if (collision.gameObject.tag == "Plate")
        {
            DeliveryManager.Instance.DeliverPlate(collision.gameObject.GetComponent<PlateKitchenObject>());
            collision.gameObject.transform.position = dishReturn.position;
        }
        if (collision.gameObject.tag == "Cup")
        {
            DeliveryManager.Instance.DeliverCup(collision.gameObject.GetComponent<CupKitchenObject>());
            collision.gameObject.transform.position = dishReturn.position;
        }
    }

    public void buttontest()
    {
        Debug.Log("Pressed");
    }
}
