using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine.XR.Content.Interaction
{
    public class Toaster : MonoBehaviour
    {
        private enum State
        {
            Idle,
            Toasting,
            Toasted,
        }

        [SerializeField]
        private XRSlider slider;
        [SerializeField]
        private GameObject grabToast;
        private float toastingTimer;
        private float filledSlots;
        private State state;
        public ToasterRecipeSO toastRecipeSO;
        [SerializeField] private AudioSource toastDown;
        [SerializeField] private AudioSource toastUp;
        [SerializeField]
        private List<GameObject> breadList;
        [SerializeField]
        private List<GameObject> toastList;
        private void Update()
        {
            switch (state)
            {
                case State.Idle:
                    if (slider.value < 0.95 && slider.value > 0.1)
                    {
                        slider.value = Mathf.Lerp(slider.value, 1f, 0.1f);
                    }
                    else if (slider.value < 0.1 && filledSlots > 0)
                    {
                        state = State.Toasting;
                        toastDown.Play();
                    }
                    else if (slider.value < 0.95) {
                        slider.value = Mathf.Lerp(slider.value, 1f, 0.1f);
                    }
                    break;
                case State.Toasting:
                    toastingTimer += Time.deltaTime;
                    if (toastingTimer > toastRecipeSO.toastingTimeMax)
                    {
                        state = State.Toasted;
                        toastUp.Play();
                        if (filledSlots >= 1)
                        {
                            breadList[0].gameObject.SetActive(false);
                            toastList[0].gameObject.SetActive(true);
                            if (filledSlots == 2)
                            {
                                breadList[1].gameObject.SetActive(false);
                                toastList[1].gameObject.SetActive(true);
                            }
                        }
                        toastingTimer = 0;
                    }
                    break;
                case State.Toasted:
                    slider.value = Mathf.Lerp(slider.value, 1f, 0.5f);
                    grabToast.SetActive(true);
                    if (filledSlots <= 0)
                    {
                        filledSlots = 0;
                        state = State.Idle;
                        grabToast.SetActive(false); 
                    }
                    break;
            }
        }
        public void TakeToast()
        {
            if (state == State.Toasted)
            {
                if (filledSlots == 2)
                {
                    toastList[1].gameObject.SetActive(false);
                }
                if(filledSlots == 1)
                {
                    toastList[0].gameObject.SetActive(false);
                }
                filledSlots--;
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<KitchenObject>() != null && filledSlots < 2 && state == State.Idle)
            {
                if (toastRecipeSO.input == collision.gameObject.GetComponent<KitchenObject>().GetKitchenObjectSO())
                {
                    filledSlots++;
                    if (filledSlots >= 1)
                    {
                        breadList[0].gameObject.SetActive(true);
                        if (filledSlots == 2)
                        {
                            breadList[1].gameObject.SetActive(true);
                        }
                    }
                    Destroy(collision.gameObject);

                }
            }
        }

    }
}
    

