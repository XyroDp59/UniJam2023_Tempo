using UnityEngine;

public class WarpManager : MonoBehaviour
{
    [SerializeField] float mapUpperBound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string test = other.gameObject.tag;
        if (test == "Player" || test == "Projectile" || test == "Enemy")
        {
            Debug.Log("Player detected!");
            Vector3 originalPosition = other.transform.position;
            other.transform.position = new Vector3(originalPosition.x, mapUpperBound, originalPosition.z);
        }
    }
}
