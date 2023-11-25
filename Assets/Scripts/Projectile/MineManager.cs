using UnityEngine;

public class MineManager : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public bool active = false;

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
        active = true;
    }
}