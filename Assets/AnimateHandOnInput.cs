using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    [SerializeField] InputActionProperty _gripActionProperty;
    [SerializeField] InputActionProperty _triggerActionProperty;
    [SerializeField] Animator _handAnimator;
    
    // Update is called once per frame
    void Update()
    {
        float triggerValue = _triggerActionProperty.action.ReadValue<float>();
        float gripValue = _gripActionProperty.action.ReadValue<float>();
        _handAnimator.SetFloat("Trigger", triggerValue);
        _handAnimator.SetFloat("Grip", gripValue);
    }
}
