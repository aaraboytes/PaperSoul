using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : MonoBehaviour
{
    public int Coins { get { return coins; } set { coins = value; OnCoinsUpdated?.Invoke(); } }
    public UnityAction OnCoinUsed;
    public UnityAction OnCoinsUpdated;
    public UnityAction OnOrigamiLaunched;
    public UnityAction OnOrigamiReset;
    
    private int coins;
    public void UseCoin()
    {
        if (coins > 0)
        {
            OnCoinUsed?.Invoke();
            coins--;
        }
    }
}
