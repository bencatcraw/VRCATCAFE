using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;

public class CatCafeMultiplayer : NetworkBehaviour
{
    public static CatCafeMultiplayer Instance { get; private set; }

    [Serializable] public struct locationObjects {
        public KitchenObjectSO kitchenObjectSO;
        public Transform transform;
    }
    [SerializeField] private KitchenObjectListSO kitchenObjectListSO;
    [SerializeField] private List<locationObjects> locationList;

    private void Awake()
    {
        Instance = this; 
    }

    public void SpawnKitchenObject(KitchenObjectSO kitchenObjectSO)
    {
        SpawnKitchenObjectServerRpc(GetKitchenObjectSOIndex(kitchenObjectSO));
    }

    [ServerRpc(RequireOwnership = false)]
    private void SpawnKitchenObjectServerRpc(int kitchenObjectSOIndex)
    {
        KitchenObjectSO kitchenObjectSO = GetKitchenObjectSOFromIndex(kitchenObjectSOIndex);

        for(int i = 0; i < locationList.Count; i++)
        {
            if(kitchenObjectSO == locationList[i].kitchenObjectSO)
            {
                Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab, locationList[i].transform);

                NetworkObject kitchenObjectNetworkObject = kitchenObjectTransform.GetComponent<NetworkObject>();
                kitchenObjectNetworkObject.Spawn(true);
            }
        }
        
 
    }

    public int GetKitchenObjectSOIndex(KitchenObjectSO kitchenObjectSO)
    {
        return kitchenObjectListSO.kitchenObjectSOList.IndexOf(kitchenObjectSO);
    }

    public KitchenObjectSO GetKitchenObjectSOFromIndex(int kitchenObjectSOIndex)
    {
        return kitchenObjectListSO.kitchenObjectSOList[kitchenObjectSOIndex];
    }
}
   

