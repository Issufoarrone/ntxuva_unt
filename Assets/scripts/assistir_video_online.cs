using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class assistir_video_online : MonoBehaviour {


	public void ShowRewardedAd()
	{


		GameObject Obj = Instantiate(Resources.Load("espera")) as GameObject;;
		Obj.transform.SetParent(GameObject.Find ("Canvas").transform);
		Obj.transform.localPosition = GameObject.Find ("Assistir_Video_Btm").transform.localPosition;
		Obj.transform.localScale = Vector3.one;

		GameObject.Find ("Assistir_Video_Btm").SetActive (false);

		if (Advertisement.IsReady("rewardedVideo"))
		{
			var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show("rewardedVideo", options);
		}

	}

	private void HandleShowResult(ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
				
				Debug.Log ("The ad was successfully shown.");
				
				PlayerPrefs.SetInt ("Jogadas_Online",PlayerPrefs.GetInt ("Jogadas_Online")+2);
				
				if (PlayerPrefs.GetInt ("Jogadas_Online") == 1) {
					GameObject.Find ("Jogadas_Online").GetComponent<Text> ().text = PlayerPrefs.GetInt("Jogadas_Online")+" Jogada";
				} else {
					GameObject.Find ("Jogadas_Online").GetComponent<Text> ().text = PlayerPrefs.GetInt("Jogadas_Online")+" Jogadas";
				}
				
				GameObject.Find ("Nao_Tem_Jogadas_Online(Clone)").SetActive (false);
				GameObject.Find ("espera(Clone)").SetActive (false);

				GameObject Obj = Instantiate(Resources.Load("Ganhou_Jogadas_Online")) as GameObject;;
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