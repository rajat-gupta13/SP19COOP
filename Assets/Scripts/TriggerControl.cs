using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerControl : MonoBehaviour {

	public AudioClip[] clips;
	public AudioSource audio;
	public CameraMovement2 move;
	public GameObject titleScreen;
	public GameObject creditScreen;

	private int index;

	// Use this for initialization
	void Start () {
		titleScreen.SetActive (true);
		creditScreen.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		// for testing - use a keyboard key to trigger some event
		if (Input.GetKeyDown(KeyCode.J))
		{
			TriggerEvent ("Trigger #2 - experience begins");
		}
	}

	public void TriggerEvent(string eventName) {
		// DANGER!!!! MATCH INDEX VALUES WITH ARRAY INDEX AS LOADED INTO CLIPS VAR IN EDITOR
		print("in TriggerEvent with : " + eventName);
		switch (eventName)
		{
		    case "Trigger #1 - experience start":
			    // hide the title screen
			    titleScreen.SetActive(false);
		    	// setup lighting
			    StartCoroutine(NormalLights(Color.blue, 0.01f));
			    // play audio file title screen music
			    StartCoroutine(PlayFile(0, 0.01f));
			    //StartCoroutine(WaitingToTrigger("Trigger #2 - experience begins", clips[1].length + 0.01f));
			    break;

		    case "Trigger #2 - enable player1 tutorial":
			    // play audio file #1
			    StartCoroutine(PlayFile(1, 0.01f));
			    // Right Guest is allowed to move throttle (joystick)
			    move.p1Enabled = true;
			    break;

            case "Trigger #2-2 - enable player2 tutorial":
                // play audio file #1
                StartCoroutine(PlayFile(2, 0.01f));
                // Left Guest is allowed to move throttle (joystick)
                move.p2Enabled = true;
                break;

            case "Trigger #2-3 - tutorial ends":
                // play audio file #1
                StartCoroutine(PlayFile(3, 0.01f));               
                break;

            case "Trigger #3 - whale interaction":
                // play audio file #1
                StartCoroutine(PlayFile(4, 0.01f));
                // Right Guest is allowed to move throttle (joystick)
                move.whaleEnabled = true;
                break;

            case "Trigger #12 - credits":
			    // show credit screen
			    creditScreen.SetActive(true);
			    // stop guests from moving
			    move.p1Enabled = false;
			    move.p2Enabled = false;
			    break;

		}
	}

	IEnumerator PlayFile(int i, float delay) {
		// wait if needed
		yield return new WaitForSeconds(delay);
		// play an audio clip
		audio.clip = clips [i];
		audio.Play ();
	}

	IEnumerator SendObjMessage(GameObject obj, string func, float delay) {
		// wait if needed 
		yield return new WaitForSeconds(delay);
		// tell the supplied obj to run the supplied function
		obj.SendMessage(func);
	}

	IEnumerator WaitingToTrigger(string trigger, float delay) {
		// wait if needed 
		yield return new WaitForSeconds(delay);
		// trigger the supplied event
		TriggerEvent(trigger);
	}

	IEnumerator BlinkingLights(Color newColor, float delay)
	{
		// flashing certain colored lights
		float endTime = Time.time + delay;
		while (Time.time < endTime) 
		{
			DMXController.Lighting.TurnOff("Guest");
			//LightDelay();
			yield return new WaitForSeconds(0.2f);
			DMXController.Lighting.TurnOn("Guest", newColor, 0, 255);
			//LightDelay();
			yield return new WaitForSeconds(0.2f);
		}
		//yield return null;
	}

	IEnumerator NormalLights(Color newColor, float delay)
	{
		// delay the lighting change and then set the guest lights
		yield return new WaitForSeconds(delay);
		DMXController.Lighting.TurnOn("Guest", Color.blue, 0, 255);
	}

}
