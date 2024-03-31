using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Chains : MonoBehaviour
{
    [SerializeField] private XRSocketInteractor _socketInteractor;
    [SerializeField] private SphereCollider _socketTrigger;

    private int _unlocked;

    // Start is called before the first frame update
    void Start()
    {
        _unlocked = 0;
        _socketInteractor.enabled = false;
        _socketTrigger.enabled = false;
    }

    public void Unlocked()
    {
        _unlocked++;

        if (_unlocked >= 2)
        {
            _socketInteractor.enabled = true;
            _socketTrigger.enabled = true;
        }   
    }
}