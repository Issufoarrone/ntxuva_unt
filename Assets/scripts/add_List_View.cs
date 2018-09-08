 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class add_List_View : MonoBehaviour {

	public GameObject ItemTemplate;
	public GameObject content;

	int cont = 0;
	string Nick_Name = "";

	public void Add_Button_Click()
	{
		var copy = Instantiate (ItemTemplate);
		copy.transform.parent = content.transform;
		cont++;


		foreach (Text component in  copy.GetComponentsInChildren<Text> ())
		{
			if (component.name == "Nick_Name") {
				component.text = cont.ToString();
				Nick_Name = component.text;
			}
		}


		foreach (Button component in  copy.GetComponentsInChildren<Button> ()) {

			if (component.name == "jogar_btm") {

				component.onClick.AddListener (delegate {
					TaskWithParameters ("Hello");
				});

			}
		}


	}


	void TaskWithParameters(string message)
	{
		//Output this to console when the Button is clicked
		Debug.Log(message);
	}
}
