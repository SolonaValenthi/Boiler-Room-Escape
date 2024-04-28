using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabAttachAdjust : MonoBehaviour
{
    // Set right hand transform as element 0
    [SerializeField] private Transform[] _attachPoints;

    private XRGrabInteractable _interactable;

    void Start()
    {
        _interactable = GetComponent<XRGrabInteractable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Right Control"))
            SetAttach(true);
        else if (other.CompareTag("Left Control"))
            SetAttach(false);
    }

    private void SetAttach(bool rightControl)
    {
        if (rightControl)
            _interactable.attachTransform = _attachPoints[0];
        else
            _interactable.attachTransform = _attachPoints[1];
    }
}
