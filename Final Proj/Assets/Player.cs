using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Player : MonoBehaviour
{
	public bool started;
	public int symbolCounter;
	public int totalCount;
	public int correctCount;
	public bool feedbackMode;
	
	// Use this for initialization
	void Start ()
	{
		started = false;
		symbolCounter = 0;
		feedbackMode = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		HandleInput();
		if (!feedbackMode)
		{
			var scrollbar = FindObjectOfType<Scrollbar>();
			var val = 2f * scrollbar.value + .5f;
			foreach (var o in FindObjectsOfType<Symbol>())
			{
				if (Time.time > o.t + .21f)
				{
					o.UpdateVelocity(val);
				}

			}
		}
		if (started && !feedbackMode)
		{
			var accuracy = (float) correctCount / totalCount;
			accuracy *= 100f;
			accuracy = Mathf.Round(accuracy);
			var go = GameObject.FindWithTag("Accuracy");
			if (totalCount == 0)
			{
				go.GetComponent<Text>().text = "Accuracy: ";
			}
			else
			{
				go.GetComponent<Text>().text = "Accuracy: " + accuracy.ToString() + "%";	
			}
			
		}
		EnterFeedbackMode();
	}

	public void EnterFeedbackMode()
	{
		if (started && symbolCounter == 0 && !feedbackMode)
		{
			feedbackMode = true;
			foreach (var go in FindObjectsOfType<Button>())
			{
				if (go.GetComponent<ControlComponent>() == null)
				{
					Destroy(go.gameObject);
				}
			}
			foreach (var go in FindObjectsOfType<Text>())
			{
				if (go.GetComponent<ControlComponent>() == null)
				{
					Destroy(go.gameObject);
				}
				else if (go.text == "")
				{
					go.text = "Pause";
				}
			}
			foreach (var go in FindObjectsOfType<Scrollbar>())
			{
				Destroy(go.gameObject);
			}
			Destroy(GameObject.FindWithTag("Pause").gameObject);
			CreateFeedbackScreen();
			
		}
	}
	
	public void CreateFeedbackScreen() {
		//Create the actual (with their colors)
		FindObjectOfType<LeftTrackQueue>().CreateFeedback();
		FindObjectOfType<RightTrackQueue>().CreateFeedback();
		var go1 = GameObject.FindWithTag("C1").GetComponent<RectTransform>();
		go1.localScale = new Vector3(1,1,1);
		var go4 = GameObject.FindWithTag("C4").GetComponent<RectTransform>();
		go4.localScale = new Vector3(1,1,1);
		go4.tag = "C1";
		var go5 = GameObject.FindWithTag("C4").GetComponent<RectTransform>();
		go5.localScale = new Vector3(1,1,1);
		go5.tag = "C1";
		var go6 = GameObject.FindWithTag("C4").GetComponent<RectTransform>();
		go6.localScale = new Vector3(1,1,1);
		go6.tag = "C1";
		var go7 = GameObject.FindWithTag("C4").GetComponent<RectTransform>();
		go7.localScale = new Vector3(1,1,1);
		go7.tag = "C1";
		var go8 = GameObject.FindWithTag("C4").GetComponent<RectTransform>();
		go8.localScale = new Vector3(1,1,1);
		go8.tag = "C1";
		var go = GameObject.FindWithTag("C").GetComponent<Text>();
		go.text = "Pause/Play"; 
		//Create your own taps for the misses
	}
	
	public void Pause()
	{
		foreach (var t in FindObjectsOfType<Text>())
		{
			if (t.text == "Choose Dance Options:")
			{
				Destroy(t.gameObject);
			}
		}
		if (!started)
		{
			started = true;
			//FindObjectOfType<LeftTrackQueue>().Create();
			//FindObjectOfType<RightTrackQueue>().Create();
			foreach (var b in FindObjectsOfType<Button>())
			{
				if (b.tag == "MainMenu")
				{
					Destroy(b.gameObject);
				}
			}
		}
		var go = GameObject.FindWithTag("Pause");
		foreach (var o in FindObjectsOfType<Symbol>())
		{
			o.ToggleVelocity();
		}
		if (go.GetComponentInChildren<Text>().text == "Pause")
		{
			go.GetComponentInChildren<Text>().text = "Resume";
		}
		else
		{
			go.GetComponentInChildren<Text>().text = "Pause";
		}
	}

	public void PauseFB()
	{
		foreach (var o in FindObjectsOfType<FeedbackSymbol>())
		{
			o.ToggleVelocity();
		}
		/*
		var go = GameObject.FindWithTag("C").GetComponent<Text>();
		if (go.text == "Resume")
		{
			go.GetComponent<Text>().text = "Pause";
		}
		else
		{
			go.GetComponent<Text>().text = "Resume";
		}*/
	}
	
	public void HandleInput()
	{
		if (Input.GetKeyDown("left"))
		{
			LeftTrackInputT();
		}
		if (Input.GetKeyDown("down"))
		{
			LeftTrackInputH();
		}
		if (Input.GetKeyDown("right"))
		{
			RightTrackInputT();
		}
		if (Input.GetKeyDown("up"))
		{
			RightTrackInputH();
		}
	}

	public void LeftTrackInputT()
	{
		float minDistance = float.PositiveInfinity;
		float distance;
		float posY;
		Symbol closest = null;
		foreach (var o in FindObjectsOfType<ToeSymbol>())
		{
			if (o.GetComponent<Rigidbody2D>().position.x < -2f && o.GetComponent<Symbol>().activated == true)
			{
				posY = o.GetComponent<Rigidbody2D>().position.y;
				distance = Mathf.Abs(posY - (-3f));
				if (distance < minDistance && o.GetComponent<Symbol>().correct == false)
				{
					closest = o.GetComponent<Symbol>();
					minDistance = distance;
				}
			}
		}
		totalCount++;
		if (closest != null)
		{
			closest.correct = true;
			closest.state = 2;
			correctCount++;
			Text t = GameObject.FindWithTag("LeftTrackFeedback").GetComponent<Text>();
			GameObject.FindWithTag("LeftTrackFeedback").GetComponent<FeedbackText>().changed = true;
			if (closest.GetComponent<Rigidbody2D>().position.y > (-2.7f))
			{
				t.text = "Slightly Early!";
				closest.result = 1;
				FindObjectOfType<LeftTrackQueue>().AddFeeback(4, FindObjectOfType<Runner>().GetComponent<Rigidbody2D>().position.y, "toe");
			}
			else if (closest.GetComponent<Rigidbody2D>().position.y < (-3.3f))
			{
				t.text = "Slightly Late!";
				closest.result = 3;
				FindObjectOfType<LeftTrackQueue>().AddFeeback(4, FindObjectOfType<Runner>().GetComponent<Rigidbody2D>().position.y, "toe");
			}
			else
			{
				t.text = "Excellent!";
				closest.result = 2;
			}
		}
		else
		{
			Instantiate(Resources.Load("Error"), new Vector3(-3, -3, .5f), new Quaternion());
			FindObjectOfType<LeftTrackQueue>().AddFeeback(4, FindObjectOfType<Runner>().GetComponent<Rigidbody2D>().position.y, "toe");
		}
	}
	
	public void LeftTrackInputH()
	{
		float minDistance = float.PositiveInfinity;
		float distance;
		float posY;
		Symbol closest = null;
		foreach (var o in FindObjectsOfType<HeelSymbol>())
		{
			if (o.GetComponent<Rigidbody2D>().position.x < -2f && o.GetComponent<Symbol>().activated == true)
			{
				posY = o.GetComponent<Rigidbody2D>().position.y;
				distance = Mathf.Abs(posY - (-3f));
				if (distance < minDistance && o.GetComponent<Symbol>().correct == false)
				{
					closest = o.GetComponent<Symbol>();
					minDistance = distance;
				}
			}
		}
		totalCount++;
		if (closest != null)
		{
			closest.correct = true;
			closest.state = 2;
			correctCount++;
			Text t = GameObject.FindWithTag("LeftTrackFeedback").GetComponent<Text>();
			GameObject.FindWithTag("LeftTrackFeedback").GetComponent<FeedbackText>().changed = true;
			if (closest.GetComponent<Rigidbody2D>().position.y > (-2.7f))
			{
				t.text = "Slightly Early!";
				closest.result = 1;
				FindObjectOfType<LeftTrackQueue>().AddFeeback(4, FindObjectOfType<Runner>().GetComponent<Rigidbody2D>().position.y, "heel");
			}
			else if (closest.GetComponent<Rigidbody2D>().position.y < (-3.3f))
			{
				t.text = "Slightly Late!";
				closest.result = 3;
				FindObjectOfType<LeftTrackQueue>().AddFeeback(4, FindObjectOfType<Runner>().GetComponent<Rigidbody2D>().position.y, "heel");
			}
			else
			{
				t.text = "Excellent!";
				closest.result = 2;
			}
		}
		else
		{
			Instantiate(Resources.Load("Error"), new Vector3(-3, -3, .5f), new Quaternion());
			FindObjectOfType<LeftTrackQueue>().AddFeeback(4, FindObjectOfType<Runner>().GetComponent<Rigidbody2D>().position.y, "heel");
		}
	}
	
	
	
	public void RightTrackInputT()
	{
		float minDistance = float.PositiveInfinity;
		float distance;
		float posY;
		Symbol closest = null;
		foreach (var o in FindObjectsOfType<ToeSymbol>())
		{
			if (o.GetComponent<Rigidbody2D>().position.x > 2f && o.GetComponent<Symbol>().activated == true)
			{
				posY = o.GetComponent<Rigidbody2D>().position.y;
				distance = Mathf.Abs(posY - (-3f));
				if (distance < minDistance && o.GetComponent<Symbol>().correct == false)
				{
					closest = o.GetComponent<Symbol>();
					minDistance = distance;
				}
			}
		}
		totalCount++;
		if (closest != null)
		{
			closest.correct = true;
			closest.state = 2;
			correctCount++;
			Text t = GameObject.FindWithTag("RightTrackFeedback").GetComponent<Text>();
			GameObject.FindWithTag("RightTrackFeedback").GetComponent<FeedbackText>().changed = true;
			if (closest.GetComponent<Rigidbody2D>().position.y > (-2.7f))
			{
				t.text = "Slightly Early!";
				FindObjectOfType<RightTrackQueue>().AddFeeback(4, FindObjectOfType<Runner>().GetComponent<Rigidbody2D>().position.y, "toe");
				closest.result = 1;
			}
			else if (closest.GetComponent<Rigidbody2D>().position.y < (-3.3f))
			{
				t.text = "Slightly Late!";
				FindObjectOfType<RightTrackQueue>().AddFeeback(4, FindObjectOfType<Runner>().GetComponent<Rigidbody2D>().position.y, "toe");
				closest.result = 3;
			}
			else
			{
				t.text = "Excellent!";
				closest.result = 2;		
			}
				
		}
		else
		{
			Instantiate(Resources.Load("Error"), new Vector3(3, -3, .5f), new Quaternion());
			FindObjectOfType<RightTrackQueue>().AddFeeback(4, FindObjectOfType<Runner>().GetComponent<Rigidbody2D>().position.y, "toe");
		}
	}
	
	public void RightTrackInputH()
	{
		float minDistance = float.PositiveInfinity;
		float distance;
		float posY;
		Symbol closest = null;
		foreach (var o in FindObjectsOfType<HeelSymbol>())
		{
			if (o.GetComponent<Rigidbody2D>().position.x > 2f && o.GetComponent<Symbol>().activated == true)
			{
				posY = o.GetComponent<Rigidbody2D>().position.y;
				distance = Mathf.Abs(posY - (-3f));
				if (distance < minDistance && o.GetComponent<Symbol>().correct == false)
				{
					closest = o.GetComponent<Symbol>();
					minDistance = distance;
				}
			}
		}
		totalCount++;
		if (closest != null)
		{
			closest.correct = true;
			closest.state = 2;
			correctCount++;
			Text t = GameObject.FindWithTag("RightTrackFeedback").GetComponent<Text>();
			GameObject.FindWithTag("RightTrackFeedback").GetComponent<FeedbackText>().changed = true;
			if (closest.GetComponent<Rigidbody2D>().position.y > (-2.7f))
			{
				t.text = "Slightly Early!";
				FindObjectOfType<RightTrackQueue>().AddFeeback(4, FindObjectOfType<Runner>().GetComponent<Rigidbody2D>().position.y, "heel");
				closest.result = 1;
			}
			else if (closest.GetComponent<Rigidbody2D>().position.y < (-3.3f))
			{
				t.text = "Slightly Late!";
				FindObjectOfType<RightTrackQueue>().AddFeeback(4, FindObjectOfType<Runner>().GetComponent<Rigidbody2D>().position.y, "heel");
				closest.result = 3;
			}
			else
			{
				t.text = "Excellent!";
				closest.result = 2;		
			}
				
		}
		else
		{
			Instantiate(Resources.Load("Error"), new Vector3(3, -3, .5f), new Quaternion());
			FindObjectOfType<RightTrackQueue>().AddFeeback(4, FindObjectOfType<Runner>().GetComponent<Rigidbody2D>().position.y, "heel");
		}
	}

	public void AddOption1()
	{
		var go = GameObject.FindWithTag("Option1");
		var ti = go.GetComponent<TrackInfo>();
		var setText = GameObject.FindWithTag("Set");
		setText.GetComponent<Text>().text += "\n - Toe Tap";
		FindObjectOfType<LeftTrackQueue>().AddInfo(ti);
		FindObjectOfType<RightTrackQueue>().AddInfo(ti);
	}
	
	public void AddOption2()
	{
		var go = GameObject.FindWithTag("Option2");
		var ti = go.GetComponent<TrackInfo>();
		var setText = GameObject.FindWithTag("Set");
		setText.GetComponent<Text>().text += "\n - Heel Tap";
		FindObjectOfType<LeftTrackQueue>().AddInfo(ti);
		FindObjectOfType<RightTrackQueue>().AddInfo(ti);
	}
	
	public void AddOption3()
	{
		var go = GameObject.FindWithTag("Option3");
		var ti = go.GetComponent<TrackInfo>();
		var setText = GameObject.FindWithTag("Set");
		setText.GetComponent<Text>().text += "\n - Shuffle";
		FindObjectOfType<LeftTrackQueue>().AddInfo(ti);
		FindObjectOfType<RightTrackQueue>().AddInfo(ti);
	}

	public void Begin()
	{
		var go = GameObject.FindWithTag("Pause");
		go.GetComponent<Button>().interactable = true;
		go.GetComponent<Button>().GetComponentInChildren<Text>().text = "Start!";
		var go2 = GameObject.FindWithTag("Begin");
		Destroy(go2);
		FindObjectOfType<LeftTrackQueue>().Create();
		FindObjectOfType<RightTrackQueue>().Create();
	}

	public void DisplayOption1()
	{
		var go = GameObject.Instantiate(Resources.Load("opt1"), new Vector3(0, 0, 0), new Quaternion()) as GameObject;
		go.GetComponent<VideoPlayer>().targetCamera = Camera.main;
	}
	public void DisplayOption2()
	{
		var go = GameObject.Instantiate(Resources.Load("opt2"), new Vector3(0, 0, 0), new Quaternion()) as GameObject;
		go.GetComponent<VideoPlayer>().targetCamera = Camera.main;
		
	}
	public void DisplayOption3()
	{
		var go = GameObject.Instantiate(Resources.Load("opt3"), new Vector3(0, 0, 0), new Quaternion()) as GameObject;
		go.GetComponent<VideoPlayer>().targetCamera = Camera.main;
		
	}
}
