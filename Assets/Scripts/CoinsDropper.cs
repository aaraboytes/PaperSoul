using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
[CustomEditor(typeof(CoinsDropper))]
public class CoinsDropperEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        CoinsDropper dropper = target as CoinsDropper;
        if (GUILayout.Button("Drop"))
        {
            dropper.Drop();
        }
    }
}
#endif
public class CoinsDropper : MonoBehaviour
{
    [SerializeField] GameObject _coinPrefab;
    [SerializeField] Vector2 _offset;
    [SerializeField] AudioClip _dropCoinSound;
    private List<GameObject> spawnedCoins = new List<GameObject>();
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position - Vector3.right * (_offset.x / 2), transform.position + Vector3.right * (_offset.x / 2));
        Gizmos.DrawLine(transform.position - Vector3.forward * (_offset.y / 2), transform.position + Vector3.forward * (_offset.y / 2));
    }
    public void Drop()
    {
        GameObject coin = Instantiate(_coinPrefab);
        coin.transform.position = transform.position + Vector3.right * Random.Range(-_offset.x / 2, _offset.x / 2) + Vector3.forward * Random.Range(-_offset.y / 2, _offset.y / 2);
        coin.transform.rotation = Random.rotation;
        spawnedCoins.Add(coin);
        AudioFXPlayer.Instance.PlaySound(_dropCoinSound, 0.5f);
    }
    public void Reset()
    {
        spawnedCoins.ForEach(c => { Destroy(c); });
        spawnedCoins.Clear();
    }
}
