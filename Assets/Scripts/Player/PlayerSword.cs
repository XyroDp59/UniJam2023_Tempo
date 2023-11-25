using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    private Transform player;
    private Camera camera;

    [SerializeField] private float attackSpeed = 0.5f;
    private float attackTiming = 0.2f;
    private float rechargeTime;

    private bool swinging;
    // Start is called before the first frame update
    void Start()
    {
        player = this.transform.parent;
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        rechargeTime += Time.deltaTime;
        Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 currentGunDirection = (this.transform.position - player.transform.position).normalized;
        Vector3 shotDirection = (mousePosition - player.transform.position).normalized;
        float angle = Vector3.Angle(currentGunDirection, shotDirection);
        transform.RotateAround(player.position, Vector3.forward, angle);
        if (Input.GetMouseButton(0) && rechargeTime >= attackSpeed)
        {
            GetComponent<PolygonCollider2D>().enabled = true;
            rechargeTime = 0;
            swinging = true;
        }

        if (rechargeTime >= attackTiming && swinging)
        {
            GetComponent<PolygonCollider2D>().enabled = false;
            swinging = false;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            
        }
    }
}
