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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.CompareTag("Wall"))
        {
            Stick();
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
        PlayerLauncher.instance.BulletList.Remove(this.gameObject);
    }

    public void Explode()
    {
        Destroy(gameObject);
        return;
    }
}