using UnityEngine;

namespace Enemies
{
    public class EnemyDirectMovement : MonoBehaviour
    {
        private Transform player;
        private Rigidbody2D rb;
        [SerializeField] private float speed = 3.0f;
        [SerializeField] private float maxSpeed = 5.0f;
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.Find("Player").transform;
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            Vector2 directionToPlayer = (player.transform.position - this.transform.position).normalized;
            rb.velocity += Time.deltaTime * speed * directionToPlayer;
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = maxSpeed * rb.velocity.normalized;
            }
        }
    }
}
