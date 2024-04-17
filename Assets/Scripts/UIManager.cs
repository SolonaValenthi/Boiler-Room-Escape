using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("No scenario manager found");

            return _instance;
        }
    }

    [SerializeField] InputActionReference _openMenu;
    [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject[] _menuInteractors;
    [SerializeField] private GameObject _contTurnSettings;
    [SerializeField] private GameObject _snapTurnSettings;
    [SerializeField] private TMP_Text _turnMode;
    [SerializeField] private TMP_Text _contTurnSpeed;
    [SerializeField] private TMP_Text _moveSpeed;
    [SerializeField] private TMP_Text _teleportText;
    [SerializeField] private TMP_Text _teleButtonText;
    [SerializeField] private Slider _speedSlider;
    [SerializeField] private TMP_Dropdown _amountDropdown;
    [SerializeField] private Slider _moveSlider;

    private bool _menuOpen = false;
    
    private void Awake()
    {
        _instance = this;
    }

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

    public void SwitchTurnMode()
    {
        if (ScenarioManager.Instance._currentProvider == ScenarioManager.TurningMode.Continuous)
        {
            _turnMode.text = "Continuous";
            _contTurnSettings.SetActive(true);
            _snapTurnSettings.SetActive(false);
        }
        else
        {
            _turnMode.text = "Snap";
            _contTurnSettings.SetActive(false);
            _snapTurnSettings.SetActive(true);
        }
    }

    public void SetMoveSpeed()
    {
        _moveSpeed.text = $"{Mathf.Round(_moveSlider.value * 10) / 10 }";
        ScenarioManager.Instance.SetMoveSpeed(_moveSlider.value);
    }

    public void SetTurnSpeed()
    {
        _contTurnSpeed.text = $"{_speedSlider.value}";
        ScenarioManager.Instance.SetTurnSpeed(_speedSlider.value);
    }

    public void SetTurnAmount()
    {
        switch (_amountDropdown.value)
        {
            case 0:
                ScenarioManager.Instance.SetTurnAmount(15);
                break;
            case 1:
                ScenarioManager.Instance.SetTurnAmount(30);
                break;
            case 2:
                ScenarioManager.Instance.SetTurnAmount(45);
                break;
            case 3:
                ScenarioManager.Instance.SetTurnAmount(60);
                break;
            case 4:
                ScenarioManager.Instance.SetTurnAmount(75);
                break;
            case 5:
                ScenarioManager.Instance.SetTurnAmount(90);
                break;
            default:
                Debug.LogError("Invalid dropdown item selected");
                break;
        }
    }

    public void SetTeleport(bool enabled)
    {
        if (enabled)
        {
            _teleportText.text = "ENABLED";
            _teleButtonText.text = "Disable";
            _teleportText.color = Color.green;
        }
        else
        {
            _teleportText.text = "DISABLED";
            _teleButtonText.text = "Enable";
            _teleportText.color = Color.red;
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
