using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MugCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectList;
    private void Start()
    {
        ClearDrinks();
    }

    public void DrinkVisual(KitchenObjectSO drinkSO)
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjectList)
        {
            if (kitchenObjectSOGameObject.kitchenObjectSO == drinkSO)
            {
                kitchenObjectSOGameObject.gameObject.SetActive(true);
            }
        }
    }

    public void ClearDrinks()
    {
        foreach (KitchenObjectSO_GameObject kitchenObjectSOGameObject in kitchenObjectSOGameObjectList)
        {
            kitchenObjectSOGameObject.gameObject.SetActive(false);
        }
    }
}
