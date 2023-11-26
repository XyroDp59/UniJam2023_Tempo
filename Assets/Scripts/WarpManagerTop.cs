using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpManagerTop : MonoBehaviour
{
    [SerializeField] float mapLowerBound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        string test = other.gameObject.tag;
        if (test == "Player" || test == "Projectile" || test == "Enemy" && other.GetComponent<Rigidbody2D>().velocity.y > 0)
        {
            Vector3 originalPosition = other.transform.position;
            other.transform.position = new Vector3(originalPosition.x, mapLowerBound, originalPosition.z);
        }
    }
}
