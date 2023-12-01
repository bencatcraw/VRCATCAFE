using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : KitchenObject
{
    public event EventHandler<OnSpreadAddedEventArgs> OnSpreadAdded;
    public KitchenObjectSO defaultKitchenObjectSO;
    public class OnSpreadAddedEventArgs : EventArgs
    {
        public KitchenObjectSO kitchenObjectSO;
    }
    private List<KitchenObjectSO> kitchenObjectSOList;
    public AudioSource spreadAudio;
    public AudioSource chopSound;
    private void Awake()
    {
        kitchenObjectSOList = new List<KitchenObjectSO>();
        SetKitchenObjectSO(defaultKitchenObjectSO);
    }
    public bool TryAddSpread(KitchenObjectSO kitchenObjectSO)
    {
            kitchenObjectSOList.Add(kitchenObjectSO);

            OnSpreadAdded?.Invoke(this, new OnSpreadAddedEventArgs
            {
                kitchenObjectSO = kitchenObjectSO
            });
            return true;

    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Spread" && GetKitchenObjectSO() == defaultKitchenObjectSO)
        {
            if (TryAddSpread(collision.gameObject.GetComponent<KitchenObject>().GetKitchenObjectSO()))
            {
                SetKitchenObjectSO(collision.gameObject.GetComponent<KitchenObject>().GetKitchenObjectSO());
                spreadAudio.Play();
                this.gameObject.tag = "Spread";
            }

        }
    }

    public List<KitchenObjectSO> GetKitchenObjectSOList()
    {
        return kitchenObjectSOList;
    }
}
