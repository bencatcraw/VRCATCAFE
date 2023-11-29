using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ReceiptHolder : MonoBehaviour
{
    [SerializeField] private Transform notePlace;
    [SerializeField] private float yInc = 0.4f;
    private float currInc = 0f;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Note")
        {
            collision.gameObject.GetComponent<XRGrabInteractable>().enabled = false;
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            collision.gameObject.transform.position = notePlace.position + new Vector3(0f, currInc, 0f);
            collision.gameObject.transform.rotation = Quaternion.Euler(0f, Random.Range(0f,360f), 0f);

            currInc += yInc;
        }
    }

    }
