using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpManager : MonoBehaviour
{
    [SerializeField] float mapUpperBound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player") {
            Debug.Log("Player detected!");
            Vector3 originalPosition = other.transform.position;
            other.transform.position = new Vector3(originalPosition.x, mapUpperBound, originalPosition.z);
        }
    }
}