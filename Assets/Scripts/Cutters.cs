using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cutters : MonoBehaviour
{
    [SerializeField] UnityEvent _chainsCut;

    private GameObject _chains;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Chains"))
            _chains = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Chains"))
            _chains = null;
    }

    public void CutChains()
    {
        if (_chains != null)
        {
            Destroy(_chains);
            _chainsCut?.Invoke();
        }
    }
}
