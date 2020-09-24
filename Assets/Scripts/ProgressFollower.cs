using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressFollower : MonoBehaviour
{
    public CutSegmentHead Head { set { _head = value; }get { return _head; } }
    [SerializeField] CutSegmentHead _head;
    private void Update()
    {
        Vector3 dir = _head.EndPoint - _head.StartPoint;
        transform.position = Vector3.Lerp(transform.position,_head.StartPoint + dir * _head.Progress,0.5f);
    }
}
