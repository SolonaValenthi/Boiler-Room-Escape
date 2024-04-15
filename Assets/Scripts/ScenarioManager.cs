using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

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

    [SerializeField] private Transform _chair;

    [Space]
    [Header("Movement Providers")]

    [SerializeField] ActionBasedContinuousTurnProvider _continuousTurn;
    [SerializeField] ActionBasedSnapTurnProvider _snapTurn;
    [SerializeField] ActionBasedContinuousMoveProvider _continuousMovement;

    private enum TurningMode
    {
        Continuous,
        Snap
    }

    private int _nailsRemoved = 0;
    private bool _outageReady = true;
    private TurningMode _currentProvider = TurningMode.Continuous;

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
            SetChair();
            _outageReady = false;
        }
    }

    public void EnableMovement()
    {
        _continuousMovement.enabled = true;

        if (_currentProvider == TurningMode.Continuous)
            _continuousTurn.enabled = true;
        else
            _snapTurn.enabled = true;
    }

    public void DisableMovement()
    {
        _continuousMovement.enabled = false;
        _continuousTurn.enabled = false;
        _snapTurn.enabled = false;
    }

    private void SetChair()
    {
        _chair.position = new Vector3(-0.055f, -0.009f, 1.633f);
        _chair.eulerAngles = new Vector3(-0.047f, 180, 0);
    }
}
