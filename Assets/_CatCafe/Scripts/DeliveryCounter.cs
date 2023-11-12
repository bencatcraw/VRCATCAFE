using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Plate")
        {
            DeliveryManager.Instance.DeliverRecipe(collision.gameObject.GetComponent<PlateKitchenObject>());
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Cup")
        {
            Destroy(collision.gameObject);
        }
    }
}
