using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
[SerializeField] float angle = 200f;
        void Update()
    {
        transform.Rotate(new Vector3(0, angle,0) * Time.deltaTime);
    }
}
