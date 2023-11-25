using UnityEngine;

namespace Enemies
{
    public class EnemyDirectMovement : MonoBehaviour
    {
        private Transform player;
        private Rigidbody2D rb;
        [SerializeField] private float speed = 3.0f;
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.Find("Player").transform;
            rb = GetComponentInParent<Rigidbody2D>();
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector3 directionToPlayer = (player.transform.position - this.transform.position).normalized;
            rb.AddForce(Time.deltaTime * speed * directionToPlayer);
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, 2f);
        }
    }
}
