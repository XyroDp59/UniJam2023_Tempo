using System;
using UnityEngine;

public class MineManager : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public bool active = false;
    public int knockback;
    private void Start()
    {
        active = false;
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * speed);
        Debug.Log(rb.velocity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy") && active)
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            return;
        }else if (collision.gameObject.CompareTag("Enemy"))
        {
           // Vector2 awayDirection = collision.transform.position - this.transform.position;
            //awayDirection = awayDirection.normalized;
            //collision.gameObject.GetComponent<Rigidbody2D>().AddForce(awayDirection * knockback, ForceMode2D.Impulse);

        }

        if(collision.gameObject.CompareTag("Wall"))
        {
            Stick();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && active)
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void Stick()
    {
        GetComponent<CircleCollider2D>().isTrigger = true;
        rb.velocity = Vector3.zero;
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    private void OnDestroy()
    {
        //Player
    }
}