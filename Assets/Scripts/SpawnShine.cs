using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpawnShine : MonoBehaviour
{
    [SerializeField] ParticleSystem _shines;
    [SerializeField] ParticleSystem _light;
    public UnityAction OnSpawnEnds;
    public IEnumerator Shine()
    {
        _shines.Play();
        yield return new WaitForSeconds(1);

        _light.transform.localScale = Vector3.zero;
        _light.gameObject.SetActive(true);
        LeanTween.scale(_light.gameObject, Vector3.one, 2.5f).setLoopPingPong(1).setOnComplete(()=> { _light.gameObject.SetActive(false); });
        bool allHided = false;
        LeanTween.value(_shines.emission.rateOverTime.constant, 500, 2.5f).setLoopPingPong(1).setOnUpdate((val) =>
        {
            if (val == 500 && !allHided)
            {
                allHided = true;
                OnSpawnEnds?.Invoke();
            }
            var emission = _shines.emission;
            emission.rateOverTime = val;
        }).setOnComplete(()=>
        {
            _shines.Stop();
        });
    }
}
