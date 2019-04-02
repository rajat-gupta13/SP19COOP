using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerControl : MonoBehaviour {

	public AudioClip[] clips;
	public AudioSource audio;
	public CameraMovement2 move;
	public GameObject titleScreen;
	public GameObject creditScreen;
    public GameObject getInsideTrigger;
    public GameObject room2Trigger;

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

		    case "Trigger #2 - enable tutorial":
			    // play audio file #1
			    StartCoroutine(PlayFile(2, 0.01f));
                // Right Guest is allowed to move throttle (joystick)
                move.subHighCutoff = 300.0f;
			    //move.p1Enabled = true;
                getInsideTrigger.SetActive(true);
                StartCoroutine(WaitingToTrigger("Trigger #2-2 - give it a try", clips[1].length + 0.01f));
                break;

            case "Trigger #2-2 - give it a try":
                // play audio file #1
                StartCoroutine(PlayFile(3, 0.01f));
                // Left Guest is allowed to move throttle (joystick)
                //move.p2Enabled = true;
                //StartCoroutine(WaitingToTrigger("Trigger #2-3 - tutorial ends", clips[3].length + 0.01f));
                break;

            case "Trigger #2-3 - tutorial ends":
                // play audio file #1
                StartCoroutine(PlayFile(5, 0.01f));               
                break;

            case "Trigger #3 - not getting in keep":
                // play audio file #1
                if (UnityEngine.Random.Range(0,1) > 0.5)
                    StartCoroutine(PlayFile(21, 0.01f));
                else
                    StartCoroutine(PlayFile(25, 0.01f));
                // Right Guest is allowed to move throttle (joystick)
                break;

            case "Trigger #4 - shoot at wall":
                // play audio file #1
                StartCoroutine(PlayFile(7, 0.01f));
                // treasure chest picked
                //move.treasurePicked = true;
                break;

            case "Trigger #5 - break first wall":
                // play audio file #1
                StartCoroutine(PlayFile(8, 0.01f));
                // treasure chest picked
                break;

            case "Trigger #6 - enter keep":
                // play audio file #1
                StartCoroutine(PlayFile(10, 0.01f));
                StartCoroutine(WaitingToTrigger("Trigger #7 - locate treasure", clips[10].length + 0.01f));
                break;

            case "Trigger #7 - locate treasure":
                // play audio file #1
                StartCoroutine(PlayFile(12, 0.01f));
                // treasure chest picked
                break;

            case "Trigger #8 - treasure chest picked":
                // play audio file #1
                StartCoroutine(PlayFile(11, 0.01f));
                StartCoroutine(WaitingToTrigger("Trigger #9 - cave in start", clips[11].length + 0.01f));
                // treasure chest picked
                move.treasurePicked = true;
                break;

            case "Trigger #9 - cave in start":
                // play audio file #1
                StartCoroutine(PlayFile(13, 0.01f));
                StartCoroutine(WaitingToTrigger("Trigger #10 - cave in happening", clips[13].length + 0.01f));
                // treasure chest picked
                break;

            case "Trigger #10 - cave in happening":
                // play audio file #1
                StartCoroutine(PlayFile(15, 0.01f));
                StartCoroutine(WaitingToTrigger("Trigger #11 - ghost pirates appear", clips[15].length + 0.01f));
                // treasure chest picked
                break;

            case "Trigger #11 - ghost pirates appear":
                // play audio file #1
                StartCoroutine(PlayFile(14, 0.01f));
                StartCoroutine(WaitingToTrigger("Trigger #12 - get back here", clips[14].length + 0.01f));
                // treasure chest picked
                break;

            case "Trigger #12 - get back here":
                // play audio file #1
                StartCoroutine(PlayFile(16, 0.01f));
                // treasure chest picked
                break;

            case "Trigger #13 - spawn ghost in room":
                // play audio file #1
                StartCoroutine(PlayFile(17, 0.01f));
                // treasure chest picked
                room2Trigger.SetActive(false);
                move.room2Entered = true;
                break;

            case "Trigger #14 - exit keep":
                // play audio file #1
                StartCoroutine(PlayFile(18, 0.01f));
                StartCoroutine(WaitingToTrigger("Trigger #15 - credits", clips[18].length + 0.01f));
                // treasure chest picked
                move.endExperience = true;
                break;

            case "Trigger #15 - credits":
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
