using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoystickManager : MonoBehaviour {

	public GameObject notifyObj;

		void Update() {
        // check for button events 
        //if (Input.GetButton("P1-A")) { print("P1-A"); }
        if (Input.GetButton("P1-A")) { notifyObj.SendMessage("ButtonHit","P1-A"); }
		if (Input.GetButton("P1-B")) { notifyObj.SendMessage("ButtonHit","P1-B"); }
		if (Input.GetButton("P1-Y")) { notifyObj.SendMessage("ButtonHit","P1-Y"); }
		if (Input.GetButton("P1-X")) { notifyObj.SendMessage("ButtonHit","P1-X"); }
		if (Input.GetButton("P1-LeftBumper")) { notifyObj.SendMessage("ButtonHit","P1-LeftBumper"); }
		if (Input.GetButton("P1-RightBumper")) { notifyObj.SendMessage("ButtonHit","P1-RightBumper"); }
		if (Input.GetButton("P1-Back")) { notifyObj.SendMessage("ButtonHit","P1-Back"); }
		if (Input.GetButton("P1-Start")) { notifyObj.SendMessage("ButtonHit","P1-Start"); }
		if (Input.GetButton("P1-LeftStick")) { notifyObj.SendMessage("ButtonHit","P1-LeftStick"); }
		if (Input.GetButton("P1-RightStick")) { notifyObj.SendMessage("ButtonHit","P1-RightStick"); }

		if (Input.GetButton("P2-A")) { notifyObj.SendMessage("ButtonHit","P2-A"); }
		if (Input.GetButton("P2-B")) { notifyObj.SendMessage("ButtonHit","P2-B"); }
		if (Input.GetButton("P2-Y")) { notifyObj.SendMessage("ButtonHit","P2-Y"); }
		if (Input.GetButton("P2-X")) { notifyObj.SendMessage("ButtonHit","P2-X"); }
		if (Input.GetButton("P2-LeftBumper")) { notifyObj.SendMessage("ButtonHit","P2-LeftBumper"); }
		if (Input.GetButton("P2-RightBumper")) { notifyObj.SendMessage("ButtonHit","P2-RightBumper"); }
		if (Input.GetButton("P2-Back")) { notifyObj.SendMessage("ButtonHit","P2-Back"); }
		if (Input.GetButton("P2-Start")) { notifyObj.SendMessage("ButtonHit","P2-Start"); }
		if (Input.GetButton("P2-LeftStick")) { notifyObj.SendMessage("ButtonHit","P2-LeftStick"); }
		if (Input.GetButton("P2-RightStick")) { notifyObj.SendMessage("ButtonHit","P2-RightStick"); }
	}

	public float GetAxis(string name) {
		// return the value of the given axis
		return(Input.GetAxis(name));
	}
    
}
