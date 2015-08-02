using UnityEngine;
using System.Collections;

public class EnemyGun : MonoBehaviour {

	public GameObject EnemyBulletGameObj;
	public Sprite[] BulletSprites;

	double atk;
	double fireRate;
	int style;
	float angle;
	float speed;
	double timecount = 0.25f;

	// Use this for initialization
	void Start () {
		//Invoke ("FireEnemyBullet", 1f);
	}
	
	// Update is called once per frame
	void Update () {
		timecount -= Time.deltaTime;
		if(timecount < 0){
			timecount = fireRate;
			FireEnemyBullet ();
		}

	}

	public void Setup(int style, double attack, double bulletSpeed, double fireRate, Vector3 offset){
		this.style = style;
		atk = attack;
		speed = (float) bulletSpeed;
		this.fireRate = fireRate;
		transform.position = offset;
		angle = 0;
	}

	void FireEnemyBullet(){
		float factor;
		GameObject bullet;
		GameObject playerShip = GameObject.Find ("PlayerGameObj");
		if(playerShip != null){
			switch(style){
			//Aim to player
			case 0:
				for(int i = 0; i < 3; i++){
					bullet = (GameObject)Instantiate (EnemyBulletGameObj);
					bullet.GetComponent<EnemyBulletController>().parent = this.gameObject;
					bullet.GetComponent<EnemyBulletController>().SetAtk(atk);
					bullet.GetComponent<EnemyBulletController>().SetSpeed(speed);
					bullet.transform.position = new Vector3(transform.position.x, transform.position.y, 0);

					bullet.GetComponent<EnemyBulletController>().SetDirection(new Vector2((float)(playerShip.transform.position.x - (bullet.transform.position.x + 1 - i)), 
					                                                                      (float)(playerShip.transform.position.y - (bullet.transform.position.y))));
				}
				break;

			//Circles
			case 1:
				factor = Random.Range(1f,16f);
				for(int i = 0; i < 32; i++){
					bullet = (GameObject)Instantiate (EnemyBulletGameObj);
					bullet.GetComponent<EnemyBulletController>().parent = this.gameObject;
					bullet.GetComponent<EnemyBulletController>().SetAtk(atk);
					bullet.GetComponent<EnemyBulletController>().SetSpeed(speed);
					bullet.GetComponent<SpriteRenderer>().sprite = BulletSprites[1];
					bullet.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
					
					bullet.GetComponent<EnemyBulletController>().SetDirection(new Vector2( 	Mathf.Sin ((float)(2*Mathf.PI*i/32.0+factor)), Mathf.Cos ((float)(2*Mathf.PI*i/32.0+factor))));
				}
				break;
			//Spray to player
			case 2:
				factor = Random.Range(-3f,3f);
				bullet = (GameObject)Instantiate (EnemyBulletGameObj);
				bullet.GetComponent<EnemyBulletController>().parent = this.gameObject;
				bullet.GetComponent<EnemyBulletController>().SetAtk(atk);
				bullet.GetComponent<EnemyBulletController>().SetSpeed(speed);
				bullet.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
				
				bullet.GetComponent<EnemyBulletController>().SetDirection(new Vector2(0 + factor, -1));
				break;
			//Spiral
			case 3:

				bullet = (GameObject)Instantiate (EnemyBulletGameObj);
				bullet.GetComponent<EnemyBulletController>().parent = this.gameObject;
				bullet.GetComponent<EnemyBulletController>().SetAtk(atk);
				bullet.GetComponent<EnemyBulletController>().SetSpeed(speed);
				bullet.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
				
				bullet.GetComponent<EnemyBulletController>().SetDirection(new Vector2(Mathf.Sin (angle), Mathf.Cos (angle)));
				angle+= 0.12f;
				break;

			//Checkerboardz
			case 4:
				
				bullet = (GameObject)Instantiate (EnemyBulletGameObj);
				bullet.GetComponent<EnemyBulletController>().parent = this.gameObject;
				bullet.GetComponent<EnemyBulletController>().SetAtk(atk);
				bullet.GetComponent<EnemyBulletController>().SetSpeed(speed);
				bullet.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
				bullet.GetComponent<EnemyBulletController>().SetDirection(new Vector2(Mathf.Sin (angle), Mathf.Cos (angle)));
				angle+= 0.5f;
				break;
			//Returning Bullets Circle
			case 5:
				for(int i = 0; i < 32; i++){
					bullet = (GameObject)Instantiate (EnemyBulletGameObj);
					bullet.GetComponent<EnemyBulletController>().parent = this.gameObject;
					bullet.GetComponent<EnemyBulletController>().SetAtk(atk);
					bullet.GetComponent<EnemyBulletController>().SetSpeed(speed);
					bullet.GetComponent<EnemyBulletController>().SetAccel(-0.005f);
					bullet.GetComponent<SpriteRenderer>().sprite = BulletSprites[2];
					bullet.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
					bullet.GetComponent<EnemyBulletController>().SetDirection(new Vector2( 	Mathf.Sin ((float)(2*Mathf.PI*i/32.0)), Mathf.Cos ((float)(2*Mathf.PI*i/32.0))));
				
				}
				break;

			//Single Bullet
			case 6:
				
				bullet = (GameObject)Instantiate (EnemyBulletGameObj);
				bullet.GetComponent<EnemyBulletController>().parent = this.gameObject;
				bullet.GetComponent<EnemyBulletController>().SetAtk(atk);
				bullet.GetComponent<EnemyBulletController>().SetSpeed(speed);
				bullet.GetComponent<SpriteRenderer>().sprite = BulletSprites[1];
				bullet.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
				bullet.GetComponent<EnemyBulletController>().SetDirection(new Vector2((float)(playerShip.transform.position.x - (bullet.transform.position.x)), 
				                                                          (float)(playerShip.transform.position.y - (bullet.transform.position.y))));

				break;
			default:
				break;
			}
		}
	}
}
