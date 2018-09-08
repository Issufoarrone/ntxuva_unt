using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class Online_: MonoBehaviour {

	public GameObject ItemTemplate2;
	public GameObject content2;
	public static GameObject ItemTemplate;
	public static GameObject content;

	void Start()
	{

		ItemTemplate = ItemTemplate2;
		content = content2;
	}


	public static void  Listar_Jogadores_Disponiveis_Static()
	{
		Debug.Log ("Listar Jogadores...");

		network_socket.m_NetworkMatch.ListMatches (0, 10, "", true, 0, 0, OnMatchList_Jogadores_Disponiveis);
	}


	public void  Listar_Jogadores_Disponiveis()
	{
		if (GameObject.Find ("espera(Clone)") != null) {
			GameObject.Find ("espera(Clone)").SetActive (false);
		}

		GameObject Quem_Somos = Instantiate(Resources.Load("espera")) as GameObject;;
		Quem_Somos.transform.SetParent(GameObject.Find ("Canvas").transform);
		Quem_Somos.transform.localPosition = Vector3.zero;
		Quem_Somos.transform.localScale = Vector3.one;

		foreach (Transform child in content.transform)
		{
			Destroy(child.gameObject);
		}

		Debug.Log ("Listar Jogadores...");
		network_socket.m_NetworkMatch.ListMatches (0, 10, "", true, 0, 0, OnMatchList_Jogadores_Disponiveis);

	}




	public static void OnMatchList_Jogadores_Disponiveis(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
	{


		if (success && matches != null)
		{

			int cont = 0;

			foreach (var match in matches)
			{
				

				if(match.currentSize==1)
				{

				
					if (match.name != PlayerPrefs.GetString ("Nome")) {
						

						var copy = Instantiate (ItemTemplate);
						copy.transform.parent = content.transform;
						copy.transform.localScale = Vector3.one;


						foreach (Text component in  copy.GetComponentsInChildren<Text> ()) {
							if (component.name == "Nick_Name") {
								component.text = match.name;
							}
						}


						foreach (Button component in  copy.GetComponentsInChildren<Button> ()) {

							if (component.name == "jogar_btm") {

								component.onClick.AddListener (delegate {
									OnClick_Jogar_Btm (match.name);
								});

							}
						}

						GameObject.Find ("espera(Clone)").SetActive (false);

						cont++;
					}
				}

			}

			if (cont == 0) {
				Debug.Log("Nao ha jogadores disponiveis...");

				Listar_Jogadores_Disponiveis_Static ();
			}




		}
		else if(!success)
		{
			Debug.Log("Nao foi possivel conectar ao servidor");
			Listar_Jogadores_Disponiveis_Static ();
			//Debug.LogError("List match failed: " + extendedInfo);
		}

	}

	static void OnClick_Jogar_Btm(string room_name)
	{
		network_socket.Aceitar_Convinte (room_name);
	}

}
