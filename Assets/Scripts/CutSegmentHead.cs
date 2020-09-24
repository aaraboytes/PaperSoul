using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutSegmentHead : CutSegment
{
    public float Progress => progress;
    public float Score => score;
    public ProgressFollower ProgressFollower => _progressFollower;
    public List<CutSegment> Segments { get { return segments; } set { segments = value; } }
    public Vector3 StartPoint { get { return _startPoint; } set { _startPoint = value; } }
    public Vector3 EndPoint { get { return _endPoint; } set { _endPoint = value; } }
    public UnityAction OnSegmentCompleted;
    [Header("Head")]
    [SerializeField] Vector3 _startPoint;
    [SerializeField] Vector3 _endPoint;
    [SerializeField] [Range(0,1)] private float progress;
    [SerializeField] ProgressFollower _progressFollower;
    [SerializeField] private List<CutSegment> segments = new List<CutSegment>();
    private int cuttedSegments = 0;
    private float score;

    public void EndAndCalculateScore()
    {
        int currentScore = 0;
        foreach (CutSegment segment in segments)
        {
            segment.gameObject.SetActive(false);
            currentScore += segment.Correct ? 1 : 0;
        }
        score = (float)currentScore / segments.Count;
        OnSegmentCompleted?.Invoke();
        UIScore.Instance.ShowScore(score,segments[segments.Count-1].transform.position);
    }
    public void NotifyCut()
    {
        cuttedSegments++;
        progress = (float)cuttedSegments / segments.Count;
    }
    public bool IsSegmentForward(CutSegment first, CutSegment last)
    {
        int firstIndex = 0;
        int lastIndex = 0;
        for (int i = 0; i < segments.Count; i++)
        {
            if (segments[i] == first) firstIndex = i;
            else if (segments[i] == last)
            {
                lastIndex = i;
                break;
            }
        }
        return firstIndex < lastIndex;
    }
    public void Reset()
    {
        segments.ForEach(s => {
            s.Reset();
            s.gameObject.SetActive(true); 
        });
        progress = 0;
        cuttedSegments = 0;
    }
}
