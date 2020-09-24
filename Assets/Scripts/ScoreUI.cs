using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _score;
    [SerializeField] GameObject _newScoreAdvice;
    [SerializeField] Animator _scoreAnimator;
    [SerializeField] DistanceCalculator _calculator;
    [SerializeField] AvionControl _avioncito;
    private void Start()
    {
        _avioncito.OnCrashed += ShowScore;
    }
    public void CraftAgain()
    {
        _scoreAnimator.SetBool("Showed", false);
    }
    public void ShowScore()
    {
        _scoreAnimator.SetBool("Showed", true);
        int distance = (int)Mathf.Round(_calculator.Distance);
        _score.text = distance.ToString();
        if (distance >= PlayerPrefs.GetInt("SCORE", 0))
        {
            _newScoreAdvice.gameObject.SetActive(true);
            PlayerPrefs.SetInt("SCORE",distance);
        }
    }
}
