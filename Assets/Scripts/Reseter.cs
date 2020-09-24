using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reseter : MonoBehaviour
{
    [SerializeField] AirplaneOrigami _origami;
    [SerializeField] Animator _houseAnimator;
    [SerializeField] CameraTransitionReseter _cameraReseter;
    [SerializeField] CoinsDropper _coinDropper;
    [SerializeField] CutManager _cutManager;
    [SerializeField] Fader _fader;
    [SerializeField] GameObject _airplane;
    [SerializeField] LevelManager _levelManager;
    [SerializeField] ProceduralGeneration _proceduralGeneration;
    [SerializeField] Transform _origin;
    [SerializeField] UICoins _uiCoins;
    #if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            ResetAirplane();
    }
#endif
    private void Start()
    {
        _fader.OnFadedOut += ResetAirplane;
    }
    private void ResetAirplane()
    {
        Rigidbody body = _airplane.GetComponent<Rigidbody>();
        body.velocity = Vector3.zero;
        body.isKinematic = true;
        _airplane.GetComponent<AvionControl>().Reset();
        _airplane.transform.position = _origin.position;
        _airplane.transform.rotation = _origin.rotation;
        _cameraReseter.Reset();
        _coinDropper.Reset();
        _cutManager.Reset();
        _origami.Reset();
        _proceduralGeneration.Reset();
        _uiCoins.Reset();
        _houseAnimator.SetTrigger("Close");
        _levelManager.OnOrigamiReset?.Invoke();
        StartCoroutine(WaitChanges());
    }
    private IEnumerator WaitChanges()
    {
        yield return new WaitForSeconds(2);
        _fader.FadeIn();
    }
    public void ExecuteReset()
    {
        _fader.FadeOut();
    }
}
