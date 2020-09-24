using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBuildings : MonoBehaviour
{
    [SerializeField] GameObject[] _buildings;
    private bool firstTime = true;
    private Vector3[] originalSizes;
    private Quaternion[] originalRotations;
    private void Awake()
    {
        originalSizes = new Vector3[_buildings.Length];
        originalRotations = new Quaternion[_buildings.Length];
        for (int i = 0; i < _buildings.Length; i++)
        {
            originalSizes[i] = _buildings[i].transform.localScale;
            originalRotations[i] = _buildings[i].transform.rotation;
            _buildings[i].transform.localScale = Vector3.zero;
            _buildings[i].transform.rotation = Quaternion.Euler(Vector3.up * Random.Range(-360f, 360f));
        }
    }
    private void OnEnable()
    {
        if (!firstTime)
        {
            for (int i = 0; i < _buildings.Length; i++)
            {
                float time = Random.Range(0.5f, 1.2f);
                LeanTween.scale(_buildings[i], originalSizes[i], time);
                LeanTween.rotate(_buildings[i], originalRotations[i].eulerAngles, time);
            }
        }
        else
            firstTime = false;
    }
    private void OnDisable()
    {
        for (int i = 0; i < _buildings.Length; i++)
        {
            _buildings[i].transform.localScale = Vector3.zero;
            _buildings[i].transform.rotation = Quaternion.Euler(Vector3.up * Random.Range(-360f, 360f));
        }
    }
}
