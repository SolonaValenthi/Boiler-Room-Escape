using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightSwitch : MonoBehaviour
{
    private Animator _switchAnim;
    private bool _switchOn = false;
    private int _ID;

    [SerializeField] UnityEvent _onFlip;

    // Start is called before the first frame update
    void Start()
    {
        _switchAnim = GetComponent<Animator>();
        _ID = Animator.StringToHash(_switchAnim.parameters[0].name);
    }

    public void FlipSwitch()
    {
        _switchOn = !_switchOn;
        _switchAnim.SetBool(_switchAnim.parameters[0].name, _switchOn);

        if (_switchOn)
            _onFlip.Invoke();
    }

    public void Flipped()
    {
        
    }
}
