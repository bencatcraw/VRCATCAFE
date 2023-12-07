using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.Netcode.Transports.UTP;
using TMPro;

public class NetworkConnection : MonoBehaviour
{
    public string joinCode;
    public int maxConnection = 2;
    public UnityTransport transport;
    [SerializeField]
    public TextMeshProUGUI codeText;
    [SerializeField]
    public TMP_InputField codeInput;

    private async void Awake()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }
    public async void Host()
    {
        Allocation allocation = await RelayService.Instance.CreateAllocationAsync(maxConnection);
        string newJoinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);

        codeText.SetText(newJoinCode);

        transport.SetHostRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port,
            allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData);

        NetworkManager.Singleton.StartHost();
    }

    public async void Client()
    {
        joinCode = codeInput.text.ToUpper();
        JoinAllocation allocation = await RelayService.Instance.JoinAllocationAsync(joinCode);

        transport.SetClientRelayData(allocation.RelayServer.IpV4, (ushort)allocation.RelayServer.Port,
            allocation.AllocationIdBytes, allocation.Key, allocation.ConnectionData, allocation.HostConnectionData);

        NetworkManager.Singleton.StartClient();
    }
    public void startSingleplayer() { StartCoroutine(Singleplayer()); }
    IEnumerator Singleplayer()
    {
        Host();
        yield return new WaitForSeconds(3f);
        NetworkManager.Singleton.SceneManager.LoadScene("Singleplayer", LoadSceneMode.Single);
    }
}
