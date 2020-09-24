using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(LineGenerator))]
public class LineGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        LineGenerator generator = (LineGenerator)target;
        if (GUILayout.Button("Generate"))
        {
            generator.Clear();
            generator.Generate();
        }
    }
}
#endif
public class LineGenerator : MonoBehaviour
{
    [SerializeField] float _thickness,_segmentLength;
    [SerializeField] Vector3 _pointA, _pointB;
    [SerializeField] GameObject _headLineSegmentPrefab;
    [SerializeField] GameObject _lineSegmentPrefab;
    [SerializeField] GameObject _startSegmentPrefab;
    [SerializeField] GameObject _progressSegmentPrefab;
    private List<GameObject> segments = new List<GameObject>();

    private void OnDrawGizmosSelected()
    {
        if (_segmentLength > 0)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(_pointA, 0.025f);
            Gizmos.DrawWireSphere(_pointB, 0.025f);
            Vector3 dir = _pointB - _pointA;
            int index = 0;
            Gizmos.color = Color.red;
            for (float progress = 0; progress < dir.magnitude; progress += _segmentLength)
            {
                if (index % 2 == 0)
                {
                    Gizmos.DrawLine(_pointA + (progress - _segmentLength) * dir.normalized, _pointA + progress * dir.normalized);
                }
                index++;
            }
        }
    }
    public void Generate()
    {
        Vector3 direction = _pointB - _pointA;
        Vector3 start = _pointA;
        Vector3 end = Vector3.zero;
        GameObject stepContainer = new GameObject("Step container");
        StepContainer step = stepContainer.AddComponent(typeof(StepContainer)) as StepContainer;
        int index = 0;
        for (float progress = 0; progress < direction.magnitude; progress+=_segmentLength)
        {
            Vector3 segmentPos = _pointA + direction.normalized * progress;
            GameObject segment;
            if (index == 0)
                segment = Instantiate(_headLineSegmentPrefab, segmentPos, Quaternion.LookRotation(direction.normalized, Vector3.up));
            else
                segment = Instantiate(_lineSegmentPrefab, segmentPos, Quaternion.LookRotation(direction.normalized, Vector3.up));
            segment.transform.localScale = new Vector3(_thickness, segment.transform.localScale.y, _segmentLength);
            segment.transform.parent = stepContainer.transform;
            segments.Add(segment);
            end = segmentPos;
            index++;
        }

        CutSegmentHead head = null;
        List<CutSegment> cutsegments = new List<CutSegment>();
        for (int i = 0; i < segments.Count; i++)
        {
            CutSegment cutSegment = segments[i].GetComponent<CutSegment>();
            cutsegments.Add(cutSegment);
            if (i == 0)
            {
                cutSegment.Head = true;
                head = cutSegment as CutSegmentHead;
            }
            else if (i > 0)
                cutSegment.Previous = segments[i - 1].GetComponent<CutSegment>();
            if (i < segments.Count - 1)
                cutSegment.Next = segments[i + 1].GetComponent<CutSegment>();
            else if (i == segments.Count - 1)
                cutSegment.Tail = true;
            cutSegment.HeadSegment = head;
        }
        head.Segments = cutsegments;
        head.StartPoint = start;
        head.EndPoint = end;

        GameObject line = Instantiate(_startSegmentPrefab,stepContainer.transform);
        LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        segments.Add(line);

        StartLineRenderer startLineRenderer = line.GetComponent<StartLineRenderer>();
        startLineRenderer.Head = head;
        
        line = Instantiate(_progressSegmentPrefab,stepContainer.transform);
        lineRenderer = line.GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
        segments.Add(line);

        ProgressLineRenderer progressLineRenderer = line.GetComponent<ProgressLineRenderer>();
        progressLineRenderer.Head = head;

        step.Head = head;
    }
    public void Clear()
    {
        segments.Clear();
    }
}
