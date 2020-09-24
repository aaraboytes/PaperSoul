using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Animator _pauseAnimator;
    private bool paused = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            _pauseAnimator.SetBool("Paused", paused);
        }
    }
    public void Continue()
    {
        paused = false;
        _pauseAnimator.SetBool("Paused", false);
    }
    public void ExitToDesktop()
    {
        Application.Quit();
    }
}
