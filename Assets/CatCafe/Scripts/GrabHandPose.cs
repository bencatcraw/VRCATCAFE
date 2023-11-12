using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

#if UNITY_EDITOR
using UnityEditor;
#endif
namespace UnityEngine.XR.Content.Interaction
{
    public class GrabHandPose : MonoBehaviour
    {
        public float posTransitionDuration = 0.2f;
        public HandData rightHandPose;
        public HandData leftHandPose;

        private Vector3 startingHandPosition;
        private Vector3 finalHandPosition;
        private Quaternion startingHandRotation;
        private Quaternion finalHandRotation;

        private Quaternion[] startingFingerRotation;
        private Quaternion[] finalFingerRotation;
        // Start is called before the first frame update
        void Start()
        {
            if (GetComponent<XRGrabInteractable>() != null)
            {
                XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
                grabInteractable.selectEntered.AddListener(SetupPose);
                grabInteractable.selectExited.AddListener(UnSetPose);
            }
            else if (GetComponentInParent<XRSlider>() != null)
            {
                XRSlider grabInteractable = GetComponentInParent<XRSlider>();
                grabInteractable.selectEntered.AddListener(SetupPose);
                grabInteractable.selectExited.AddListener(UnSetPose);
            }
            

            rightHandPose.gameObject.SetActive(false);
            leftHandPose.gameObject.SetActive(false);
        }

        public void SetupPose(BaseInteractionEventArgs arg)
        {
            if (arg.interactorObject is XRDirectInteractor)
            {
                HandData handData = arg.interactorObject.transform.parent.GetComponentInChildren<HandData>();
                handData.animator.enabled = false;

                if (handData.handType == HandData.HandModelType.Right)
                {
                    SetHandDataValues(handData, rightHandPose);
                }
                else
                {
                    SetHandDataValues(handData, leftHandPose);
                }


                SetHandData(handData, finalHandPosition, finalHandRotation, finalFingerRotation);
                //StartCoroutine(SetHandDataRoutine(handData, finalHandPosition, finalHandRotation, finalFingerRotation, startingHandPosition, startingHandRotation, startingFingerRotation));
            }
        }

        public void UnSetPose(BaseInteractionEventArgs arg)
        {
            if (arg.interactorObject is XRDirectInteractor)
            {
                HandData handData = arg.interactorObject.transform.parent.GetComponentInChildren<HandData>();
                handData.animator.enabled = true;

                SetHandData(handData, startingHandPosition, startingHandRotation, startingFingerRotation);
                //StartCoroutine(SetHandDataRoutine(handData, startingHandPosition, startingHandRotation, startingFingerRotation, finalHandPosition, finalHandRotation, finalFingerRotation));
            }
        }

        public void SetHandDataValues(HandData h1, HandData h2)
        {
            startingHandPosition = new Vector3(h1.root.localPosition.x / h1.root.localScale.x, h1.root.localPosition.y / h1.root.localScale.y, h1.root.localPosition.z / h1.root.localScale.z);
            finalHandPosition = new Vector3(h2.root.localPosition.x / h2.root.localScale.x, h2.root.localPosition.y / h2.root.localScale.y, h2.root.localPosition.z / h2.root.localScale.z); ;

            startingHandRotation = h1.root.localRotation;
            finalHandRotation = h2.root.localRotation;

            startingFingerRotation = new Quaternion[h1.fingerBones.Length];
            finalFingerRotation = new Quaternion[h1.fingerBones.Length];

            for (int i = 0; i < h1.fingerBones.Length; i++)
            {
                startingFingerRotation[i] = h1.fingerBones[i].localRotation;
                finalFingerRotation[i] = h2.fingerBones[i].localRotation;
            }
        }

        public void SetHandData(HandData h, Vector3 newPostion, Quaternion newRotation, Quaternion[] newBonesRotation)
        {
            h.root.localPosition = newPostion;
            h.root.localRotation = newRotation;

            for (int i = 0; i < newBonesRotation.Length; i++)
            {
                h.fingerBones[i].localRotation = newBonesRotation[i];
            }
        }

        public IEnumerator SetHandDataRoutine(HandData h, Vector3 newPostion, Quaternion newRotation, Quaternion[] newBonesRotation, Vector3 startingPostion, Quaternion startingRotation, Quaternion[] startingBonesRotation)
        {
            float timer = 0;

            while (timer < posTransitionDuration)
            {
                Vector3 p = Vector3.Lerp(startingPostion, newPostion, timer / posTransitionDuration);
                Quaternion r = Quaternion.Lerp(startingRotation, newRotation, timer / posTransitionDuration);

                h.root.localPosition = p;
                h.root.localRotation = r;

                for (int i = 0; i < newBonesRotation.Length; i++)
                {
                    h.fingerBones[i].localRotation = Quaternion.Lerp(startingBonesRotation[i], newBonesRotation[i], timer / posTransitionDuration);
                }

                timer += Time.deltaTime;
                yield return null;
            }
        }

#if UNITY_EDITOR
        [MenuItem("Tools/Mirror Selected Right Grab Pose")]
        public static void MirrorRightPose()
        {
            GrabHandPose handPose = Selection.activeGameObject.GetComponent<GrabHandPose>();
            handPose.MirrorPose(handPose.leftHandPose, handPose.rightHandPose);
        }
        [MenuItem("Tools/Mirror Selected Left Grab Pose")]
        public static void MirrorLeftPose()
        {
            GrabHandPose handPose = Selection.activeGameObject.GetComponent<GrabHandPose>();
            handPose.MirrorPose(handPose.rightHandPose, handPose.leftHandPose);
        }

#endif
        public void MirrorPose(HandData poseToMirror, HandData poseUsedToMirror)
        {
            Vector3 mirroredPosition = poseUsedToMirror.root.localPosition;
            mirroredPosition.x *= -1;

            Quaternion mirroredQuaternion = poseUsedToMirror.root.localRotation;
            mirroredQuaternion.y *= -1;
            mirroredQuaternion.z *= -1;

            poseToMirror.root.localPosition = mirroredPosition;
            poseToMirror.root.localRotation = mirroredQuaternion;

            for (int i = 0; i < poseUsedToMirror.fingerBones.Length; i++)
            {
                poseToMirror.fingerBones[i].localRotation = poseUsedToMirror.fingerBones[i].localRotation;
            }
        }
    }
}
