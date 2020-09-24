using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCraftDone : MonoBehaviour
{
    [SerializeField] CamFollower _camFollower;
    [SerializeField] Animator _houseAnimator;
    [SerializeField] SpawnShine _spawn;
    [SerializeField] GameObject _airplaneModel;
    [SerializeField] AvionControl _airplaneController;
    private void Awake()
    {
        _spawn.OnSpawnEnds += PrepareToLaunch;
    }
    private void PrepareToLaunch()
    {
        _camFollower.enabled = true;
        _houseAnimator.SetTrigger("Open");
        LeanTween.moveY(_airplaneModel, _airplaneModel.transform.position.y + 0.2f,2f).setOnComplete(() =>
        {
            _airplaneController.enabled = true;
        });
    }
}
