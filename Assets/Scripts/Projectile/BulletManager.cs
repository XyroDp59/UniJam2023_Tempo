using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private float timeToLive;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(20 * Time.deltaTime * Vector3.up);
        timeToLive += Time.deltaTime;
        if (timeToLive >= 1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
