using UnityEngine;
using System.Collections;

public class EnemySpawnerController : MonoBehaviour {

	public GameObject EnemyGameObj;
	public GameObject EnemyGunObj;
	public int wave = 0;
	public int numEnemies = 3;

	// Use this for initialization
	void Start () {
		wave = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GameBegin(){
		wave = 1;
		SpawnWave ();
	}
	public void GameEnd(){

	}

	void SpawnEnemy(int style, int health, int atk, double speed, double fireRate, double bulletSpeed, int dropPower, Vector3 gunOffset){
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));
		GameObject enemy = (GameObject)Instantiate (EnemyGameObj);
		enemy.transform.position = new Vector3(Random.Range(min.x, max.x),max.y, enemy.transform.position.z);
		GameObject gun = (GameObject)Instantiate (EnemyGunObj);
		gun.GetComponent<EnemyGun>().Setup (style, atk, bulletSpeed, fireRate, gunOffset);
		enemy.GetComponent<EnemyControl>().Setup (health, speed, dropPower, (GameObject) gun);
	}

	public void SpawnWave(){
		switch(wave){
		case 6:
			numEnemies = 3;
			SpawnEnemy (0, 100, 10, 1, 1.25, 3, 3, new Vector3(0,0,0));
			SpawnEnemy (0, 100, 10, 1, 1, 3, 3, new Vector3(0,0,0));
			SpawnEnemy (0, 100, 10, 1, 1.25, 3, 3, new Vector3(0,0,0));
			break;
		case 2:
			numEnemies = 2;
			SpawnEnemy (2, 500, 10, 0.5, 0.05, 3, 10, new Vector3(0,0,0));
			SpawnEnemy (2, 500, 10, 0.5, 0.05, 3, 10, new Vector3(0,0,0));
			break;
		
		case 3:
			numEnemies = 1;
			SpawnEnemy (3, 2000, 10, 0.2, 0.01, 1, 15, new Vector3(0,0,0));
			break;
		case 4:
			numEnemies = 3;
			SpawnEnemy (1, 1000, 20, 0.5, 0.75, 3, 15, new Vector3(0,0,0));
			SpawnEnemy (0, 500, 10, 1, 1, 3, 3, new Vector3(0,0,0));
			SpawnEnemy (0, 500, 10, 1, 1, 3, 3, new Vector3(0,0,0));
			break;
		case 5:
			numEnemies = 2;
			SpawnEnemy (5, 2000, 10, 0.2, 2.5, 3, 15, new Vector3(0,0,0));
			SpawnEnemy (6, 1000, 50, 1, 1.25, 8, 5, new Vector3(0,0,0));
			break;
		case 1:
			numEnemies = 1;
			SpawnEnemy (7, 1000, 10, 1, 0.05, 0.5, 6, new Vector3(0,0,0));
			break;
		default:
			wave = 1;
			SpawnWave();
			break;
		}
	}


}
