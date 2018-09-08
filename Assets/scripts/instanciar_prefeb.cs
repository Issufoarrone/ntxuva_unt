using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instanciar_prefeb : MonoBehaviour {

	public GameObject Prefeb;

	// Use this for initialization
	void Start () {
		Instantiate (Prefeb, transform.position, transform.rotation);
	}

}
