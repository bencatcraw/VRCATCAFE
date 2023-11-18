using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : MonoBehaviour
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
    private void OnTriggerStay(Collider collision)
    {   
        if (collision.gameObject.tag == "Plate")
        {
            DeliveryManager.Instance.DeliverPlate(collision.gameObject.GetComponent<PlateKitchenObject>());
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Cup")
        {
            DeliveryManager.Instance.DeliverCup(collision.gameObject.GetComponent<CupKitchenObject>());
            Destroy(collision.gameObject);
        }
    }
}
