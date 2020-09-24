using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICoins : MonoBehaviour
{
    [SerializeField] LevelManager _levelManager;
    [SerializeField] List<GameObject> _coins = new List<GameObject>();
    private int coinIndex = 0;
    private int maxCoins;
    private void Start()
    {
        _levelManager.OnCoinsUpdated += GetMaxCoins;
        _levelManager.OnCoinUsed += UseCoin;
    }
    private void GetMaxCoins()
    {
        maxCoins = _levelManager.Coins;
        Debug.Log("[UICoins] : Getting max coins " + maxCoins);
        for (int i = 0; i < _coins.Count; i++)
        {
            if (i > maxCoins - 1)
            {
                _coins[i].gameObject.SetActive(false);
            }
            else
            {
                _coins[i].gameObject.SetActive(true);
            }
        }

    }
    private void UseCoin()
    {
        _coins[maxCoins-1 - coinIndex].GetComponent<Coffee.UIEffects.UIDissolve>().Play();
        coinIndex++;
    }
    public void Reset()
    {
        coinIndex = 0;
        _coins.ForEach(g => {
            g.SetActive(true);
            g.GetComponent<Coffee.UIEffects.UIDissolve>().Stop();
        });
    }
}
