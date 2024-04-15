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

    public enum TurningMode
    {
        Continuous,
        Snap
    }

    private int _nailsRemoved = 0;
    private bool _outageReady = true;
    private float _contTurnSpeed = 60;
    private int _snapTurnAmount = 45;
    public TurningMode _currentProvider { get; private set; } = TurningMode.Continuous;

    private void Awake()
    {
        _instance = this;
        _nailsRemoved = 0;
    }

    void Start()
    {
        EnableMovement();
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
        {
            _continuousTurn.turnSpeed = _contTurnSpeed;
            _snapTurn.turnAmount = 0;
        }
        else
        {
            _continuousTurn.turnSpeed = 0;
            _snapTurn.turnAmount = _snapTurnAmount;
        }
    }

    public void DisableMovement()
    {
        _continuousMovement.enabled = false;
        _continuousTurn.turnSpeed = 0;
        _snapTurn.turnAmount = 0;
    }

    public void SetTurnMode()
    {
        if (_currentProvider == TurningMode.Continuous)
            _currentProvider = TurningMode.Snap;
        else
            _currentProvider = TurningMode.Continuous;

        UIManager.Instance.SwitchTurnMode();
    }

    public void SetTurnSpeed(float speed)
    {
        _contTurnSpeed = speed;
    }

    public void SetTurnAmount(int degrees)
    {
        _snapTurnAmount = degrees;
    }

    private void SetChair()
    {
        _chair.position = new Vector3(-0.055f, -0.009f, 1.633f);
        _chair.eulerAngles = new Vector3(-0.047f, 180, 0);
    }
}
