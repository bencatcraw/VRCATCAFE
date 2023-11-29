using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avocado : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO cutAvocado;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Knife")
        {
            Transform cutCado = Instantiate(cutAvocado.prefab);
            cutCado.transform.position = transform.position;
            Destroy(this.gameObject);
        }
    }
}
