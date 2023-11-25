using UnityEngine;

namespace Enemies
{
    public class EnemyDirectMovement : MonoBehaviour
    {
        private Transform player;
        [SerializeField] private float speed = 3.0f;
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.Find("Player").transform;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 directionToPlayer = (player.transform.position - this.transform.position).normalized;
            transform.Translate(Time.deltaTime * speed * directionToPlayer);
        }
    }
}
