using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorAlert : MonoBehaviour
{
	private float StartTime;
	// Use this for initialization
	void Start ()
	{
		StartTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > StartTime + .2f)
		{
			Destroy(gameObject);
		}
	}
}
