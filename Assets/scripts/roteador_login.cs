using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class roteador_login : MonoBehaviour {

	// Use this for initialization
	void Start () {

		if(PlayerPrefs.GetString("Nome")!="")
		{
				SceneManager.LoadScene("home");
		}
	}
	

}
