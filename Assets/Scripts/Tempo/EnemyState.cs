using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
	private List<GameObject> stateObjectList = new List<GameObject>();
	private int currentState; // 0 = R ; 1 = V ; 2 = B
	public bool isDying = false;
	
    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 0; i <=2 ; i++) stateObjectList.Add(transform.GetChild(i).gameObject);
    }

	public void SetState(int state){
		//Debug.Log(state);
		for(int i = 0; i<=2;i++)
			if(i == state)transform.GetChild(i).gameObject.SetActive(true);
			else transform.GetChild(i).gameObject.SetActive(false);
			
		currentState = state;
	}
	public void Die()
	{	
        SpawnManager.instance.EnemyList.Remove(this.gameObject);
		transform.GetChild(currentState).GetComponent<Animator>().SetTrigger("DCD");
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        GetComponent<Rigidbody2D>().isKinematic = true;
        isDying = true;
        // transform.GetChild(currentState).GetComponent
	}

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
