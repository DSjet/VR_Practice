using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon;
using Unity.XR.CoreUtils;

public class NetworkPlayer : PunBehaviour
{
    [SerializeField] Transform head;
    [SerializeField] Transform leftHand;
    [SerializeField] Transform rightHand;

    [SerializeField] Animator rightHandAnimator;
    [SerializeField] Animator leftHandAnimator;

    private Transform headOrigin;   
    private Transform leftHandOrigin;
    private Transform rightHandOrigin;

    private PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        XROrigin xrOrigin = FindObjectOfType<XROrigin>();
        headOrigin = xrOrigin.transform.Find("Camera Offset/Main Camera");
        leftHandOrigin = xrOrigin.transform.Find("Camera Offset/LeftHand Controller");
        rightHandOrigin = xrOrigin.transform.Find("Camera Offset/RightHand Controller");


        // disable network player renderer if the game objects spawned
        foreach(var item in GetComponentsInChildren<Renderer>())
        {
            item.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            MapPosition(head, headOrigin);
            MapPosition(leftHand, leftHandOrigin);
            MapPosition(rightHand, rightHandOrigin);

            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.LeftHand), leftHandAnimator);
            UpdateHandAnimation(InputDevices.GetDeviceAtXRNode(XRNode.RightHand), rightHandAnimator);
        }

    }

    private void UpdateHandAnimation(InputDevice targetDevice, Animator handAnimator)
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }
        
        if(targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }

    void MapPosition(Transform target, Transform originTransform)
    {
        target.position = originTransform.position;
        target.rotation = originTransform.rotation;
    }
}
