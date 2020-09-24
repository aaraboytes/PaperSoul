using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCalculator : MonoBehaviour
{
    public float Distance => distance;
    [SerializeField] Transform _origin;
    [SerializeField] Transform _target;
    private float distance;
    private void Update()
    {
        Vector3 targetToOrigin =  _origin.position + _target.position;
        float angle = Vector3.Angle(targetToOrigin.normalized, _origin.forward);
        distance = Mathf.Abs(targetToOrigin.magnitude * Mathf.Cos(angle * Mathf.Deg2Rad));
    }
}
