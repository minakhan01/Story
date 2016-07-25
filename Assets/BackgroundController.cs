using UnityEngine;
using System.Collections;

public class BackgroundController : MonoBehaviour {

	public GameObject[] backgroundLayers = new GameObject[4];
	public bool[] isFaded = new bool[4];
	public float calmnessLevel;

	float fadeOut = 1;
	float fadeIn = 0;

	float increment = 0.01f;

	public int previousMaxIndex;
	public int currentMaxIndex = -1;

	public bool fadingOut = false;
	public bool fadingIn = false;

	public float minTransparency = 0.1f;

	public int currentFadingLayer = -1;

	// Use this for initialization
	void Start () {
		currentMaxIndex = -1;
			
		backgroundLayers[0] = GameObject.Find("Middle_New");
		backgroundLayers[1] = GameObject.Find("Front_New");
		backgroundLayers[2] = GameObject.Find("Middle_New_Lush");
		backgroundLayers[3] = GameObject.Find("Front_New_Lush");

		foreach (GameObject backgroundLayer in backgroundLayers) {
			Color prevColor = backgroundLayer.transform.GetComponent<Renderer> ().material.color;
			backgroundLayer.transform.GetComponent<Renderer>().material.color = new Color(prevColor.r, prevColor.g, prevColor.b, 0f);
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (fadingIn) {
			fadeInLayer(currentFadingLayer);
			if (currentFadingLayer > currentMaxIndex) {
				fadingIn = false;
			}
		}

		if (fadingOut) {
			fadeOutLayer(currentFadingLayer);
			if (currentFadingLayer == currentMaxIndex) {
				fadingOut = false;
			}
		}
	
	}

	void fadeInLayer(int layerNumber) {
		if (fadeIn < 1) {
			if (layerNumber >= 0) {
				fadeIn += increment;
				Debug.Log ("current fading in layer: " + layerNumber + " with alpha: " + fadeIn);
				backgroundLayers [layerNumber].transform.GetComponent<Renderer> ().material.color = new Color (1f, 1f, 1f, fadeIn);
				if (fadeIn >= 1) {
					Debug.Log ("completely faded in");
					currentFadingLayer++;
					fadeIn = minTransparency;
				}
			} 
		}
	}

	void fadeOutLayer(int layerNumber) {
		if (fadeOut > 0 && layerNumber >= 0) {
			fadeOut -= increment;
			Debug.Log ("current fading out layer: "+ layerNumber + " with alpha: " +fadeOut);
			backgroundLayers[layerNumber].transform.GetComponent<Renderer>().material.color = new Color(1f,1f,1f,fadeOut);
			if (fadeOut <= minTransparency) {
				Debug.Log ("completely faded out");
				currentFadingLayer--;
				fadeOut = 1;
			}
		}
	}

	int findMaxLayerInt() {
		if (calmnessLevel > 80) {
			return 3;
		} else if (calmnessLevel > 60) {
			return 2;
		} else if (calmnessLevel > 40) {
			return 1;
		} else if (calmnessLevel > 20) {
			return 0;
		} else {
			return -1;
		}
	}

	public void updateCalmnessScore( int calmnessScore) {
		calmnessLevel = calmnessScore;
		previousMaxIndex = currentMaxIndex;
		currentMaxIndex = findMaxLayerInt();
		if (currentMaxIndex < previousMaxIndex) {
			fadingOut = true;
			currentFadingLayer = previousMaxIndex;
		} else if (currentMaxIndex > previousMaxIndex) {
			fadingIn = true;
			currentFadingLayer = previousMaxIndex + 1;
		}
	}
}
