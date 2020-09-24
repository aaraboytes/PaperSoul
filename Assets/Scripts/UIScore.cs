using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    public static UIScore Instance;
    [SerializeField] GameObject _scorePrefab;
    [SerializeField] CoinsDropper _coinsDropper;
    private void Awake()
    {
        Instance = this;
    }
    public void ShowScore(float score,Vector3 sourcePos)
    {
        if (score == 1)
            _coinsDropper.Drop();
        GameObject scoreIndicator = Instantiate(_scorePrefab, transform);
        scoreIndicator.transform.position = Camera.main.WorldToScreenPoint(sourcePos);
        if (score > 0.5f) {
            scoreIndicator.transform.localScale += new Vector3(score, score, score) *0.5f;
        };
        UIScoreIndicator uiScore = scoreIndicator.GetComponent<UIScoreIndicator>();
        uiScore.Write((int)(score * 100));
        uiScore.Spawn();
    }
}
