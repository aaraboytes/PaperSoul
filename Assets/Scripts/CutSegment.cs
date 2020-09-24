using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSegment : MonoBehaviour
{
    public bool Head = false;
    public bool Tail = false;
    public bool Cutted = false;
    public bool Correct = false;
    public CutSegment Previous;
    public CutSegment Next;
    public CutSegmentHead HeadSegment;
    public void Cut()
    {
        if (!Cutted)
        {
            HeadSegment.NotifyCut();
            Cutted = true;
        }
    }
    public void Reset()
    {
        Cutted = false;
        Correct = false;
    }
    public void ResetFromHead()
    {
        Reset();
        HeadSegment.ResetTrack();
    }
    public void ResetTrack()
    {
        Reset();
        if (Next)
            Next.ResetTrack();
    }
    public void EndTrack()
    {
        HeadSegment.EndAndCalculateScore();
    }

    public CutSegment[] SegmentsToCurrent(CutSegment jumpSegment)
    {
        if (HeadSegment.IsSegmentForward(this,jumpSegment))
        {
            List<CutSegment> segments = HeadSegment.Segments;
            List<CutSegment> middleSegments = new List<CutSegment>();
            bool pastThis = false;
            foreach(CutSegment segment in segments)
            {
                if (pastThis)
                {
                    middleSegments.Add(segment);
                }
                if (segment == this) pastThis = true;
                else if (segment == jumpSegment) break;
            }
            return middleSegments.ToArray();
        }else
            return null;
    }
}
