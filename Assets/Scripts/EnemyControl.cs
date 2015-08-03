using UnityEngine;
using System.Collections;

public class EnemyControl : MonoBehaviour {

	GameObject scoreUITextGameObj;
	float speed;
	float vspeed;
	float hspeed;
	double health;
	double MaxHealth = 100;
	double timecount = 0;
	int dropPower = 1;
	
	public GameObject ExplosionGameObj;
	public GameObject PowerupObj;
	public GameObject EnemyGunObj;
	GameObject Global;
	GameObject EnemySpawner;
	GameObject player;
	// Use this for initialization
	void Start () {
		vspeed = speed;
		hspeed = 0f;
		Global = GameObject.FindGameObjectWithTag("GlobalTag");
		health = MaxHealth;
		scoreUITextGameObj = GameObject.FindGameObjectWithTag("ScoreTextTag");
		EnemySpawner = GameObject.FindGameObjectWithTag("SpawnerTag");
		player = GameObject.FindGameObjectWithTag("PlayerShipTag");
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(transform.position.x + hspeed * Time.deltaTime, transform.position.y - vspeed * Time.deltaTime, transform.position.z);

		Vector2 min1 = Camera.main.ViewportToWorldPoint(new Vector2(0f,0.9f));
		Vector2 min2 = Camera.main.ViewportToWorldPoint(new Vector2(1f,0.9f));
		if (transform.position.y < min1.y){
			transform.position = new Vector3(transform.position.x, min1.y, transform.position.z);
			vspeed = 0f;
			hspeed = speed;
		}
		if (transform.position.x <= min1.x){
			transform.position = new Vector3(min1.x, transform.position.y, transform.position.z);
			hspeed = speed;
		}
		if (transform.position.x >= min2.x){
			transform.position = new Vector3(min2.x, transform.position.y, transform.position.z);
			hspeed = -speed;
		}

		//Death check
		if(health <= 0){
			PlayExplosion ();
			player.GetComponent<PlayerControl>().AddHealth(dropPower);
			for(int i = 0; i < dropPower; i++){
				GameObject powerup = (GameObject)Instantiate (PowerupObj);
				powerup.transform.position = transform.position;
			}
			scoreUITextGameObj.GetComponent<GameScore>().Score += 100;
			if(--EnemySpawner.GetComponent<EnemySpawnerController>().numEnemies <= 0){
				EnemySpawner.GetComponent<EnemySpawnerController>().wave++;
				EnemySpawner.GetComponent<EnemySpawnerController>().SpawnWave();
			}
			Destroy (gameObject);
		}
	}
	public void Setup(double maxHealth, double speed, int dropPower, GameObject guns){
		MaxHealth = maxHealth;
		this.speed = (float) speed;
		this.dropPower = dropPower;
		//for(int i = 0; i < guns.Length; i++){
			guns.transform.position += transform.position;
			guns.transform.parent = transform;
		//}
	}
	void OnTriggerEnter2D(Collider2D col){
		if(col.tag == "PlayerBulletTag"){
			health -= col.gameObject.GetComponent<PlayerBullet>().GetAtk();
			GetComponent<AudioSource>().Play ();
		}
		if(col.tag == "PlayerShipTag"){

		}
	}

	void PlayExplosion(){
		GameObject explosion = (GameObject)Instantiate(ExplosionGameObj);
		explosion.transform.position = transform.position;
	}
}
