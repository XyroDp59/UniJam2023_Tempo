using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MineDetectionObsever : MonoBehaviour
{

    private CircleCollider2D colliderTrigger;
    private bool isTrigger = false;
    // Start is called before the first frame update
    void Start()
    {
        
        colliderTrigger = GetComponent<CircleCollider2D>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, colliderTrigger.radius);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isTrigger && transform.parent.GetComponent<MineManager>().active)
        {
            colliderTrigger.radius *= 1.2f;
            isTrigger = true;
            Debug.Log("caca pipi");
            StartCoroutine(Peter());
        }


    }
    private IEnumerator Peter()
    {
        yield return new WaitForSeconds(0.5f);
        Collider2D[] lstBolosse = Physics2D.OverlapCircleAll(transform.position, colliderTrigger.radius,LayerMask.GetMask("Enemy"));
        foreach (Collider2D item in lstBolosse)
        {
            Destroy(item.gameObject);
        }
        
        transform.parent.GetComponent<MineManager>().Explode();
        Destroy(gameObject);
    }

}