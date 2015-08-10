using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerControl : MonoBehaviour {

	public GameObject GameManagerGameObj;
	public GameObject PlayerBulletGameObj;
	public GameObject bulletPosition1;
	public GameObject bulletPosition2;
	public GameObject ExplosionGameObj;
	public GameObject scoreUITextGameObj;
	public Slider slider;
	public Sprite[] BulletSprites;	

	public Text PowerUIText;

	public float offset;

	const double MaxPower = 5.0;
	double MaxHealth;
	double BaseHealth = 100;
	double power;
	double health;
	double BaseAtk = 10;
	double atk;

	float timecount = 0.1f;


	public float speed;
	public void Init(){
		power = 1;
		MaxHealth = BaseHealth;
		health = MaxHealth;
		atk = BaseAtk;
		PowerUIText.text = "Power: "+power.ToString ("F3");
		slider.value = 100;
		transform.position = new Vector3(0,0,transform.position.z);
		gameObject.SetActive(true);
	}

	// Use this for initialization
	void Start () {

	}

	void ShootBullet(Vector3 pos, double attack, Vector2 direction, Sprite image){
		GameObject bullet = (GameObject)Instantiate (PlayerBulletGameObj);
		bullet.transform.position = pos;
		bullet.GetComponent<PlayerBullet>().SetAtk(attack);
		bullet.GetComponent<PlayerBullet>().SetDir(direction);
		bullet.GetComponent<SpriteRenderer>().sprite = image;
	}
	// Update is called once per frame
	void Update () {
		timecount -= Time.deltaTime;
		if(timecount < 0){
			timecount = 0.1f;

			//Level 1
			ShootBullet (new Vector3(transform.position.x, transform.position.y), atk, new Vector2(0,1), BulletSprites[2]);
			if(power > 2){
				ShootBullet (new Vector3(transform.position.x - offset, transform.position.y), atk, new Vector2(0,1), BulletSprites[0]); 
				ShootBullet (new Vector3(transform.position.x + offset, transform.position.y), atk, new Vector2(0,1), BulletSprites[0]); 
			}

			if(power > 3){
				ShootBullet (new Vector3(transform.position.x - offset, transform.position.y), atk/2, new Vector2(-1,8), BulletSprites[1]); 
				ShootBullet (new Vector3(transform.position.x + offset, transform.position.y), atk/2, new Vector2(1,8), BulletSprites[1]); 
			}
			if(power > 4){
				ShootBullet (new Vector3(transform.position.x - offset/2, transform.position.y), atk/3, new Vector2(0,1), BulletSprites[1]); 
				ShootBullet (new Vector3(transform.position.x + offset/2, transform.position.y), atk/3, new Vector2(0,1), BulletSprites[1]); 
			}
		
			
			scoreUITextGameObj.GetComponent<GameScore>().Score += 1;
		}
		/*
		float x = Input.GetAxisRaw ("Horizontal");
		float y = Input.GetAxisRaw("Vertical");

		Vector2 direction = new Vector2(x, y).normalized;*/
		Move();

		if(power<MaxPower) power += 0.0001; else power = MaxPower;
		PowerUIText.text = "Power: "+power.ToString("F3");

		//atk = BaseAtk*power;
		//health += MaxHealth/10000;

		//Checks
		if(health > MaxHealth){
			health = MaxHealth;
		}
		if(health <= 0){
			health = MaxHealth;
			power--;
		}
		slider.value = ((float)(health)*100)/((float)(MaxHealth));
		// PowerUIText.text = "Power: "+power.ToString()+"x";
		if(power <= 0){
			GameManagerGameObj.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
			gameObject.SetActive(false);
			
		}
	}
	
	void Move(){
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0,0));
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1,1));

		Vector3 target;
		target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		target.z = transform.position.z;

		if(target.x > max.x)
			target.x = max.x;
		if(target.y < min.y)
			target.y = min.y;
		if(target.x < min.x)
			target.x = min.x;
		if(target.y > max.y)
			target.y = max.y;


		transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);


		/*
		Vector2 pos = transform.position;

		pos += direction * speed * Time.deltaTime;

		pos.x = Mathf.Clamp (pos.x, min.x, max.x);
		pos.y = Mathf.Clamp (pos.y, min.y, max.y);

		transform.position = pos;*/
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.tag == "EnemyBulletTag"){
			PlayExplosion ();
			health -= col.gameObject.GetComponent<EnemyBulletController>().GetAtk();

		}
		if(col.tag == "EnemyShipTag"){

		}
		if(col.tag == "PowerUpTag"){

		}
	}

	void PlayExplosion(){
		GameObject explosion = (GameObject)Instantiate (ExplosionGameObj);
		explosion.transform.position = transform.position;
	}

	public void SetPower(double val){
		power = val;
		PowerUIText.text = "Power: "+power.ToString("F3");

	}
	public void AddPower(double val){
		power += val;
		PowerUIText.text = "Power: "+power.ToString("F3");

	}

	public void SetHealth(double val){
		health = val;
	}

	public void AddHealth(double val){
		health += val;
	}
	public void SetAtk(double val){
		atk = val;
	}
	public double GetPower(){
		return power;
	}
	public double GetHealth(){
		return health;
	}
	public double GetAtk(){
		return atk;
	}
}
