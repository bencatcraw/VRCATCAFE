using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

namespace UnityEngine.XR.Content.Interaction
{
    public class XRInstantiateGrabbableObject : XRBaseInteractable
    {
        [SerializeField]
        private GameObject grabbableObject;

        [SerializeField]
        private Transform transformToInstantiate;

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {

            // Instantiate object
            GameObject newObject = Instantiate(grabbableObject, transformToInstantiate.position, Quaternion.identity);
            if (newObject != null && GetComponentInParent<Toaster>() != null)
            {
                GetComponentInParent<Toaster>().TakeToast();
            }
            // Get grab interactable from prefab
            XRGrabInteractable objectInteractable = newObject.GetComponent<XRGrabInteractable>();

            // Select object into same interactor
            interactionManager.SelectEnter(args.interactorObject, objectInteractable);

            base.OnSelectEntered(args);
        }
    } }
