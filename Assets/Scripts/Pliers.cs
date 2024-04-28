using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Pliers : MonoBehaviour
{   
    private GameObject _nail;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Nail"))
            Register(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Nail"))
            Unregister();
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
