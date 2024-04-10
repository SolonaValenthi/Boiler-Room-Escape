using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pliers : MonoBehaviour
{
    // Set right hand transform as element 0
    [SerializeField] private Transform[] _attachPoints;
    
    private GameObject _nail;
    private XRGrabInteractable _interactable;

    void Start()
    {
        _interactable = GetComponent<XRGrabInteractable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Nail"))
            Register(other.gameObject);
        else if (other.CompareTag("Right Control"))
            SetAttach(true);
        else if (other.CompareTag("Left Control"))
            SetAttach(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Nail"))
            Unregister();
    }

    private void SetAttach(bool rightControl)
    {
        if (rightControl)
            _interactable.attachTransform = _attachPoints[0];
        else
            _interactable.attachTransform = _attachPoints[1];
    }

    private void Register(GameObject nail)
    {
        _nail = nail;
    }

    private void Unregister()
    {
        _nail = null;
    }

    public void PullNail()
    {
        if (_nail != null)
        {
            Destroy(_nail);
            ScenarioManager.Instance.RemoveNail();
            _nail = null;
        }
    }
}
