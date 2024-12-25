using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour,ICollectable
{
    public static event Action OnStarCollected; 
    public void Collect()
    {
        OnStarCollected?.Invoke();
        Destroy(gameObject);
    }
}
