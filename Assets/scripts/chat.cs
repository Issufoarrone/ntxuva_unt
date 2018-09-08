
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class chat : MonoBehaviour
{

	

	public void Enviar_Msg()
	{
		//Enviar Comando
		//Enviar_Msg
		network_socket.Enviar_Mensagem_Network ("enviar_mensagem_chat", GameObject.Find ("fala_1_text").GetComponent<Text> ().text);
		GameObject.Find ("fala_1").GetComponentInChildren<InputField>().text = "";

	}


	public void Escrevendo_Msg()
	{
		//Enviar Comando
		//Escrevendo_Msg
		network_socket.Enviar_Mensagem_Network ("escrevendo_mensagem_chat", "");
	}


}