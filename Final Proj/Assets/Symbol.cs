using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.Build.AssetBundle;
using UnityEngine;

public class Symbol : MonoBehaviour
{

	public Color color;
	//public Rigidbody2D rb; 
	public float velocity;
	public Vector2 direction;
	public Vector2 pos;
	//public SpriteRenderer sr; 
	public int state;
	public int played;
	public bool activated;
	public bool correct;
	public float t;
	public bool created;
	public float startingPosition;
	public int result;
	
	// Use this for initialization
	void Start ()
	{
		//rb = new Rigidbody2D();
		//sr = new SpriteRenderer();
		result = 0;
		activated = false;
		state = 0;
		t = Time.time;
		velocity = .1f;
		direction = new Vector3(0,-1f);
		played = 0;
		created = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Time.time > .2f + t && !created)
		{
			//rb = GetComponent<Rigidbody2D>();
			//sr = GetComponent<SpriteRenderer>();
			GetComponent<Rigidbody2D>().velocity = velocity * direction * played;
			created = true;
		}
		if (Time.time > .2f + t)
		{
			//rb.velocity = velocity * direction * played;
			if (GetComponent<Rigidbody2D>().position.y < -2.55f && GetComponent<Rigidbody2D>().position.y > -3.45f)
			{
				activated = true;

			}
			else if (GetComponent<Rigidbody2D>().position.y <= -3.45f && !correct)
			{
				state = 1;
				activated = false;
			}
			if (GetComponent<Rigidbody2D>().position.y < -4.5f)
			{
				Finish();
			}
			UpdateColor();
		}
		
	}

	public void UpdateVelocity(float v)
	{
		velocity = v;
		GetComponent<Rigidbody2D>().velocity = velocity * direction * played;
		FindObjectOfType<Runner>().velocity = v;
	}

	public void ToggleVelocity()
	{
		if (played == 0)
		{
			played = 1;
			FindObjectOfType<Runner>().played = 1;
		}
		else
		{
			played = 0;
			FindObjectOfType<Runner>().played = 0;
		}
	}

	public void UpdateColor()
	{
		switch (state)
		{
			case 1:
				GetComponent<SpriteRenderer>().color = Color.red;
				break;
			case 2:
				GetComponent<SpriteRenderer>().color = Color.green;
				break;
			case 3:
				GetComponent<SpriteRenderer>().color = Color.yellow;
				break;
			case 0:
				GetComponent<SpriteRenderer>().color = Color.white;
				break;
			default:
				GetComponent<SpriteRenderer>().color = Color.white;
				break;
		}
	}

	public void Finish()
	{
		//send accuracy information
		FindObjectOfType<Player>().symbolCounter--;
		string type;
		if (GetComponent<ToeSymbol>() != null)
		{
			type = "toe";
		}
		else
		{
			type = "heel";
		}
		if (GetComponent<Rigidbody2D>().position.x < -2.9f && GetComponent<Rigidbody2D>().position.x > -3.1)
		{
			var go = FindObjectOfType<LeftTrackQueue>();

			go.AddFeeback(result, startingPosition, type);
		}
		else
		{
			var go = FindObjectOfType<RightTrackQueue>();
			go.AddFeeback(result, startingPosition, type);
		}
		Destroy(gameObject);
	}
}
