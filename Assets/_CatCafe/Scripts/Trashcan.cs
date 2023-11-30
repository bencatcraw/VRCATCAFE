using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Trashcan : NetworkBehaviour
{
    [SerializeField] private KitchenObjectSO[] trashableObjects;
    private void OnCollisionEnter(Collision collision)
    {
        foreach(KitchenObjectSO s in trashableObjects)
        {
            if (collision.gameObject.GetComponent<KitchenObject>().GetKitchenObjectSO() == s)
            {
                collision.gameObject.GetComponent<NetworkObject>().Despawn();
                Destroy(collision.gameObject);
            }
        }
        
    }

    [ServerRpc(RequireOwnership = false)]
    public void TrashObjectServerRpc(ulong id)
    {
        TrashObjectClientRpc(id);
    }
    [ClientRpc]
    public void TrashObjectClientRpc(ulong id)
    {
        
    }
}
