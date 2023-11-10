using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GameManager : NetworkBehaviour
{
    public NetworkVariable<int> playerCount = new();
    public GameObject codeCanvas;
    public override void OnNetworkSpawn()
    {
        if (IsHost)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnectCallback;
        }
    }

    private void OnClientDisconnectCallback(ulong obj)
    {
        playerCount.Value--;
        if(playerCount.Value < 2)
        {
            codeCanvas.SetActive(true);
        }
        
    }

    private void OnClientConnectedCallback(ulong obj)
    {
        playerCount.Value++;
        if (playerCount.Value == 2)
        {
            codeCanvas.SetActive(false);
        }
    }
}

