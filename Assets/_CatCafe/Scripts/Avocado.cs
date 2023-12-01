using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avocado : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO cutAvocado;
    private bool cut = false;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Knife" && !cut)
        {
            cut = true;
            Transform cutCado = Instantiate(cutAvocado.prefab);
            cutCado.transform.position = transform.position;
            collision.gameObject.GetComponent<Knife>().chopSound.Play();
            Destroy(this.gameObject);
        }
    }
}
