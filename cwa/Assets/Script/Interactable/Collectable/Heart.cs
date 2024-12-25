using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour,ICollectable
{
    public static event Action OnHeartCollected; 
    public void Collect()
    {
        OnHeartCollected?.Invoke();
        Destroy(gameObject);
    }
}
