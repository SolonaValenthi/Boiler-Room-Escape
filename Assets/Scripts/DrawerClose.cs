using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To be placed on a parent object containing one or more hinge based grabbable drawers
// This script assumes all drawers share the same "closed" position.
public class DrawerClose : MonoBehaviour
{
    [SerializeField] private float _closedPosition; // The closed value of the drawer's movement axis
    [SerializeField] private GameObject[] _drawers;

    Vector3 _tempPos;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _drawers.Length; i++)
        {
            _tempPos = _drawers[i].transform.localPosition;
            _drawers[i].transform.localPosition = new Vector3(_tempPos.x, _tempPos.y, _closedPosition);
        }
    }
}
