using System.Collections;
using UnityEngine;

public class Teleguidash : MonoBehaviour
{

    public Transform target;
    private Rigidbody2D rb;
    [SerializeField]
    private float speed, pauseDuration, minAngle, maxAngle, dashDurations, dashCooldown;
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerController.instance.transform;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(BougeBien());
    }

    private IEnumerator BougeBien()
    {
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(dashCooldown);
        Vector3 vecteur = (target.transform.position - transform.position).normalized;
        rb.AddForce(vecteur * speed);
        yield return new WaitForSeconds(dashDurations);
        StartCoroutine(BougeMal());
    }

    private IEnumerator BougeMal()
    {
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(dashCooldown);
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Vector3 perpendicular = new Vector3(-direction.y, direction.x, 0);
        float randomness = Random.Range(minAngle, maxAngle);
        int sign = Random.Range(0, 2) == 1 ? -1 : 1;
        direction = (direction + (perpendicular * sign * 1.75f * randomness)).normalized;
        rb.AddForce(direction * speed);
        yield return new WaitForSeconds(dashDurations);
        StartCoroutine(BougeBien());
    }

}