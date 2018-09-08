using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sons : MonoBehaviour {


	public void Play_Sound(string Path)
	{
		GameObject.Find (Path).GetComponent<AudioSource> ().Play ();
	}
}
