using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupKitchenObject : KitchenObject
{
    public KitchenObjectSO defaultKitchenObjectSO;

    private void Start()
    {
        SetKitchenObjectSO(defaultKitchenObjectSO);
    }
}


