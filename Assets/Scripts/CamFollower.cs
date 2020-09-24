using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollower : MonoBehaviour
{
    [SerializeField] float _followSpeed;
    [SerializeField] Transform _followTarget;
    [SerializeField] Transform _lookAtTarget;
    [SerializeField] Vector3 _offset;
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, _followTarget.position + _offset, _followSpeed * Time.deltaTime);
        transform.LookAt(_lookAtTarget);
    }
}
