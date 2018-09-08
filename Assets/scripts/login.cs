using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class login : MonoBehaviour {


	public void Logar()
	{
		
		string Nick_Name = GameObject.Find ("Nick_Name").GetComponent<InputField> ().text;

		if (Nick_Name != "") {

			PlayerPrefs.SetString ("som", "on");
			PlayerPrefs.SetInt ("Jogadas_Offline", 10);
			PlayerPrefs.SetInt ("Jogadas_Online", 5);
			PlayerPrefs.SetInt ("Vitorias_Offline", 0);
			PlayerPrefs.SetInt ("Derotas_Offline", 0);
			PlayerPrefs.SetInt ("Vitorias_Online", 0);
			PlayerPrefs.SetInt ("Derotas_Online", 0);
			PlayerPrefs.SetString("Nome", Nick_Name);
			SceneManager.LoadScene("home");


		} else {
			GameObject.Find ("Login_Alerta").GetComponent<Text> ().text = "campo obrigatório";
		}

	}





	/*
	// remember to use StartCoroutine when calling this function!
	IEnumerator PostScores(string name, int score)
	{
		//This connects to a server side php script that will add the name and score to a MySQL DB.
		// Supply it with a string representing the players name and the players score.
		string hash = MD5Test.Md5Sum(name + score + secretKey);

		string post_url = addScoreURL + "name=" + WWW.EscapeURL(name) + "&score=" + score + "&hash=" + hash;

		// Post the URL to the site and create a download object to get the result.
		WWW hs_post = new WWW(post_url);
		yield return hs_post; // Wait until the download is done

		if (hs_post.error != null)
		{
			print("There was an error posting the high score: " + hs_post.error);
		}
	}
	*/
	// Get the scores from the MySQL DB to display in a GUIText.
	// remember to use StartCoroutine when calling this function!
	IEnumerator login_get(string Email, string Senha)
	{
		//gameObject.guiText.text = "Loading Scores";
	
		WWW hs_get = new WWW("http://ntxuva.taduma.co.mz/index.php?action=login&email="+Email+"&senha="+Senha);
		//http://localhost:8888/ntxuva_mobile
		yield return hs_get;

		if (hs_get.error != null)
		{
			print("Erro de conexao: " + hs_get.error);
		}
		else
		{


			Login_Respose myObject = JsonUtility.FromJson<Login_Respose>(hs_get.text);

			if (myObject.status=="sucesso") {
				PlayerPrefs.SetString("Nome", myObject.nome);
				SceneManager.LoadScene("oponentes");
			}

			if (myObject.status=="erro") {
				GameObject.Find ("Login_Alerta").GetComponent<Text> ().text = "Dados Errados";
			}
		
		}
	

	
	}


}

[Serializable]
public class Login_Respose
{
	public string status;
	public string nome;
}
