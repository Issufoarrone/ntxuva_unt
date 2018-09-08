using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using UnityEngine.Events;

public class assistir_video_offline : MonoBehaviour {


	public void ShowRewardedAd()
	{


		GameObject Obj = Instantiate(Resources.Load("espera")) as GameObject;;
		Obj.transform.SetParent(GameObject.Find ("Canvas").transform);
		Obj.transform.localPosition = GameObject.Find ("Assistir_Video_Btm").transform.localPosition;
		Obj.transform.localScale = Vector3.one;

		GameObject.Find ("Assistir_Video_Btm").SetActive (false);
			
		if (Advertisement.IsReady ("rewardedVideo")) {
			var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show ("rewardedVideo", options);
		} else {
			GameObject.Find ("Aviso").GetComponent<Text> ().text = "Não foi possível carregar o vídeo.\n  tente mais tarde";
			GameObject.Find ("espera(Clone)").SetActive (false);
			GameObject.Find ("som_erro").GetComponent<AudioSource> ().Play ();
		}
		

	}

	private void HandleShowResult(ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
				
				Debug.Log ("The ad was successfully shown.");
				
				PlayerPrefs.SetInt ("Jogadas_Offline",PlayerPrefs.GetInt ("Jogadas_Offline")+5);
				
				if (PlayerPrefs.GetInt ("Jogadas_Offline") == 1) {
					GameObject.Find ("Jogadas_Offline").GetComponent<Text> ().text = PlayerPrefs.GetInt("Jogadas_Offline")+" Jogada";
				} else {
					GameObject.Find ("Jogadas_Offline").GetComponent<Text> ().text = PlayerPrefs.GetInt("Jogadas_Offline")+" Jogadas";
				}
				
				GameObject.Find ("Nao_Tem_Jogadas_Offline(Clone)").SetActive (false);
				GameObject.Find ("espera(Clone)").SetActive (false);

				GameObject Obj = Instantiate(Resources.Load("Ganhou_Jogadas_Offline")) as GameObject;;
				Obj.transform.SetParent(GameObject.Find ("Canvas").transform);
				Obj.transform.localPosition = Vector3.zero;
				Obj.transform.localScale = Vector3.one;

				GameObject.Find ("som_moedas").GetComponent<AudioSource> ().Play ();

				//
				// YOUR CODE TO REWARD THE GAMER
				// Give coins etc.
				break;
			case ShowResult.Skipped:
				Debug.Log ("The ad was skipped before reaching the end.");
				GameObject.Find ("Aviso").GetComponent<Text> ().text = "Você não assistiu o vídeo até o fim.\n  tente mais tarde";
				GameObject.Find ("espera(Clone)").SetActive (false);
				GameObject.Find ("som_erro").GetComponent<AudioSource> ().Play ();

				break;
			case ShowResult.Failed:
				Debug.LogError ("The ad failed to be shown.");
				GameObject.Find ("Aviso").GetComponent<Text> ().text = "Não foi possível abrir o vídeo.\n tente mais tarde"; 
				GameObject.Find ("espera(Clone)").SetActive (false);
				GameObject.Find ("som_erro").GetComponent<AudioSource> ().Play ();
				break;
		}
	}



}