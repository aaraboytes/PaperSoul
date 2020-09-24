using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Origami/Blueprint")]
public class OrigamiBlueprint : ScriptableObject
{
    List<OrigamiStep> steps = new List<OrigamiStep>();
}

[System.Serializable]
public class OrigamiStep
{
    public Vector3 Start;
    public Vector3 End;
    public GameObject Model;
}