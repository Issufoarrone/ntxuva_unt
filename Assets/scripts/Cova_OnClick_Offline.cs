using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Required when using Event data.

public class Cova_OnClick_Offline : MonoBehaviour // required interface when using the OnPointerDown method.
{

	//public 
	public void OnClick () 
	{


		inicio_offline a = new inicio_offline ();
		StartCoroutine (  a.Destribuir_Pedras (this.gameObject));
	

		//gameObject.GetComponent<Image>().color = Color.Lerp(new Color(1,1,1,0), Color.white, 1 * Time.deltaTime);

		//Debug.Log (this.gameObject.name + " Was Clicked. cova N."+inicio.Object_To_Cova_Numeber(this.gameObject));
	}




}