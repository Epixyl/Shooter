using UnityEngine;
using System.Collections;

public class PowerUpController : MonoBehaviour {

	float vspeed;
	float accel = 0.04f;
	float hspeed;
	Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2 (0,0));
	Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2 (1f,1.5f));

	// Use this for initialization
	void Start () {
		vspeed = Random.Range (-1.5f, -2.5f);
		hspeed = Random.Range (-1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		if(vspeed < 4) vspeed += accel;
		transform.position -= new Vector3(hspeed * Time.deltaTime, vspeed * Time.deltaTime);
		if(transform.position.x > max.x){
			transform.position = new Vector3(max.x, transform.position.y);
		}
		if(transform.position.x < min.x){
			transform.position = new Vector3(min.x, transform.position.y);
		}		
		if((transform.position.x < min.x) || (transform.position.y < min.y) ||
		   (transform.position.y > max.y) || (transform.position.x > max.x)){
			Destroy (gameObject);
		}
	}
	void OnTriggerEnter2D(Collider2D col){
		if(col.tag == "PlayerShipTag"){
			col.gameObject.GetComponent<PlayerControl>().AddPower(0.02);
			Destroy (gameObject);
		}
	}
}
