using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScenarioManager : MonoBehaviour
{
    private static ScenarioManager _instance;
    public static ScenarioManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("No scenario manager found");

            return _instance;
        }
    }

    [SerializeField] UnityEvent _allNailsRemoved;
    [SerializeField] UnityEvent _incorrectCombination;

    private int _nailsRemoved = 0;
    private bool _outageReady = true;

    private void Awake()
    {
        _instance = this;
        _nailsRemoved = 0;
    }

    public void RemoveNail()
    {
        _nailsRemoved++;

        if (_nailsRemoved >= 2)
            _allNailsRemoved.Invoke();
    }

    public void IncorrectCombo()
    {
        if (_outageReady)
        {
            _incorrectCombination.Invoke();
            _outageReady = false;
        }
    }
}
