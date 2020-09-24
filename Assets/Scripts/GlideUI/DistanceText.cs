using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DistanceText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _meters;
    [SerializeField] TextMeshProUGUI _unit;
    [SerializeField] DistanceCalculator _calculator;
    private void Update()
    {
        _meters.text = Mathf.Round(_calculator.Distance).ToString();
    }
}
