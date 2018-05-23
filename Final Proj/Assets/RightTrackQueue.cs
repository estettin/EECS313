using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightTrackQueue : MonoBehaviour {

	public float yLast;
	public List<TrackInfoClone> l;
	public List<FeedbackInfo> fil;
	
	// Use this for initialization
	void Start () {
		l = new List<TrackInfoClone>();
		fil = new List<FeedbackInfo>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AddInfo(TrackInfo ti)
	{
		TrackInfoClone newTrackInfo = new TrackInfoClone();

		for (int j = 0; j < ti.positionListRight.Count; j++)
		{
			newTrackInfo.positionListRight.Add(ti.positionListRight[j]);
			newTrackInfo.stringListRight.Add(ti.stringListRight[j]);
		}
		
		if (l.Count == 0)
		{
			l.Add(newTrackInfo);
			yLast = newTrackInfo.positionListRight[newTrackInfo.positionListRight.Count - 1];
		}
		else
		{
			for (int i = 0; i < newTrackInfo.positionListRight.Count; i++)
			{
				newTrackInfo.positionListRight[i] += yLast;
			}
			yLast = newTrackInfo.positionListRight[newTrackInfo.positionListRight.Count - 1];
			l.Add(newTrackInfo);
			
		}
		
	}
	
	public void Create()
	{
		foreach (var ti in l)
		{
			int size = ti.stringListRight.Count;
			var _toe = Resources.Load("ToeSymbol");
			var _heel = Resources.Load("HeelSymbol");
			for (int i = 0; i < size; i++)
			{
				FindObjectOfType<Player>().symbolCounter++;
				if (ti.stringListRight[i] == "toe")
				{
					var go = Instantiate(_toe, new Vector2(3, ti.positionListRight[i]), new Quaternion()) as GameObject;
					go.GetComponent<Symbol>().startingPosition = ti.positionListRight[i];
				}
				else
				{
					var go = Instantiate(_heel, new Vector2(3, ti.positionListRight[i]), new Quaternion()) as GameObject;
					go.GetComponent<Symbol>().startingPosition = ti.positionListRight[i];
				}
				
			}
		}
	}

	public void AddFeeback(int mode, float posy, string type)
	{
		var fi = new FeedbackInfo();
		fi.startingPosition = posy;
		fi.mode = mode;
		fi.type = type;
		fil.Add(fi);
	}
	
	public void CreateFeedback()
	{
		var _toe = Resources.Load("ToeFeed");
		var _heel = Resources.Load("HeelFeed");
		
		foreach (var fi in fil)
		{
			Object pre;
			if (fi.type == "toe")
			{
				pre = _toe;
			}
			else
			{
				pre = _heel;
			}
			switch (fi.mode) 
			{
				case 0:
					var go1 = Instantiate(pre, new Vector2(3, fi.startingPosition), new Quaternion()) as GameObject;
					go1.GetComponent<FeedbackSymbol>().GetComponent<SpriteRenderer>().color = Color.red;
					break;
				case 1:
					var go2 = Instantiate(pre, new Vector2(3, fi.startingPosition), new Quaternion()) as GameObject;
					go2.GetComponent<FeedbackSymbol>().GetComponent<SpriteRenderer>().color = Color.yellow;
					break;
				case 2:
					var go3 = Instantiate(pre, new Vector2(3, fi.startingPosition), new Quaternion()) as GameObject;
					go3.GetComponent<FeedbackSymbol>().GetComponent<SpriteRenderer>().color = Color.green;
					break;
				case 3:
					var go4 = Instantiate(pre, new Vector2(3, fi.startingPosition), new Quaternion()) as GameObject;
					go4.GetComponent<FeedbackSymbol>().GetComponent<SpriteRenderer>().color = Color.yellow;
					break;
				case 4:
					var go5 = Instantiate(pre, new Vector2(5, fi.startingPosition), new Quaternion()) as GameObject;
					go5.GetComponent<FeedbackSymbol>().GetComponent<SpriteRenderer>().color = Color.blue;
					break;
			}
		}
	}
	
	public class FeedbackInfo
	{
		public float startingPosition;
		public int mode;
		public string type;
	}
	
	public class TrackInfoClone
	{
		public List<string> stringListLeft = new List<string>();
		public List<float> positionListLeft = new List<float>();
	
		public List<string> stringListRight = new List<string>();
		public List<float> positionListRight = new List<float>();
	}
}
