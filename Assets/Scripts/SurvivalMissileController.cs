﻿using UnityEngine;
using System.Collections;

public class SurvivalMissileController : MonoBehaviour {

	public float moveSpeed;

	public float forceOnImpact;
	public int damageToPlayer;

	public Rigidbody2D myRigidBody2D;
	public GameObject player;

	public GameObject explosion;

	public SurvivalHealthManager healthManager;

	// Use this for initialization
	void Start () {
		healthManager = FindObjectOfType<SurvivalHealthManager> ();
		player = GameObject.Find ("UFO");
		myRigidBody2D = GetComponent<Rigidbody2D> ();
		if (transform.position.x < player.transform.position.x) {
			transform.localScale = transform.localScale * -1;
		} else {
			moveSpeed = moveSpeed * -1;
		}
	}

	// Update is called once per frame
	void Update () {
		myRigidBody2D.velocity = new Vector2 (moveSpeed, 0f);
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			healthManager.damagePlayer (damageToPlayer);
			SurvivalPlayerController playerController = other.GetComponent<SurvivalPlayerController> ();
			if (transform.position.x > other.transform.position.x) {
				playerController.applyForce (-forceOnImpact);
			} else {
				playerController.applyForce (forceOnImpact);
			}
			destroyMissile ();
		}
	}

	public void destroyMissile(){
		Instantiate (explosion, transform.position, transform.rotation);
		Destroy (gameObject);
	}
}
