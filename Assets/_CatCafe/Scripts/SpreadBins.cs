using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadBins : MonoBehaviour
{
    [SerializeField]
    private Collider jamSpread;
    [SerializeField]
    private Collider peanutButterSpread;
    // Start is called before the first frame update

    private void OnCollisionEnter(Collision collision)
    {
        Collider myCollider = collision.GetContact(0).thisCollider;

        if(myCollider == jamSpread)
        {
            Debug.Log("jam");
        }
        if (myCollider == peanutButterSpread)
        {
            Debug.Log("oeabnut");
        }
    }
}

