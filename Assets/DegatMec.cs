using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DegatMec : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            PlayerController.instance.takeDamage();
            Debug.Log("Tappe");
        }
    }
}
