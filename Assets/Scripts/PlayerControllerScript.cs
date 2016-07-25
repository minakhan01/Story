using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour {

	Animator animator;
	public int globalEnergy = 100;
	public int globalCalmness = 100;
	public int prevGlobalCalmness = 100;
	public int globalFocus = 100;
	public int prevGlobalFocus = 100;
	public GameObject[] backgrounds;
	public GameObject lushBackground;
	public GameObject[] solidBackgrounds;

	// Use this for initialization
	void Start () {

		backgrounds = GameObject.FindGameObjectsWithTag("Background");
		setSelfSlowBackgroundSpeeds();
		
		globalCalmness = (int) GameObject.Find ("CalmnessSlider").GetComponent <Slider>().value;
		globalEnergy = (int) GameObject.Find ("EnergySlider").GetComponent <Slider>().value;
		globalFocus = (int) GameObject.Find ("FocusSlider").GetComponent <Slider>().value;

		// update speed for fog and clouds
		foreach (GameObject background in backgrounds) {
			background.transform.GetComponent<Scroller>().SetSpeedRank(1);
		}
			
		animator = GetComponent<Animator>();
		animator.SetFloat ("energy", globalEnergy);

		GameObject.Find ("Backgrounds").GetComponent<BackgroundController> ().updateCalmnessScore (globalCalmness);
		GameObject.Find ("Zombie").GetComponent<ZombieController> ().updateMentalScore(globalFocus);

		solidBackgrounds = new GameObject[4];
		setSolidBackgrounds ();
		updateBackgroundSpeed ();
	}

	void setSelfSlowBackgroundSpeeds() {
		GameObject.Find("Fog").transform.GetComponent<Scroller>().SetSelfSlow(0.1f);
		GameObject.Find("Cloud").transform.GetComponent<Scroller>().SetSelfSlow(0.1f);
		GameObject.Find("Front_New").transform.GetComponent<Scroller>().SetSelfSlow(0.5f);
		GameObject.Find("Front_New_Lush").transform.GetComponent<Scroller>().SetSelfSlow(0.5f);
		GameObject.Find("Fog_Middle").transform.GetComponent<Scroller>().SetSelfSlow(1.5f);
		GameObject.Find("Middle_New").transform.GetComponent<Scroller>().SetSelfSlow(20f);
		GameObject.Find("Middle_New_Lush").transform.GetComponent<Scroller>().SetSelfSlow(20f);
	}

	void setSolidBackgrounds() {
		solidBackgrounds[0] = GameObject.Find("Middle_New_Lush");
		solidBackgrounds[1] = GameObject.Find("Front_New_Lush");
		solidBackgrounds[2] = GameObject.Find("Middle_New");
		solidBackgrounds[3] = GameObject.Find("Front_New");
	}

	public void updateBackgroundSpeed() {
		//todo: move to findSpeed function
		int speedRank = 0;
		if (globalEnergy > 80) {
			speedRank = 4;
		} else if (globalEnergy > 60) {
			speedRank = 2;
		}
		else if (globalEnergy > 40) {
			speedRank = 1;
		}
		else {
			speedRank = 0;
		}

		foreach (GameObject background in solidBackgrounds) {
			background.transform.GetComponent<Scroller>().SetSpeedRank(speedRank);
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void calmnessUpdated() {
		prevGlobalCalmness = globalCalmness;
		globalCalmness = (int) GameObject.Find ("CalmnessSlider").GetComponent <Slider>().value;
		Debug.Log ("updating calmness score");
		GameObject.Find ("Backgrounds").GetComponent<BackgroundController> ().updateCalmnessScore (globalCalmness);
	}

	public void focusUpdated() {
		prevGlobalFocus = globalFocus;
		globalFocus = (int) GameObject.Find ("FocusSlider").GetComponent <Slider>().value;
		GameObject.Find ("Zombie").GetComponent<ZombieController> ().updateMentalScore(globalFocus);
	}

	public void energyUpdated() {
		globalEnergy = (int) GameObject.Find ("EnergySlider").GetComponent <Slider>().value;
		animator.SetFloat("energy",globalEnergy);
		updateBackgroundSpeed ();
	}
}
