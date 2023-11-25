using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    [SerializeField] private float attackSpeed = 0.5f;
    private float attackTiming = 0.2f;
    private float rechargeTime;

    private bool swinging;
  
    void Update()
    {
        rechargeTime += Time.deltaTime;
      
        if (Input.GetMouseButton(0) && rechargeTime >= attackSpeed)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            rechargeTime = 0;
            swinging = true;
        }

        if (rechargeTime >= attackTiming && swinging)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            swinging = false;
        }
    }
}
