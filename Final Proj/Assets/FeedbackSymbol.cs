using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedbackSymbol : MonoBehaviour {

	// Use this for initialization
	public int played;
	public Vector3 direction;
	public float velocity;
	
	void Start () {
		velocity = .5f;
		direction = new Vector3(0,-1f);
		played = 1;
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody2D>().velocity = velocity * direction * played;
	}
	
	public void UpdateDirection(Vector2 dir)
	{
		direction = dir;
	}

	public void ToggleVelocity()
	{
		if (played == 0)
		{
			played = 1;
		}
		else
		{
			played = 0;
		}
	}
}
