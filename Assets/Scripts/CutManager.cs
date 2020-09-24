using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutManager : MonoBehaviour
{
    [SerializeField] FingerPointer _pointer;
    [SerializeField] AirplaneOrigami _airplaneOrigami;
    
    private CutSegment targetSegment;
    private CutSegment lastSegment;
    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
    }
    private void Start()
    {
        _airplaneOrigami.OnOrigamiCompleted += StopPointer;
    }
    private void Update()
    {
        if(_pointer.Active)
        {
            GameObject detectedObject = _pointer.TouchedObject;
            if (!detectedObject) return;
            CutSegment cutSegment = detectedObject.GetComponent<CutSegment>();
            if (cutSegment)
            {
                if((targetSegment == null && cutSegment.Head) || (cutSegment == targetSegment))
                {
                    CutSegment(cutSegment);
                    Debug.Log("Cortando segmento");
                }
                else if(lastSegment && cutSegment!=targetSegment && cutSegment != lastSegment)
                {
                    Debug.Log("Segmento equivocado");
                    CutSegment[] middleSegments = lastSegment.SegmentsToCurrent(cutSegment);
                    if (middleSegments!= null)
                    {
                        foreach (CutSegment segment in middleSegments)
                            segment.Cut();

                        CutSegment(cutSegment);
                    }
                }
            }
        }
    }
    private void CutSegment(CutSegment segment)
    {
        targetSegment = segment.Next;
        lastSegment = segment;
        segment.Cut();
        segment.Correct = true;
        if (segment.Tail)
        {
            segment.EndTrack();
        }
    }
    private void StopPointer()
    {
        _pointer.SetActive(false);
    }
    public void SetTargetSegment(CutSegment target)
    {
        targetSegment = target;
    }
    public bool VerifyCut(CutSegment currentTarget)
    {
        return currentTarget == targetSegment;
    }
    public void Reset()
    {
        _pointer.SetActive(true);
    }
}
