using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlideUI : MonoBehaviour
{
    [SerializeField] Animator _uiAnimator;
    [SerializeField] LevelManager _levelManager;
    [SerializeField] Button _launchButton;
    [SerializeField] AvionControl _avioncito;

    private void Start()
    {
        _levelManager.OnOrigamiLaunched += ShowUI;
        _levelManager.OnOrigamiReset += HideUI;
        _avioncito.OnCrashed += DisableButton;
    }
    private void ShowUI()
    {
        _uiAnimator.SetBool("Showed", true);
        _launchButton.interactable = true;
    }
    private void HideUI()
    {
        _uiAnimator.SetBool("Showed", false);
    }
    private void DisableButton()
    {
        _launchButton.interactable = false;
    }
}
