using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    public GameObject[] animalPrefabsArray;
    private GameObject animalSelected;

    private float spawnRangeX = 23f;
    private float spawnRangeZ = 7f;
    //public Camera cam;

    [SerializeField] private float startSpawnDelay = .5f;
    public float spawnInterval = 1f;

    public GameObject player;
    Vector3 spawnPosition;

    public static int wavenumber;



	public List<GameObject> EnemyList = new List<GameObject>();
	public int actualState=0;


    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);    // Suppression d'une instance précédente (sécurité...sécurité...)

        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        wavenumber = 0;
        StartNewWave();
    }
	


    public void StartNewWave()
    {
        InvokeRepeating("SpawnRandomAnimal", startSpawnDelay, spawnInterval);
        spawnInterval -= 0.1f;
        wavenumber++;
    }

    void SpawnRandomAnimal()
    {
        int index = Random.Range(0, animalPrefabsArray.Length);
        int side = Random.Range(0, 2);
        

        animalSelected = animalPrefabsArray[index];

		GameObject newEnemy;

        if (side == 0) //gauche
        {
            //spawnPosition = cam.ViewportToWorldPoint(new Vector3(0, Random.Range(0,1), cam.nearClipPlane));
            spawnPosition = new Vector3(- spawnRangeX, Random.Range(-spawnRangeZ,spawnRangeZ), 0);
            newEnemy = Instantiate(animalSelected, spawnPosition, Quaternion.Euler(new Vector3(0, 0, 0)));

        }
        
        else //droite
        {
            //spawnPosition = cam.ViewportToWorldPoint(new Vector3(1, Random.Range(0, 1), cam.nearClipPlane));
            spawnPosition = new Vector3(spawnRangeX, Random.Range(-spawnRangeZ, spawnRangeZ), 0);
            newEnemy = Instantiate(animalSelected,spawnPosition , Quaternion.Euler(new Vector3(0,0,0 )));
        }
		EnemyList.Add(newEnemy);
		newEnemy.GetComponent<EnemyState>().SetState(actualState);

    }
	
	
	public void UpdateStateEnemies()
	{

		List<int> PossibleStates = new List<int>();
		for (int i = 0; i <=2; i++)
			if (i != actualState) PossibleStates.Add(i);
		
		int newState = PossibleStates[Random.Range(0,2)];
		
		foreach(GameObject enemy in EnemyList){
			enemy.GetComponent<EnemyState>().SetState(newState);
		}
		actualState = newState;
	}
	
	
}


    