using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class chamar_janela : MonoBehaviour {

	public string Janela_Nome;


	public void Chamar_Janela()
	{
		GameObject Quem_Somos = Instantiate(Resources.Load(Janela_Nome)) as GameObject;;
		Quem_Somos.transform.SetParent(GameObject.Find ("Canvas").transform);
		Quem_Somos.transform.localPosition = Vector3.zero;
	    Quem_Somos.transform.localScale = Vector3.one;
	}


}
