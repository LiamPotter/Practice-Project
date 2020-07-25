using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractionButton : MonoBehaviour {

	public bool RequireButtonPress;

	public bool OneTimeActivation;

	private bool playerInsideTrigger;

	private bool activatedOnce=false;

	public UnityEvent OnPress;

	void Update()
	{
		if (!RequireButtonPress)
			return;
		if (playerInsideTrigger && Input.GetKey(KeyCode.E))
		{
			OnPress.Invoke();
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag != "Player")
			return;
		if (OneTimeActivation && activatedOnce)
			return;
		playerInsideTrigger = true;
		if (!RequireButtonPress)
		{
			OnPress.Invoke();
			activatedOnce = true;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag != "Player")
			return;
		playerInsideTrigger = false;
	}
}
