using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement2 : MonoBehaviour {

	//public FLController floor;
	public FloorController floor;
	public Text portText;
	public Text starboardText;
    public GameObject node;         // camera rig node
    public TriggerControl triggerControl;

    [HideInInspector]
    public bool p1Enabled = false;
    [HideInInspector]
    public bool p2Enabled = false;
    [HideInInspector]
    public bool canRise = false;
    [HideInInspector]
    public bool canFall = false;


    private bool rightfirst = false;
    private bool leftfirst = false;
    private bool risefirst = false;
    private bool fallfirst = false;
    private float left;
    private float right;
    private bool rise = false;   // rcc - changed from float  to bool
    private bool fall = false;   // rcc - changed from float to bool
    private float forward = 0.0f;
    private float up = 0.0f;
    private float turn = 0.0f;
    private float forwardThresh = 0.2f;
    private float upwardSpeed = 0.1f;  // rcc - rate of verticle movement
    private bool started = false;
    private string floorStatus = "down";
    private float subHighCutoff = 15.0f;

    // Use this for initialization
    void Start () {
        InvokeRepeating("Position", 0.1f, 0.1f);
        Invoke("SetupFloor", 3.0f);
        // just test how many joystick are seen
        string[] joys = Input.GetJoystickNames();
        foreach (string j in joys)
        {
            print(j);
        }
        // JUST TO GET STARTED
        p1Enabled = true;
        p2Enabled = true;
        
    }

    public void StartButton() {
        // this is called when the start button on the puppet screen is clicked by the tour guide
        if (!started) {
            started = true;
            ///triggerControl.TriggerEvent("Trigger #1 - title screen");
            print("i'm starting");
        }
    }

    public void SkipButton()
    {
        // this is called when the start button on the puppet screen is clicked by the tour guide
        if (!started)
        {
            started = true;
            ///triggerControl.TriggerEvent("Trigger #2-5-7 - skip to here");
            print("i'm skipping");
        }
    }

    // Update is called once per frame
    void Update () {
        
        // help the facilatator tell what joystick is which
        if (!started) {
            starboardText.text = "Starboard Controller - ";
            portText.text = "Port Controller - ";
        } 

        // check for joystick or keyboard input => store the input  AND test for tutorial advancement
        if (p1Enabled == true) {
            right = -Input.GetAxis("P1-VerticalLeft");
            if (Input.GetKey(KeyCode.RightArrow)) right = 1.0f;   // rcc - add keyboard control for testing
            ///if (right > 0.3 && !rightfirst) {        // advance tutorial?
                ///rightfirst = true;
                ///triggerControl.TriggerEvent("Trigger #2-2 - experience begins");
            ///}
        }
        if (p2Enabled == true) {
            left = -Input.GetAxis("P2-VerticalLeft");
			if (Input.GetKey(KeyCode.LeftArrow)) left = 1.0f; // rcc - add keyboard control for testing
            ///if (left > 0.3 && !leftfirst) {        // advance tutorial?
                ///leftfirst = true;
                ///triggerControl.TriggerEvent("Trigger #2-3 - experience begins");
            ///}
        }

        /*if (canRise == true) {
            rise = Input.GetButton("P1-A"); // rcc - changed to bool since Input returns bool
            if (Input.GetKey(KeyCode.UpArrow)) rise = true; // rcc - add keyboard control for testing
            //if (rise && !risefirst) {       // advance tutorial
                ///risefirst = true;
                ///triggerControl.TriggerEvent("Trigger #2-4 - experience begins");
            ///}
        }
        if (canFall == true) {
            fall = Input.GetButton("P2-A"); // rcc - changed to bool since Input returns bool
            if (Input.GetKey(KeyCode.S)) fall = true; // rcc - add keyboard control for testing
            ///if (fall && !fallfirst) {        // advance tutorial?
                ///fallfirst = true;
                ///triggerControl.TriggerEvent("Trigger #2-5-6 - experience begins");
            ///}
        }*/

        // update the position of the node
        node.transform.Translate(Vector3.forward * forward * Time.deltaTime*6.0f);
        ///node.transform.Translate(Vector3.up * up * Time.deltaTime * 15.0f);     // rcc - add in rise and fall
        node.transform.Rotate(Vector3.up, turn * Time.deltaTime * 10.0f);
        // rcc - cap the sub vertical height
        ///if (node.transform.position.y > subHighCutoff) { node.transform.position = new Vector3(node.transform.position.x, subHighCutoff, node.transform.position.z); }

    }

    void Position()
    {
        forward = Math.Abs(Input.GetAxis("Vertical"));
        /*
        // how far off are the sticks from each other
        float dif = left - right;
        // move forward if sticks are within a threshold of each other
        if (Mathf.Abs(dif) <= forwardThresh)
        {
            forward = left;
            turn = 0.0f;
        }
        // else turn 
        else
        {
            forward = 0.0f;
            turn = dif;
        }
        // rcc - add in upward movement
        up = 0.0f;
        ///if (rise) up = upwardSpeed;
        ///if (fall) up = upwardSpeed * -1.0f;

        // rcc - position the floor
        if (turn == 0.0f && floorStatus != "down")
        {
            // moving so drop the floor
            //floor.LevelDown();
            floorStatus = "down";
            floor.moveAll(5.0f);
            //floor.SetFloor(new Vector4(5.0f, 5.0f, 5.0f, 5.0f));
        }
        else if (turn < 0.0f && floorStatus != "right")
        {
            // turning right so tilt the floor
            floor.raiseRight(10.0f);
            floorStatus = "right";
            ///floor.SetFloor(new Vector4(0.0f, 10.0f, 0.0f, 10.0f));
        }
        else if (turn > 0.0f && floorStatus != "left")
        {
            // turning left so tilt the floor
            floor.raiseLeft(10.0f);
            floorStatus = "left";
           //// floor.SetFloor(new Vector4(10.0f, 0.0f, 10.0f, 0.0f));
        }
         */
    }

    public void StopMovement()
    {
        // called by TrackWaypoints to stop the player after teleport
        p1Enabled = false;
        p2Enabled = false;
        right = 0;
        left = 0;
        floor.moveAll(0.0f);
    }

    public void ButtonHit(string name) {
        // a joystick button has been hit
        print(name);
		switch (name) {
		case "P1-A": 
			// help the tour guide to know this is the P1 stick
			if (!started) starboardText.text += " GOT IT";
			// do something
            ///if (canRise) rise = true;
			break;
		case "P1-B": 
			// do something

			break;
		case "P1-Y": 
			// do something

			break;
		case "P1-X": 
			// do something

			break;
		case "P1-LeftBumper": 
			// do something

			break;
		case "P1-RightBumper": 
			// do something

			break;
		case "P1-Back": 
			// do something

			break;
		case "P1-Start": 
			// do something

			break;
		case "P1-LeftStick": 
			// do something

			break;
		case "P1-RightStick": 
			// do something

			break;

		case "P2-A": 
			// help the tour guide to know this is the P1 stick
			if (!started) portText.text += " GOT IT";
			// do something
            ///if (canFall) fall = true;
			break;
		case "P2-B": 
			// do something

			break;
		case "P2-Y": 
			// do something

			break;
		case "P2-X": 
			// do something

			break;
		case "P2-LeftBumper": 
			// do something

			break;
		case "P2-RightBumper": 
			// do something

			break;
		case "P2-Back": 
			// do something

			break;
		case "P2-Start": 
			// do something

			break;
		case "P2-LeftStick": 
			// do something

			break;
		case "P2-RightStick": 
			// do something

			break;
		
		}
	}

    void SetupFloor()
    {
        floor.resetFloor();
        floor.enable();
        floorStatus = "down";
    }
    
}
