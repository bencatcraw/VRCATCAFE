using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Linq;

public class ObjectSpawner : NetworkBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObject;
    [SerializeField] private string objTag;
    [SerializeField] private int maxOfTagged;
    public void spawnKitchenObj()
    {
        CatCafeMultiplayer.Instance.SpawnKitchenObject(kitchenObject);
    }

    public void spawnPlateOrCup()
    {
        if (GameObject.FindGameObjectsWithTag(objTag).Length < maxOfTagged)
        {
            CatCafeMultiplayer.Instance.SpawnKitchenObject(kitchenObject);
        }
    }
}
