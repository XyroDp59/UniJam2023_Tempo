using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    public class EnemyAIMovement : MoveBehaviour
    {
        private Transform player;
        private NavMeshAgent agent;
        // Start is called before the first frame update
        void Start()
        {
            player = GameObject.Find("Player").transform;
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }

        // Update is called once per frame
        void Update()
        {
            agent.SetDestination(player.position);
        }
    }
}
