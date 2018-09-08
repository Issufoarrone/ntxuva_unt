using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class convite : MonoBehaviour {


	public void Convite()
	{
		
		string Convinte_Text = GameObject.Find ("Convite_Text").GetComponent<InputField> ().text;

		if (Convinte_Text != "") {
			//StartCoroutine (login_get (Email, Senha));
		} else {
			GameObject.Find ("Convite_Alerta").GetComponent<Text> ().text = "Campo Obrigatório";
		}

	}


}
