using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGunOrienter : MonoBehaviour
{
    private Transform player;
    private Camera camera;
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
        float angle = Vector3.Angle(currentGunDirection, shotDirection);
        transform.RotateAround(player.position, Vector3.forward, angle);
    }
}
