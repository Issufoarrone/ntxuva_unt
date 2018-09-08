using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class home : MonoBehaviour {

	// Use this for initialization
	void Start () {


		if (PlayerPrefs.GetInt ("Jogadas_Offline") == 1) {
			GameObject.Find ("Jogadas_Offline").GetComponent<Text> ().text = PlayerPrefs.GetInt("Jogadas_Offline")+" Jogada";
		} else {
			GameObject.Find ("Jogadas_Offline").GetComponent<Text> ().text = PlayerPrefs.GetInt("Jogadas_Offline")+" Jogadas";
		}


		if (PlayerPrefs.GetInt ("Jogadas_Online") == 1) {
			GameObject.Find ("Jogadas_Online").GetComponent<Text> ().text = PlayerPrefs.GetInt("Jogadas_Online")+" Jogada";
		} else {
			GameObject.Find ("Jogadas_Online").GetComponent<Text> ().text = PlayerPrefs.GetInt("Jogadas_Online")+" Jogadas";
		}

	}
	

}
