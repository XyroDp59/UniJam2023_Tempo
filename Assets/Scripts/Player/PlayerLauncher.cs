using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLauncher : MonoBehaviour
{
    [SerializeField] private float fireSpeed = 0.7f;
    private float rechargeTime;
    private Transform player;
    private Camera camera;
    [SerializeField] GameObject bulletPrefab;
    public List<GameObject> BulletList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        player = this.transform.parent;
        camera = Camera.main;
        rechargeTime = fireSpeed;
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
        rechargeTime += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && rechargeTime >= fireSpeed)
        {
            GameObject newBullet;
            newBullet = Instantiate(bulletPrefab, this.transform.position + shotDirection, this.transform.rotation);
            BulletList.Add(newBullet);
            rechargeTime = 0;
        }
    }

    public void ActivateBullet()
    {
        foreach(GameObject bullet in BulletList)
        {
            bullet.GetComponent<MineManager>().active = true;
        }
    }
}