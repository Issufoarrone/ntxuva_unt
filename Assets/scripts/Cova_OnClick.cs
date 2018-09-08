using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Required when using Event data.

public class Cova_OnClick : MonoBehaviour // required interface when using the OnPointerDown method.
{

	//public 
	public void OnClick () 
	{


		inicio a = new inicio ();
		StartCoroutine (  a.Destribuir_Pedras (this.gameObject));
	

		//gameObject.GetComponent<Image>().color = Color.Lerp(new Color(1,1,1,0), Color.white, 1 * Time.deltaTime);

		//Debug.Log (this.gameObject.name + " Was Clicked. cova N."+inicio.Object_To_Cova_Numeber(this.gameObject));
	}




}
/*
int rndNum  = Random.Range(1, 24);
Debug.Log(rndNum);
//StartCoroutine ( DPO (rndNum));


yield return DPO (rndNum);
*/