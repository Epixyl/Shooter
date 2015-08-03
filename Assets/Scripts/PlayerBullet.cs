using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {

	float speed;
	Vector3 dir;
	double atk = 1;
	GameObject player;

	// Use this for initialization
	void Start () {
		speed = 8f;
		player = GameObject.FindGameObjectWithTag("PlayerShipTag");
	}

	public void SetDir(Vector2 val){
		dir = new Vector3(val.x,val.y).normalized;
	} 
	
	// Update is called once per frame
	void Update () {
		Vector3 position = transform.position;
		position += dir * speed * Time.deltaTime;
		transform.position = position;

		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2 (0,0));
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2 (1,1));
		
		if((transform.position.x < min.x) || (transform.position.y < min.y) ||
		   (transform.position.y > max.y) || (transform.position.x > max.x)){
			Destroy (gameObject);
		}

	}
	void OnTriggerEnter2D(Collider2D col){
		if(col.tag == "EnemyShipTag"){
			Destroy (gameObject);
		}
	}
	public double GetAtk(){
		return atk;
	}

	public void SetAtk(double val){
		atk = val;
	}

	public void SetSpeed(float val){
		speed = val;
	}
}
