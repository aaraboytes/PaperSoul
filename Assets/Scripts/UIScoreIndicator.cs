using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreIndicator : MonoBehaviour
{
    [SerializeField] GameObject _container;
    [SerializeField] Image _image;
    [SerializeField] TMPro.TextMeshProUGUI _text;
    [SerializeField] float _time;
    [SerializeField] LeanTweenType _ease;
    private IEnumerator Dissolve()
    {
        yield return new WaitForSeconds(_time);
        LeanTween.value(1, 0, 1f).setOnUpdate((val) =>
        {
            Color currentImageColor = _image.color;
            Color currentTextColor = _text.color;
            currentImageColor.a = val;
            currentTextColor.a = val;
            _image.color = currentImageColor;
            _text.color = currentTextColor;
        }).setOnComplete(()=>
        {
            Destroy(gameObject);
        });
    }
    public void Write(int score)
    {
        _text.text = score.ToString();
    }
    public void Spawn()
    {
        _container.SetActive(true);
        StartCoroutine(Dissolve());
        LeanTween.moveLocalY(gameObject ,transform.localPosition.y + 40,1f).setEase(_ease);
    }
}
