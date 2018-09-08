
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;

public class convidar_oponente : MonoBehaviour
{


	void Start()
	{
		network_socket.Conectar_Master ();
	}




}