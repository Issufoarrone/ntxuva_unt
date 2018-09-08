using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class online_sair : MonoBehaviour {

	// Use this for initialization
	public void Sair()
	{
		network_socket.Enviar_Mensagem_Network("oponente_saiu","");
		SceneManager.LoadScene("home");
	}

}
