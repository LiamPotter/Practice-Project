using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DummyButton : MonoBehaviour {

	public Text DisplayText;
	
	public void DisplayNameAndColor()
	{
		if(!DisplayText)
		{
			Debug.LogError("DisplayText was not assigned on " + name);
			return;
		}
		DisplayText.text = name;
		DisplayText.color = this.GetComponent<Button>().colors.normalColor;
	}
}
