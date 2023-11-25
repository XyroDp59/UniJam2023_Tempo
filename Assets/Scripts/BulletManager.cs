using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private void Start()
    {
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(2 * Time.deltaTime * Vector3.up);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
}
