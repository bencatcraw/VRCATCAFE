using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Trashcan : NetworkBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<KitchenObject>() != null)
        {
                collision.gameObject.GetComponent<NetworkObject>().Despawn();
                Destroy(collision.gameObject);
            
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
