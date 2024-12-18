using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<MeshRenderer>().material.color = new Color(255,0,0);

        }
    }
}
