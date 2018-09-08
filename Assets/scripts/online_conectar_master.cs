using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using UnityEngine.UI;


public class online_conectar_master : MonoBehaviour {


	void Start()
	{
		GameObject Quem_Somos = Instantiate(Resources.Load("espera")) as GameObject;;
		Quem_Somos.transform.SetParent(GameObject.Find ("Canvas").transform);
		Quem_Somos.transform.localPosition = Vector3.zero;
		Quem_Somos.transform.localScale = Vector3.one;

		Online_.Listar_Jogadores_Disponiveis_Static ();
		StartCoroutine ( Conectar_Master());
	}

	public  IEnumerator Conectar_Master()
	{
		//while (true) {
		yield return new WaitForSeconds (10f);
		network_socket.Conectar_Master ();

		//}

	}
}
