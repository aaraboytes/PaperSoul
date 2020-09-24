using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransitionReseter : MonoBehaviour
{
    [SerializeField] CamFollower _camFollower;
    [SerializeField] Transform _camera;
    [SerializeField] Transform _targetCamPos;
    public void Reset()
    {
        _camFollower.enabled = false;
        _camera.position = _targetCamPos.position;
        _camera.rotation = _targetCamPos.rotation;
    }
}
