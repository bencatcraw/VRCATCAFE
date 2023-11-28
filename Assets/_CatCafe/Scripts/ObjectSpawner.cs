using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class ObjectSpawner : NetworkBehaviour
{
    public KitchenObjectSO kitchenObject;
    public void spawn()
    {
        CatCafeMultiplayer.Instance.SpawnKitchenObject(kitchenObject);
    }
}
