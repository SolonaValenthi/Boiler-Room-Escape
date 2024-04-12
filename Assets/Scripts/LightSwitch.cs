using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightSwitch : MonoBehaviour
{
    [SerializeField] private bool _active = false;
    [SerializeField] private bool _startOn = false;
    private Animator _switchAnim;
    private bool _switchOn = false;

    [SerializeField] UnityEvent _onFlip;

    // Start is called before the first frame update
    void Start()
    {
        _switchAnim = GetComponent<Animator>();

        if (_startOn)
            FlipSwitch();
    }

    public void FlipSwitch()
    {
        _switchOn = !_switchOn;
        _switchAnim.SetBool(_switchAnim.parameters[0].name, _switchOn);

        if (_switchOn && _active)
            _onFlip.Invoke();
    }

    public void Flipped()
    {
        
    }

    public void ActivateSwitch()
    {
        _active = true;
    }
}
