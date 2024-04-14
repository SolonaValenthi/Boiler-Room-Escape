using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chains : MonoBehaviour
{
    [SerializeField] private SphereCollider _triggerVolume;

    private int _unlocked;

    // Start is called before the first frame update
    void Start()
    {
        _unlocked = 0;
        _triggerVolume.enabled = false;
    }

    public void Unlocked()
    {
        _unlocked++;

        if (_unlocked >= 2)
        {
            _triggerVolume.enabled = true;
        }   
    }
}