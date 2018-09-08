using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class jogadas_verificador : MonoBehaviour {

	public void Offline()
	{
		
		if(PlayerPrefs.GetInt ("Jogadas_Offline")>=1)
		{
			SceneManager.LoadScene("offline");
		}
		else
		{
			Debug.Log ("Voce nao tem jogadas offline");
			GameObject Quem_Somos = Instantiate(Resources.Load("Nao_Tem_Jogadas_Offline")) as GameObject;;
			Quem_Somos.transform.SetParent(GameObject.Find ("Canvas").transform);
			Quem_Somos.transform.localPosition = Vector3.zero;
			Quem_Somos.transform.localScale = Vector3.one;
		}

	}

	public void Online()
	{

		if(PlayerPrefs.GetInt ("Jogadas_Online")>=1)
		{
			SceneManager.LoadScene("online");
		}
		else
		{
			Debug.Log ("Voce nao tem jogadas online");
			GameObject Quem_Somos = Instantiate(Resources.Load("Nao_Tem_Jogadas_Online")) as GameObject;;
			Quem_Somos.transform.SetParent(GameObject.Find ("Canvas").transform);
			Quem_Somos.transform.localPosition = Vector3.zero;
			Quem_Somos.transform.localScale = Vector3.one;
		}

	}
}
