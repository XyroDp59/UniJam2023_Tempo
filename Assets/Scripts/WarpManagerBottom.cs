using UnityEngine;

public class WarpManagerBottom : MonoBehaviour
{
    [SerializeField] float mapUpperBound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        string test = other.gameObject.tag;
        if ((test == "Player" || test == "Projectile" || test == "Enemy") && other.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            Vector3 originalPosition = other.transform.position;
            other.transform.position = new Vector3(originalPosition.x, mapUpperBound, originalPosition.z);
        }
    }
}
