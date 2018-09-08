using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class navegador : MonoBehaviour {

	public void mudar_sena(string sena)
	{
		SceneManager.LoadScene(sena);
	}
}
