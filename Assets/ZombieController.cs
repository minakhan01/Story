using UnityEngine;
using System.Collections;

public class ZombieController : MonoBehaviour {

	public GameObject[] zombies = new GameObject[4];
	int currentZombie = -1;
	int prevZombie;
	float mentalAlertness = 100;
	bool fadingOut = false;
	bool fadingIn = false;
	float fadeOut = 1;
	float fadeIn = 0;

	// Use this for initialization
	void Start () {
		zombies[0] = GameObject.Find ("infantCharacter");
		zombies[1] = GameObject.Find ("babyCharacter");
		zombies[2] = GameObject.Find ("grownMidCharacter");
		zombies[3] = GameObject.Find ("grownupCharacter");
		fadeOutAll();
	}

	void fadeOutAll() {
		zombies[0].GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0);
		zombies[1].GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0);
		zombies[2].GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0);
		zombies[3].GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0);
	}

	// Update is called once per frame
	void Update () {
		float increment = 0.01f;
		if (fadingOut && fadeOut > 0) {
			if (prevZombie >= 0) {
				fadeOut -= increment;
				zombies [prevZombie].GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, fadeOut);
				if (fadeOut <= 0) {
					fadingOut = false;
					fadingIn = true;
					fadeOut = 1;
				}
			} else {
				fadingOut = false;
				fadingIn = true;
			}
		}
		if (fadingIn && fadeIn < 1 && currentZombie >= 0) {
			fadeIn += increment;
			zombies[currentZombie].GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,fadeIn);
			if (fadeIn >= 1) {
				fadingIn = false;
				fadeIn = 0;
			}
		}
	}

	int findZombieIndex() {
		if (mentalAlertness > 80) {
			return -1;
		} else if (mentalAlertness > 60) {
			return 0;
		} else if (mentalAlertness > 40) {
			return 1;
		} else if (mentalAlertness > 20) {
			return 2;
		} else {
			return 3;
		}
	}

	public void updateMentalScore( int mentalScore) {
//		fadeOutAll ();
		mentalAlertness = mentalScore;
		prevZombie = currentZombie;
		currentZombie = findZombieIndex();
		if (prevZombie != currentZombie) {
			fadingOut = true;
		}
	}
}
