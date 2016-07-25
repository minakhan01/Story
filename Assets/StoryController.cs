using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StoryController : MonoBehaviour {
	
	public bool moviePlaying = true;
	public int currentTimeIndex = 0;
	public int numTimeIndices = 12;
	public int[] calmnessValues;
	public int[] energyValues;
	public int[] focusValues;

	// Use this for initialization
	void Start () {
		calmnessValues = new int[numTimeIndices];
		focusValues = new int[numTimeIndices];
		energyValues = new int[numTimeIndices];
		populateRandomValues ();
		InvokeRepeating ("updateStoryValues", 0, 15);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void populateRandomValues() {
		calmnessValues[0] = 100;
		focusValues[0] = 100;
		energyValues[0] = 100;
		for (int i = 1; i < numTimeIndices; i++) {
			calmnessValues[i] = Random.Range(0, 100);
			focusValues[i] = Random.Range(0, 100);
			energyValues[i] = Random.Range(0, 100);
		}
	}

//	IEnumerator startGameCountdown(float time)
//	{
//		while (currentTimeIndex < numTimeIndices) {
//			updateStoryValues ();
//			yield return new WaitForSeconds(time);
//		}
//	}

	void updateStoryValues() {
		GameObject.Find ("CalmnessSlider").GetComponent <Slider> ().value = calmnessValues [currentTimeIndex];
		GameObject.Find ("EnergySlider").GetComponent <Slider> ().value = energyValues [currentTimeIndex];
		GameObject.Find ("FocusSlider").GetComponent <Slider> ().value = focusValues [currentTimeIndex];
		GameObject.Find ("TimeText").GetComponent <Text> ().text = ""+ calculateCurrentTime();

		GameObject.Find ("character").GetComponent<PlayerControllerScript> ().calmnessUpdated ();
		GameObject.Find ("character").GetComponent<PlayerControllerScript> ().focusUpdated ();
		GameObject.Find ("character").GetComponent<PlayerControllerScript> ().energyUpdated ();

		currentTimeIndex++;
	}

	string calculateCurrentTime() {
		int hourOffset = 8;
		int hour = currentTimeIndex;
		int finalHour = hour + hourOffset;
		string hourString = "";
		if (finalHour < 10) {
			hourString += "0";
		}
		hourString += finalHour;
		string minutesString = "00";
		string amPm = "";
		if (finalHour < 12) {
			amPm += "am";
		} else {
			amPm = "pm";
		}
		return hourString + ":" + minutesString + " " + amPm;
	}
}
