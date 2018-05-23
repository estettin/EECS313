using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class Runner : MonoBehaviour
{
	public int played;
	public Vector3 direction;
	public float velocity;
	// Use this for initialization
	void Start () {
		direction = new Vector3(0,1f);
		velocity = .1f;
		played = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		GetComponent<Rigidbody2D>().velocity = velocity * direction * played;
	}
}
