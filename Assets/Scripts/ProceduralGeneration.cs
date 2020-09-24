using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(ProceduralGeneration))]
public class ProceduralGenerationEditor: Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ProceduralGeneration generator = target as ProceduralGeneration;
        if (GUILayout.Button("Reset"))
            generator.Reset();
    }
}
#endif
public class ProceduralGeneration : MonoBehaviour
{
    [SerializeField] int _forwardSegments, _backSegments;
    [SerializeField] int _initialSegmentsToOmit;
    [SerializeField] float _segmentsSize;
    [SerializeField] DistanceCalculator _calculator;
    [SerializeField] Transform _target;
    [SerializeField] Transform _origin;
    [SerializeField] List<GameObject> _obstaclePrefabs;
    [SerializeField] List<GameObject> _normalPrefabs;

    private int currentSegment = 0;
    private int lastSegment = 0;
    private Queue<GameObject> segments = new Queue<GameObject>();
    private List<int> segmentIsFilled = new List<int>();
    private void OnDrawGizmos()
    {
        /*for (int i = 0; i < 10; i++)
        {
            Gizmos.color = Color.HSVToRGB(i / 10f,1,1);
            Gizmos.DrawCube(_origin.position + _origin.forward * _segmentsSize * i, new Vector3(1, 1, _segmentsSize));
        }
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(_origin.position + _origin.forward * _segmentsSize * currentSegment, 1);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(_origin.position + _origin.forward * _segmentsSize * (currentSegment - _backSegments), 1);
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(_origin.position + _origin.forward * _segmentsSize * (currentSegment + _forwardSegments), 1);*/
    }
    private void Start()
    {
        Reset();
    }
    private void Update()
    {
        currentSegment = (int)Mathf.Round(_calculator.Distance / _segmentsSize);
        if(currentSegment != lastSegment)
        {
            GameObject segment = segments.Dequeue();
            segment.SetActive(false);
            for (int i = currentSegment - _backSegments; i < currentSegment + _forwardSegments; i++)
            {
                if (!segmentIsFilled.Contains(i))
                {
                    segment = Pool.Instance.Recycle(_normalPrefabs[Random.Range(0, _normalPrefabs.Count)]);
                    segment.transform.position = _origin.position + _origin.forward * _segmentsSize * i;
                    segments.Enqueue(segment);
                    segmentIsFilled.Add(i);
                }
            }
        }
        lastSegment = currentSegment;
    }
    public void Reset()
    {
        if (segments.Count > 0)
        {
            foreach (GameObject segment in segments)
                segment.SetActive(false);
        }
        segmentIsFilled.Clear();
        segments.Clear();
        for (int i = -_backSegments; i < _forwardSegments; i++)
        {
            GameObject segment = Pool.Instance.Recycle(_normalPrefabs[Random.Range(0, _normalPrefabs.Count)]);
            segment.transform.position = _origin.position + _origin.forward * _segmentsSize * i;
            segments.Enqueue(segment);
            segmentIsFilled.Add(i);
            if (i <= _initialSegmentsToOmit)
            {
                segment.SetActive(false);
            }
        }
    }
}
