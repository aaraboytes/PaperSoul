using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bucket
{
    public int amount;
    public GameObject prefab;
}
public class Pool : MonoBehaviour
{
    public static Pool Instance;

    [SerializeField] List<Bucket> _buckets = new List<Bucket>();
    private Dictionary<GameObject, Queue<GameObject>> pool = new Dictionary<GameObject, Queue<GameObject>>();

    private void Awake()
    {
        Instance = this;
        foreach(Bucket bucket in _buckets)
        {
            Queue<GameObject> queue = new Queue<GameObject>();
            for (int i = 0; i < bucket.amount; i++)
            {
                GameObject instance = Instantiate(bucket.prefab);
                instance.SetActive(false);
                queue.Enqueue(instance);
            }
            pool.Add(bucket.prefab, queue);
        }
    }
    public GameObject Recycle(GameObject prefab,Vector3 position = new Vector3(),Quaternion rotation = new Quaternion())
    {
        if (pool.ContainsKey(prefab))
        {
            Queue<GameObject> queue = pool[prefab];
            GameObject returnable = queue.Dequeue();
            queue.Enqueue(returnable);
            returnable.transform.position = position;
            returnable.transform.rotation = rotation;
            returnable.SetActive(true);
            return returnable;
        }
        return null;
    }
}
