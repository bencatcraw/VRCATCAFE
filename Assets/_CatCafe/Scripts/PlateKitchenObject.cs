using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }
    private List<KitchenObjectSO> kitchenObjectSOList;
    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
    {
        if (kitchenObjectSOList.Contains(kitchenObjectSO)){
            return false;
        }
        else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);

            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs
            {
                kitchenObjectSO = kitchenObjectSO
            });
            return true;
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ingredient")
        {
            if (TryAddIngredient(collision.gameObject.GetComponent<KitchenObject>().GetKitchenObjectSO()))
            {
                Destroy(collision.gameObject);
            }

        }
        if (collision.gameObject.tag == "Spread")
        {
            if (TryAddIngredient(collision.gameObject.GetComponent<KitchenObject>().GetKitchenObjectSO()))
            {
                collision.gameObject.GetComponent<Knife>().SetKitchenObjectSO(collision.gameObject.GetComponent<Knife>().defaultKitchenObjectSO);
                collision.gameObject.GetComponentInChildren<KnifeCompleteVisual>().ClearSpread();
                collision.gameObject.tag = "Untagged";
            }

        }
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
