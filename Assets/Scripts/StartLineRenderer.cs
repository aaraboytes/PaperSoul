using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLineRenderer : MonoBehaviour
{
    public CutSegmentHead Head { set { _head = value; } }
    [SerializeField] CutSegmentHead _head;
    [SerializeField]private ProgressFollower progressFollower;
    private LineRenderer line;
    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }
    private void Start()
    {
        progressFollower = _head.ProgressFollower;
    }
    private void Update()
    {
        line.SetPosition(0, _head.StartPoint);
        line.SetPosition(1, progressFollower.transform.position);
    }
}
