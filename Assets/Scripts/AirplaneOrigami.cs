using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AirplaneOrigami : MonoBehaviour
{
    public UnityAction OnOrigamiCompleted;
    [SerializeField] List<StepContainer> steps = new List<StepContainer>();
    [SerializeField] Animator _animator;
    [SerializeField] SpawnShine _shineSpawner;
    [SerializeField] LevelManager _levelManager;
    [SerializeField] AudioClip _foldSound; 

    [Header("Airplanes")]
    [SerializeField] GameObject _airplaneModel;
    [SerializeField] GameObject _airplaneDrawing;
    private int index = 0;
    private StepContainer currentStep;
    private List<float> scores = new List<float>();
    private void Start()
    {
        foreach(StepContainer step in steps)
        {
            step.Head.OnSegmentCompleted += EnableNextStep;
            step.gameObject.SetActive(false);
        }
        _shineSpawner.OnSpawnEnds += SwitchModel;
    }
#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            StartCoroutine(_shineSpawner.Shine());
        }
    }
#endif
    private void EnableNextStep()
    {
        scores.Add(currentStep.Head.Score);
        _animator.SetTrigger("Fold");
        AudioFXPlayer.Instance.PlaySound(_foldSound, 0.6f);
        currentStep.gameObject.SetActive(false);
        index++;
        if (index < steps.Count){
            currentStep = steps[index];
            currentStep.gameObject.SetActive(true);
        }
        else
        {
            OnOrigamiCompleted?.Invoke();
            int coins = 0;
            scores.ForEach(s => {if (s >= 1) coins++;});
            _levelManager.Coins = coins;
            StartCoroutine(_shineSpawner.Shine());
        }
    }
    private void SwitchModel()
    {
        _airplaneModel.SetActive(true);
        _airplaneDrawing.SetActive(false);
    }
    public void Reset()
    {
        scores.Clear();
        steps.ForEach(s => { 
            s.Reset();
            s.gameObject.SetActive(false);
        });
        currentStep = steps[0];
        currentStep.gameObject.SetActive(true);
        index = 0;
        _animator.SetTrigger("Reset");
        _airplaneModel.SetActive(false);
        _airplaneDrawing.SetActive(true);
    }
    public void StartCrafting()
    {
        currentStep = steps[0];
        currentStep.gameObject.SetActive(true);
    }
}
