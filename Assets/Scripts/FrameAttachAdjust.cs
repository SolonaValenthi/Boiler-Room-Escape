using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FrameAttachAdjust : MonoBehaviour
{
    [SerializeField] private Transform[] _attachPoints;

    private FrameID _hoveredID;
    private XRSocketInteractor _socket;

    void Start()
    {
        _socket = GetComponent<XRSocketInteractor>();
    }

    public void SetAttach()
    {
        var picture = _socket.interactablesHovered[0];
        _hoveredID = picture.transform.GetComponent<FrameID>();

        if (_hoveredID.GetID() == 3)
            _socket.attachTransform = _attachPoints[1];
        else
            _socket.attachTransform = _attachPoints[0];
    }
}