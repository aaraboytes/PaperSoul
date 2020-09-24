using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuTransition : MonoBehaviour
{
    [Header("UI main menu elements")]
    [SerializeField] Image _title;
    [SerializeField] TextMeshProUGUI _clicToStart;
    [SerializeField] TextMeshProUGUI _titleText;
    [SerializeField] Button _button;
    [Header("Scene components")]
    [SerializeField] Transform _camera;
    [SerializeField] Transform _originCamPos;
    [SerializeField] Transform _targetCamPos;
    [SerializeField] AirplaneOrigami _airplane;
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(_targetCamPos.position, 0.1f);
    }
    public void Transition()
    {
        _button.interactable = false;
        LeanTween.value(1, 0, 1f).setOnUpdate((val) =>
        {
            Color currentTitleColor = _title.color;
            Color currentTitleText= _titleText.color;
            Color currentClicColor = _clicToStart.color;
            currentClicColor.a = val;
            currentTitleColor.a = val;
            currentTitleText.a = val;
            _title.color = currentTitleColor;
            _clicToStart.color = currentClicColor;
            _titleText.color = currentTitleText;
        });
        LeanTween.move(_camera.gameObject, _targetCamPos.position,2f).setOnComplete(
        ()=> {
            _airplane.StartCrafting();
            gameObject.SetActive(false);
        });
        LeanTween.rotate(_camera.gameObject, _targetCamPos.rotation.eulerAngles, 2f);
    }
}
