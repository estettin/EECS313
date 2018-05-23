using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using UnityEngine.UI;
using UnityEngine;

public class FeedbackText : MonoBehaviour
{
	public float t;
	public bool changed;
	// Use this for initialization
	void Start ()
	{
		changed = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (changed)
		{
			t = Time.time;
			changed = false;
		}
		if (Time.time > t + 1f)
		{
			gameObject.GetComponentInChildren<Text>().text = "";
		}
		
	}
}
