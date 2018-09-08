using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fechar_janela : MonoBehaviour {


	public string Janela_Nome;

	public void Fechar_Janela()
	{
		
		if (GameObject.Find ("espera(Clone)")!=null) 
		{
			GameObject.Find ("espera(Clone)").SetActive (false);
		}

		GameObject.Find (Janela_Nome).SetActive (false);
	}
}
