using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrollerObstacle : MonoBehaviour {

	public int Height;

	public Rigidbody2D Rigidbody;

	private bool moving = false;
	private float moveSpeed;

	void Start () {
		
	}

	void Update () {
		if (!moving)
			return;
		//Rigidbody.AddForce(Vector2.left * moveSpeed,ForceMode2D.Impulse);
		Rigidbody.velocity = Vector2.left * moveSpeed;
	}

	public void StartMoving(float speed)
	{
		moveSpeed = speed;
		moving = true;
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if(other.collider.tag=="Player")
		{
			Debug.Log("HIT PLAYER! GAME OVER");
			FindObjectOfType<SideScrollerLevelControl>().PlayerLost();
		}
	}
}
