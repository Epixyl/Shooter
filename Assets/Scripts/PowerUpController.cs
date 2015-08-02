﻿using UnityEngine;
using System.Collections;

public class PowerUpController : MonoBehaviour {

	float vspeed;
	float accel = 0.03f;
	float hspeed;
	Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2 (0,0));
	Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2 (1,1));

	// Use this for initialization
	void Start () {
		vspeed = Random.Range (-1.5f, -3f);
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
	}
	void OnTriggerEnter2D(Collider2D col){
		if(col.tag == "PlayerShipTag"){
			col.gameObject.GetComponent<PlayerControl>().AddPower(0.02);
			Destroy (gameObject);
		}
	}
}