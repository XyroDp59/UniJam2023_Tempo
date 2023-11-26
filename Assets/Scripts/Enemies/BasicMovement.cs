using System.Collections;
using UnityEngine;

public class BasicMovement : MoveBehaviour
{

    public Transform target;
    private Rigidbody2D rb;
    [SerializeField]
    private float speed, pauseDuration;
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerController.instance.transform;
        rb = GetComponentInParent<Rigidbody2D>();
        StartCoroutine(Bouge());
    }

    private IEnumerator Bouge()
    {
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(pauseDuration);
        Vector3 vecteur = (target.transform.position - transform.position).normalized;
        rb.AddForce(vecteur * speed);
        yield return new WaitForSeconds(pauseDuration);
        StartCoroutine(Bouge());
    }

    public void Destroy()
    {
        transform.parent.GetComponent<EnemyState>().Destroy();
    }
}