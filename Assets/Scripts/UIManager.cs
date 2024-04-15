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

    private bool _menuOpen = false;

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

    private void OpenMenu(InputAction.CallbackContext context)
    {
        if (!_menuOpen)
        {
            DisplayMenu();
        }
    }

    private void DisplayMenu()
    {
        ScenarioManager.Instance.DisableMovement();
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
        ScenarioManager.Instance.EnableMovement();
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
