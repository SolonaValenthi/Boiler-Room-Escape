using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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

    [Header("Menu")]
    [SerializeField] InputActionReference _openMenu;
    [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _movementMenu;
    [SerializeField] private GameObject _interactionMenu;
    [SerializeField] private GameObject _audioMenu;
    [SerializeField] private GameObject _backButton;
    [SerializeField] private GameObject[] _menuInteractors;

    [Space]
    [Header("Tutorial")]
    [SerializeField] private GameObject _tutorial;
    [SerializeField] private GameObject[] _pages;
    [SerializeField] private GameObject _prevButton;
    [SerializeField] private GameObject _nextButton;
    [SerializeField] private GameObject _startButton;

    [Space]
    [Header("Motion")]
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

    [Space]
    [Header("Interaction")]
    [SerializeField] private Slider _raySlider;
    [SerializeField] private TMP_Text _rayText;
    [SerializeField] private TMP_Text _rayButtonText;
    [SerializeField] private TMP_Text _rayRange;

    [Space]
    [Header("Audio")]
    [SerializeField] private Slider[] _volumeSliders;
    [SerializeField] private TMP_Text[] _volumeValues;

    [Space]
    [Header("Victory")]
    [SerializeField] private GameObject _victoryCanvas;
    [SerializeField] private TMP_Text _runTime;

    private bool _menuOpen = true;
    private int _currentPage = 1;
    
    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (var interactor in _menuInteractors)
        {
            interactor.SetActive(true);
        }
    }

    private void OnEnable()
    {
        var openMenuAction = GetInputAction(_openMenu);

        if (openMenuAction != null)
            openMenuAction.performed += OpenMenu;
    }

    public void CloseTutorial()
    {
        _tutorial.SetActive(false);
        CloseMenu();
    }

    public void NextPage()
    {
        _pages[_currentPage - 1].SetActive(false);
        _currentPage++;
        _pages[_currentPage - 1].SetActive(true);

        if (_currentPage == 6)
        {
            _nextButton.SetActive(false);
            _startButton.SetActive(true);
        }
    }

    public void PrevPage()
    {
        _pages[_currentPage - 1].SetActive(false);
        _currentPage--;
        _pages[_currentPage - 1].SetActive(true);

        if (_currentPage == 1)
        {
            _prevButton.SetActive(false);
            _nextButton.SetActive(false);
        }
        else if (_currentPage < 6)
        {
            _nextButton.SetActive(true);
            _startButton.SetActive(false);
        }
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
        ScenarioManager.Instance.EnableRays();
        _menuOpen = false;
        _menuCanvas.SetActive(false);
        _interactionMenu.SetActive(false);
        _movementMenu.SetActive(false);
        _audioMenu.SetActive(false);
        _backButton.SetActive(false);

        foreach (var interactor in _menuInteractors)
        {
            interactor.SetActive(false);
        }
    }

    public void BackButton()
    {
        _interactionMenu.SetActive(false);
        _movementMenu.SetActive(false);
        _audioMenu.SetActive(false);
        _backButton.SetActive(false);
        _mainMenu.SetActive(true);
    }

    // Set the correct ID within the inspector 0 = movement, 1 = interaction, 2 = audio
    public void OpenSubMenu(int ID)
    {
        _backButton.SetActive(true);
        _mainMenu.SetActive(false);

        switch (ID)
        {
            case 0:
                _movementMenu.SetActive(true);
                break;
            case 1:
                _interactionMenu.SetActive(true);
                break;
            case 2:
                _audioMenu.SetActive(true);
                break;
            default:
                Debug.LogError("Invalid ID detected.");
                break;
        }
    }

    public void CompletionTime(int minutes, int seconds)
    {
        StartCoroutine(VictoryDelay());
        _runTime.text = $"{minutes} minutes {seconds} seconds";
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

    public void SetRayRange()
    {
        _rayRange.text = $"{Mathf.Round(_raySlider.value * 10) / 10}";
        ScenarioManager.Instance.SetRayRange(_raySlider.value);
    }

    // set correct ID within inspecter 0 = master, 1 = ambience, 2 = interface
    public void SetVolume(int ID)
    {
        _volumeValues[ID].text = $"{_volumeSliders[ID].value}";
        ScenarioManager.Instance.SetVolume(ID, _volumeSliders[ID].value / 100);
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

    public void SetRays(bool enabled)
    {
        if (enabled)
        {
            _rayText.text = "ENABLED";
            _rayButtonText.text = "Disable";
            _rayText.color = Color.green;
        }
        else
        {
            _rayText.text = "DISABLED";
            _rayButtonText.text = "Enable";
            _rayText.color = Color.red;
        }
    }

    IEnumerator VictoryDelay()
    {
        yield return new WaitForSeconds(2);
        _victoryCanvas.SetActive(true);

        foreach (var interactor in _menuInteractors)
        {
            interactor.SetActive(true);
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
