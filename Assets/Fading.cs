using UnityEngine;
using System.Collections;

public class Fading : MonoBehaviour {

//	public float fadeSpeed = 1f;
//	public float fadeTime = 1f;
	public bool fadeIn = true;
	public float fade = 1;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
//				if (fadeIn) {
//					float fade = Mathf.SmoothDamp(1f,0f, ref fadeSpeed,fadeTime);
//				}
		//
		//		if (!fadeIn) {
		//			float Fade = Mathf.SmoothDamp(1f,0f,ref fadeSpeed,fadeTime);
		//			spriteTree.color = new Color(1f,1f,1f,Fade);
		//		}
		float increment = 0.01f;
		if (fadeIn && fade < 1) {
			fade += increment;
		}
		else if (!fadeIn && fade > 0) {
			fade -= increment;
		}
		GetComponent<Renderer>().material.color = new Color(1f,1f,1f,fade);
	}

//	IEnumerator FadeIn() {
//		for (float f = 0f; f <= 1; f += 0.001f) {
//			fade = f;
//			yield return null;
//		}
//	}
//
//	IEnumerator FadeOut() {
//		for (float f = 1f; f >= 0; f -= 0.001f) {
//			fade = f;
//			yield return null;
//		}
//	}

	public void SetCalmness(int calmnessRank) {
		if (calmnessRank > 0) {
			fadeIn = true;
		} else if (calmnessRank < 0) {
			fadeIn = false;
		}
	}
}
