using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controle_som : MonoBehaviour {

	// Use this for initialization
	public void Mudar_Som_Status () {

		if (PlayerPrefs.GetString ("som") == "on") {
			PlayerPrefs.SetString ("som", "off");
			GameObject.Find ("btm_som").GetComponent<Image> ().sprite = Resources.Load ("sem_som", typeof(Sprite)) as Sprite;

		} else {
			PlayerPrefs.SetString ("som", "on");
			GameObject.Find ("btm_som").GetComponent<Image> ().sprite = Resources.Load ("com_som", typeof(Sprite)) as Sprite;

		}
		
	}

}
