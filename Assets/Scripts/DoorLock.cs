using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorLock : MonoBehaviour
{
    [SerializeField] private GameObject _openedLockPrefab;
    [SerializeField] private XRSocketInteractor _socketInteractor;

    private GameObject _registeredKey;

    public void RegisterKey()
    {
        var selectedKey = _socketInteractor.interactablesHovered[0];
        _registeredKey = selectedKey.transform.gameObject;
    }

    public void UnregisterKey()
    {
        _registeredKey = null;
    }

    public void Unlock()
    {
        Instantiate(_openedLockPrefab, transform.position, Quaternion.identity);
        Destroy(_registeredKey?.gameObject);
        Destroy(this.gameObject);
    }
}