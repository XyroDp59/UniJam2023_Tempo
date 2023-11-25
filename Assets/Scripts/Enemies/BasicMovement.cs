using System.Collections;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{

    public Transform target;
    private Rigidbody2D rb;
    [SerializeField]
    private float speed, pauseDuration;
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerController.instance.transform;
        rb = GetComponent<Rigidbody2D>();
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
}