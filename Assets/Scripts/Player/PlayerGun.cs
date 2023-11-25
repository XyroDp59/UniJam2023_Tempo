using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    private Transform player;
    private Camera camera;
    [SerializeField] GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        player = this.transform.parent;
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 currentGunDirection = (this.transform.position - player.transform.position).normalized;
        Vector3 shotDirection = (mousePosition - player.transform.position).normalized;
        this.transform.up = shotDirection;
        //Debug.DrawRay(this.transform.position, shotDirection);

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletPrefab, this.transform.position + shotDirection, this.transform.rotation);
        }
    }
}
