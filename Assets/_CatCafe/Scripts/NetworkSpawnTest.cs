using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkSpawnTest : NetworkBehaviour
{
    private GameObject FindNearestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject nearestPlayer = null;
        float minDistance = Mathf.Infinity;

        foreach (GameObject player in players)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestPlayer = player;
            }
        }

        return nearestPlayer;
    }
    [ServerRpc(RequireOwnership = false)]
    public void changeownershipServerRpc()
    {
        GameObject player = FindNearestPlayer();

        this.GetComponent<NetworkObject>().ChangeOwnership(player.GetComponent<NetworkObject>().OwnerClientId);
        
    }

    [ServerRpc(RequireOwnership = false)]
    public void changeownershiptoserverServerRpc()
    {
        this.GetComponent<NetworkObject>().ChangeOwnership(0);

    }


}
