using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour,ICollectable
{
    public static event Action OnCoinCollected; 
    public void Collect()
    {
        OnCoinCollected?.Invoke();
        Destroy(gameObject);
    }
}
