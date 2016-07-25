using UnityEngine;
using System.Collections;

public class Scroller : MonoBehaviour {

	public float scrollSpeed;
	public float selfSlowParallax = 1f;
	private Vector2 savedOffset;

	void Start () {
		savedOffset = GetComponent<Renderer>().sharedMaterial.GetTextureOffset ("_MainTex");
	}

	void Update () {
		float x = Mathf.Repeat (Time.time * scrollSpeed, 1);
		Vector2 offset = new Vector2 (x, savedOffset.y);
		GetComponent<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex", offset);
	}

	void OnDisable () {
		GetComponent<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex", savedOffset);
	}

	public void SetSpeedRank(int speedRank) {
		scrollSpeed = speedRank / (50f * selfSlowParallax);
	}

	public void SetSelfSlow(float slowSpeed) {
		selfSlowParallax = slowSpeed;
	}
}