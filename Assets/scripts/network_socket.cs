
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class network_socket : MonoBehaviour
{

	// Matchmaker related
	public static List<MatchInfoSnapshot> m_MatchList = new List<MatchInfoSnapshot>();
	public static bool m_MatchCreated;
	public static bool m_MatchJoined;
	public static MatchInfo m_MatchInfo;
	public static string m_MatchName = "NewRoom";
	public static NetworkMatch m_NetworkMatch;

	// Connection/communication related
	public static int m_HostId = -1;
	// On the server there will be multiple connections, on the client this will only contain one ID
	public static List<int> m_ConnectionIds = new List<int>();
	public static byte[] m_ReceiveBuffer;
	public static string m_NetworkMessage = "Hello world";
	public static string m_LastReceivedMessage = "";
	public static NetworkWriter m_Writer;
	public static NetworkReader m_Reader;
	public static bool m_ConnectionEstablished;

	const int k_ServerPort = 25000;
	const int k_MaxMessageSize = 65535;


	static string Codigo;


	void Awake()
	{
	
		Application.runInBackground = true;

		m_NetworkMatch = gameObject.AddComponent<NetworkMatch>();
		m_ReceiveBuffer = new byte[k_MaxMessageSize];
		m_Writer = new NetworkWriter();
	}


	public static void Conectar_Master()
	{

		int random_1 = Random.Range(0,9);
		int random_2 = Random.Range(0,9);
		int random_3 = Random.Range(0,9);
		int random_4 = Random.Range(0,9);
		int random_5 = Random.Range(0,9);

		Codigo = PlayerPrefs.GetString("Nome");

		PlayerPrefs.SetString("Codigo", Codigo);
		PlayerPrefs.SetInt ("Jogador_Master", 1);

		m_NetworkMatch.CreateMatch(Codigo, 2, true, "", "", "", 0, 0, OnMatchCreate);


	}


	public static void Aceitar_Convinte(string room_name)
	{

		string Codigo = GameObject.Find ("Convite_Codigo_Text").GetComponent<Text> ().text;
		PlayerPrefs.SetString("Codigo", Codigo);
		PlayerPrefs.SetInt ("Jogador_Master", 2);
		m_NetworkMatch.ListMatches(0, 20, room_name, true, 0, 0, OnMatchList_Aceitar_Convite);

	}


	public static void Enviar_Mensagem_Network(string Code,string Message)
	{

		SendStructure sendStructure = new SendStructure(Code, Message);

		MemoryStream memStream = new MemoryStream();
		IFormatter formatter = new BinaryFormatter();
		formatter.Serialize(memStream, sendStructure);

		//Array data to send over network
		byte[] serializedStructure = memStream.ToArray();

		//m_Writer.SeekZero();
		//m_Writer.Write(m_NetworkMessage);
		byte error;
		for (int i = 0; i < m_ConnectionIds.Count; ++i)
		{
			NetworkTransport.Send(m_HostId,
				m_ConnectionIds[i], 0, serializedStructure, serializedStructure.Length, out error);
			if ((NetworkError)error != NetworkError.Ok)
				Debug.Log("Failed to send message");
		}

	}

	[System.Serializable]
	public class SendStructure
	{
		public string Code;
		public string Message;

		public SendStructure(string Code, string Message)
		{
			this.Code = Code;
			this.Message = Message;
		}
	}


	void OnApplicationQuit()
	{
		NetworkTransport.Shutdown();
	}

	public static void Desconectar_Servidor()
	{
		m_NetworkMatch.DropConnection(m_MatchInfo.networkId,
			m_MatchInfo.nodeId, 0, OnConnectionDropped);
	}


	public static void OnMatchList_Aceitar_Convite(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
	{
		if (success && matches != null)
		{

			m_MatchList = matches;
			bool Cod_Valid = false;

			foreach (var match in m_MatchList)
			{

				//if (match.name==PlayerPrefs.GetString("Codigo"))
				//{
					Debug.Log("Codigo de convite valido");
					m_NetworkMatch.JoinMatch(match.networkId, "", "", "", 0, 0, OnMatchJoined);
					Cod_Valid = true;
					break;
				//}
			}

			if(Cod_Valid)
			{
				Debug.Log("Codigo de convite valido");
			}
			else
			{
				Debug.Log("Codigo de convite invalido");
				GameObject.Find ("Cod_Validator_Component").GetComponent<Text> ().text = "Código Inválido";
			}

		}
		else if(!success)
		{
			Debug.LogError("List match failed: " + extendedInfo);
		}
	}




	public static void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
	{
		if (success)
		{
			Debug.Log("Create match succeeded");
			Utility.SetAccessTokenForNetwork(matchInfo.networkId, matchInfo.accessToken);

			m_MatchCreated = true;
			m_MatchInfo = matchInfo;

			StartServer(matchInfo.address, matchInfo.port, matchInfo.networkId,
				matchInfo.nodeId);
		}
		else
		{
			Debug.LogError("Create match failed: " + extendedInfo);
		}
	}

	public static void OnMatchJoined(bool success, string extendedInfo, MatchInfo matchInfo)
	{
		if (success)
		{
			Debug.Log("Join match succeeded");
			Utility.SetAccessTokenForNetwork(matchInfo.networkId, matchInfo.accessToken);

			m_MatchJoined = true;
			m_MatchInfo = matchInfo;

			Debug.Log("Connecting to Address:" + matchInfo.address +
				" Port:" + matchInfo.port +
				" NetworKID: " + matchInfo.networkId +
				" NodeID: " + matchInfo.nodeId);
			ConnectThroughRelay(matchInfo.address, matchInfo.port, matchInfo.networkId,
				matchInfo.nodeId);
		}
		else
		{
			Debug.LogError("Join match failed: " + extendedInfo);
		}
	}


	public static void OnConnectionDropped(bool success, string extendedInfo)
	{
		Debug.Log("servidor Desconectado");
		NetworkTransport.Shutdown();
		m_HostId = -1;
		m_ConnectionIds.Clear();
		m_MatchInfo = null;
		m_MatchCreated = false;
		m_MatchJoined = false;
		m_ConnectionEstablished = false;

		SceneManager.LoadScene("oponentes");
	}


	public static void SetupHost(bool isServer)
	{
		Debug.Log("Initializing network transport");
		NetworkTransport.Init();
		var config = new ConnectionConfig();
		config.AddChannel(QosType.Reliable);
		config.AddChannel(QosType.Unreliable);
		var topology = new HostTopology(config, 4);
		if (isServer)
			m_HostId = NetworkTransport.AddHost(topology, k_ServerPort);
		else
			m_HostId = NetworkTransport.AddHost(topology);
	}


	public static void ConnectThroughRelay(string relayIp, int relayPort, NetworkID networkId, NodeID nodeId)
	{
		SetupHost(false);

		byte error;
		NetworkTransport.ConnectToNetworkPeer(
			m_HostId, relayIp, relayPort, 0, 0, networkId, Utility.GetSourceID(), nodeId, out error);
	}



	public static void StartServer(string relayIp, int relayPort, NetworkID networkId, NodeID nodeId)
	{
		SetupHost(true);

		byte error;
		NetworkTransport.ConnectAsNetworkHost(
			m_HostId, relayIp, relayPort, networkId, Utility.GetSourceID(), nodeId, out error);
	}



	void Update()
	{
		Receber_Inf ();
	}

	void Receber_Inf()
	{

		if (m_HostId == -1)
			return;

		var networkEvent = NetworkEventType.Nothing;
		int connectionId;
		int channelId;
		int receivedSize;
		byte error;

		// Get events from the relay connection
		networkEvent = NetworkTransport.ReceiveRelayEventFromHost(m_HostId, out error);
		if (networkEvent == NetworkEventType.ConnectEvent)

			Debug.Log("Relay server connected");

		if (networkEvent == NetworkEventType.DisconnectEvent)
			Debug.Log("Relay server disconnected");
		
		do
		{
			// Get events from the server/client game connection
			networkEvent = NetworkTransport.ReceiveFromHost(m_HostId, out connectionId, out channelId,
				m_ReceiveBuffer, (int)m_ReceiveBuffer.Length, out receivedSize, out error);
			if ((NetworkError)error != NetworkError.Ok)
			{
				Debug.Log("Error while receiveing network message");
			}

			switch (networkEvent)
			{
			case NetworkEventType.ConnectEvent:
				{
					Debug.Log("Connected through relay, ConnectionID:" + connectionId +
						" ChannelID:" + channelId);

					SceneManager.LoadScene("game");

					m_ConnectionEstablished = true;
					m_ConnectionIds.Add(connectionId);
					break;
				}
			case NetworkEventType.DataEvent:
				{
					Debug.Log("Data event, ConnectionID:" + connectionId +
						" ChannelID: " + channelId +
						" Received Size: " + receivedSize);
					//m_Reader = new NetworkReader(m_ReceiveBuffer);
					//m_LastReceivedMessage = m_Reader.ReadString();

					//De-Serialize recevied byte array into memory
					MemoryStream stream = new MemoryStream(m_ReceiveBuffer, 0, receivedSize);
					IFormatter formatter = new BinaryFormatter();
					stream.Seek(0, SeekOrigin.Begin);

					//Received data now stored as SendStructure
					SendStructure receivedStructure = formatter.Deserialize(stream) as SendStructure;

					if(receivedStructure.Code=="enviar_mensagem_chat")
					{
						GameObject.Find ("fala_2_text").GetComponent<Text> ().text = receivedStructure.Message.ToString().Replace ("\"", "");
					}


					if(receivedStructure.Code=="escrevendo_mensagem_chat")
					{
						inicio.Efeito_Escrevendo_Start();
					}

					if(receivedStructure.Code=="destribuir_pedras_oponente")
					{
						inicio.Destribuir_Pedras_Oponente(receivedStructure.Message);
					}

					if(receivedStructure.Code=="enviar_retirar_opositor_cova_dados")
					{
						inicio.Enviar_Retirar_Opositor_Cova_Dados(receivedStructure.Message);
					}

					if(receivedStructure.Code=="partilhar_nome")
					{
						GameObject.Find ("Jogador_2_Nome").GetComponent<Text> ().text = receivedStructure.Message;
					}

					if(receivedStructure.Code=="oponente_saiu")
					{
						GameObject Quem_Somos = Instantiate(Resources.Load("Oponente_Saiu")) as GameObject;;
						Quem_Somos.transform.SetParent(GameObject.Find ("Canvas").transform);
						Quem_Somos.transform.localPosition = Vector3.zero;
						Quem_Somos.transform.localScale = Vector3.one;
					}



					break;
				}
			case NetworkEventType.DisconnectEvent:
				{
					Debug.Log("Connection disconnected, ConnectionID:" + connectionId);
					break;
				}
			case NetworkEventType.Nothing:
				break;
			}
		} while (networkEvent != NetworkEventType.Nothing);
	}










}