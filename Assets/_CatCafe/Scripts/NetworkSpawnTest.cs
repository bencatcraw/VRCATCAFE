using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkSpawnTest : NetworkBehaviour
{ 
    public void GetPlayerId()
    {
        changeownershipServerRpc(NetworkManager.LocalClientId);
    }
    [ServerRpc(RequireOwnership = false)]
    public void changeownershipServerRpc(ulong playerId)
    {
        this.GetComponent<NetworkObject>().ChangeOwnership(playerId);
    }

    [ServerRpc(RequireOwnership = false)]
    public void changeownershiptoserverServerRpc()
    {
        this.GetComponent<NetworkObject>().RemoveOwnership();

    }
}
