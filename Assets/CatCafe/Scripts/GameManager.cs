using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class GameManager : NetworkBehaviour
{
    public static GameManager Instance;
    public NetworkVariable<int> playerCount = new();
    public GameObject codeCanvas;

    private State state;
    private float waitingToStartTimer = 1f;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer = 10f;

    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver,
    }
    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if (waitingToStartTimer < 0f)
                {
                    state = State.CountdownToStart;
                }
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                }
                break;
            case State.GameOver:
                break;
        }
        Debug.Log(state);
    }
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

