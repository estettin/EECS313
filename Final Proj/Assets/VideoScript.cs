using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoScript : MonoBehaviour
{
	public float startTime;
	// Use this for initialization
	void Start ()
	{
		foreach (var ui in FindObjectsOfType<Button>())
		{
			ui.GetComponent<RectTransform>().localScale = new Vector3(0,0,0);   
		}
		foreach (var text in FindObjectsOfType<Text>())
		{
			text.GetComponent<RectTransform>().localScale = new Vector3(0,0,0);     
		}
		foreach (var sb in FindObjectsOfType<Scrollbar>())
		{
			sb.GetComponent<RectTransform>().localScale = new Vector3(0,0,0);   
		}
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > startTime + 10f && gameObject.tag == "longeroption")
		{
			foreach (var ui in FindObjectsOfType<Button>())
			{
				if (ui.tag !="C1")
				{
					ui.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
				}
			}
			foreach (var text in FindObjectsOfType<Text>())
			{
				if (text.tag != "C4")
				{
					text.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
				}
			}
			foreach (var sb in FindObjectsOfType<Scrollbar>())
			{
				sb.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);    
			}
			Destroy(gameObject);
		}
		else if (Time.time > startTime + 5f && gameObject.tag != "longeroption")
		{
			foreach (var ui in FindObjectsOfType<Button>())
			{
				if (ui.tag !="C1")
				{
					ui.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);
				}
			}
			foreach (var text in FindObjectsOfType<Text>())
			{
				if (text.tag != "C4")
				{
					text.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
				}
			}
			foreach (var sb in FindObjectsOfType<Scrollbar>())
			{
				sb.GetComponent<RectTransform>().localScale = new Vector3(1,1,1);    
			}
			Destroy(gameObject);
		}
	}
}
