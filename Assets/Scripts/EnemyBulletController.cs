using UnityEngine;
using System.Collections;

public class EnemyBulletController : MonoBehaviour {

	float speed;
	float accel = 0;
	Vector2 dir;
	bool isReady;
	double atk;
	int spriteIndex = 0;
	Vector3 prevPos = new Vector3();
	public GameObject parent;


	void Awake(){
		isReady = false;
	}

	// Use this for initialization
	void Start () {

	}
	public void SetDirection(Vector2 direction){
		dir = direction.normalized;
		isReady = true;
	}
	public void SetAtk(double val){
		atk = val;
	}

	public void SetSpeed(float val){
		speed = val;
	}
	public void SetAccel(float val){
		accel = val;
	}

	public double GetAtk(){
		return atk;
	}
	
	// Update is called once per frame
	void Update () {
		if(isReady){
			if(parent == null){
				Destroy (gameObject);
			}

			Vector2 position = transform.position;
			position += dir * speed * Time.deltaTime;
			transform.position = position;

			Vector2 min, max;
			if(accel != 0f){
				min = Camera.main.ViewportToWorldPoint(new Vector2 (-3f,-3f));
				max = Camera.main.ViewportToWorldPoint(new Vector2 (4f,4f));
			} else {
				min = Camera.main.ViewportToWorldPoint(new Vector2 (-0.1f,-0.1f));
				max = Camera.main.ViewportToWorldPoint(new Vector2 (1.1f,1.1f));
			}
			if((transform.position.x < min.x) || (transform.position.y < min.y) ||
			   (transform.position.y > max.y) || (transform.position.x > max.x)){
				Destroy (gameObject);
			}
			speed += accel;
			Vector3 moveDirection = transform.position - prevPos; 
			if (moveDirection != Vector3.zero) 
			{
				float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
			}
			prevPos = transform.position;
		}
	}
	void OnTriggerEnter2D(Collider2D col){
		if(col.tag == "PlayerShipTag"){
			Destroy (gameObject);
		}
	}
}
