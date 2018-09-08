using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using UnityEngine.SceneManagement;

public class LeaveRoom : MonoBehaviour {


	public void Leave_Room()
	{
		//convidar_oponente.m_NetworkMatch.DestroyMatch (convidar_oponente.convidar_oponente.m_MatchInfo.networkId,0,convidar_oponente.OnDestroyMatch);
		network_socket.Desconectar_Servidor();
	}








}
