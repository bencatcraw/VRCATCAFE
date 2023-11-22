using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class KitchenObject : NetworkBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }
    public static KitchenObject SpawnKitchenObject(KitchenObjectSO kitchenObjectSO)
    {
        Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);

        KitchenObject kitchenObject = kitchenObjectTransform.GetComponent<KitchenObject>();

        return kitchenObject;
    }
    public void SetKitchenObjectSO(KitchenObjectSO objectSO)
    {
        kitchenObjectSO = objectSO;
    }
}
