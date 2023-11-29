using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DegatMec : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") && !collision.gameObject.GetComponent<EnemyState>().isDying)
        {
            PlayerController.instance.takeDamage();
            Debug.Log("Tappe");
			collision.gameObject.GetComponent<EnemyState>().Die();
        }
    }
}
