using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    [SerializeField] Image _fadeImage;
    [SerializeField] float _fadeTime;
    public UnityAction OnFadedIn;
    public UnityAction OnFadedOut;
    private void Start()
    {
        _fadeImage.gameObject.SetActive(false);
        _fadeImage.color = new Color(0, 0, 0, 0);
    }
    public void FadeIn()
    {
        _fadeImage.color = new Color(0, 0, 0, 1);
        _fadeImage.gameObject.SetActive(true);
        LeanTween.value(1, 0, _fadeTime).setOnUpdate((val) =>
        {
            Color fadeColor = _fadeImage.color;
            fadeColor.a = val;
            _fadeImage.color = fadeColor;
        }).setOnComplete(()=>
        {
            OnFadedIn?.Invoke();
            _fadeImage.gameObject.SetActive(false);
        });
    }
    public void FadeOut()
    {
        _fadeImage.color = new Color(0, 0, 0, 0);
        _fadeImage.gameObject.SetActive(true);
        LeanTween.value(0, 1, _fadeTime).setOnUpdate((val) =>
        {
            Color fadeColor = _fadeImage.color;
            fadeColor.a = val;
            _fadeImage.color = fadeColor;
        }).setOnComplete(()=> 
        {
            OnFadedOut?.Invoke();
        });
    }
}
