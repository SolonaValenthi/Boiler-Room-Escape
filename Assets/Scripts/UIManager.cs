using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class UIManager : MonoBehaviour
{
    [SerializeField] InputActionReference _openMenu;
    [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject[] _menuInteractors;

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
    
    private bool _menuOpen = false;
    private TurningMode _currentProvider = TurningMode.Continuous;

    // Start is called before the first frame update
    void Start()
    {
        _menuOpen = false;
        _menuCanvas.SetActive(false);

        foreach (var interactor in _menuInteractors)
        {
            interactor.SetActive(false);
        }
    }

    private void OnEnable()
    {
        var openMenuAction = GetInputAction(_openMenu);

        if (openMenuAction != null)
            openMenuAction.performed += OpenMenu;
    }

    private void EnableMovement()
    {
        _continuousMovement.enabled = true;

        if (_currentProvider == TurningMode.Continuous)
            _continuousTurn.enabled = true;
        else
            _snapTurn.enabled = true;
    }

    private void DisableMovement()
    {
        _continuousMovement.enabled = false;
        _continuousTurn.enabled = false;
        _snapTurn.enabled = false;
    }

    private void OpenMenu(InputAction.CallbackContext context)
    {
        if (!_menuOpen)
        {
            DisplayMenu();
        }
    }

    private void DisplayMenu()
    {
        DisableMovement();
        _menuOpen = true;
        _menuCanvas.SetActive(true);
        _mainMenu.SetActive(true);

        foreach (var interactor in _menuInteractors)
        {
            interactor.SetActive(true);
        }
    }

    public void CloseMenu()
    {
        EnableMovement();
        _menuOpen = false;
        _menuCanvas.SetActive(false);

        foreach (var interactor in _menuInteractors)
        {
            interactor.SetActive(false);
        }
    }

    private void OnDisable()
    {
        var openMenuAction = GetInputAction(_openMenu);

        if (openMenuAction != null)
            openMenuAction.performed -= OpenMenu;
    }

    static InputAction GetInputAction(InputActionReference actionReference)
    {
#pragma warning disable IDE0031 // Use null propagation -- Do not use for UnityEngine.Object types
        return actionReference != null ? actionReference.action : null;
#pragma warning restore IDE0031
    }
}
