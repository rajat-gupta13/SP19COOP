using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionControl : MonoBehaviour {

    public GameObject notify;
    [HideInInspector]
    public bool introStart = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)     {
		print("Trigger hit: " + other.gameObject.name);
        switch (other.gameObject.name)
        {
            case "intro trigger":
                introStart = true;
                notify.SendMessage("TriggerEvent", "Trigger #2 - experience begins");
                break;
            case "after tutorial trigger":
                notify.SendMessage("TriggerEvent", "Trigger #3 - reach a certain point");
                break;
            case "small tremmor trigger":
                notify.SendMessage("TriggerEvent", "Trigger #4 - minor tremor, small rocks fall");
                break;
            case "get to y trigger":
                notify.SendMessage("TriggerEvent", "Trigger #5 - get to the y");
                break;
            case "loose the radio trigger":
                notify.SendMessage("TriggerEvent", "Trigger #6 - starting down the left of y");
                break;
            case "Music Change to Keep trigger":
                notify.SendMessage("TriggerEvent", "Trigger #7 - enter the underwater valley");
                break;
            case "Center of the Keep trigger (1)":
                notify.SendMessage("TriggerEvent", "Trigger #8 - hit the center of the keep");
                break;
            case "After the Center of the Keep trigger (2)":
                notify.SendMessage("TriggerEvent", "Trigger #9 - after the center point");
                break;
            case "Close call trigger":
                notify.SendMessage("TriggerEvent", "Trigger #10 - reach the way out");
                break;
            case "Ending trigger":
                notify.SendMessage("TriggerEvent", "Trigger #11 - back in the tunnel");
                break;
            case "credits trigger":
                notify.SendMessage("TriggerEvent", "Trigger #12 - credits");
                break;
        }
    }

	void OnCollisionEnter(Collision col) {
		print("Collider hit: " + col.gameObject.name);
		switch (col.gameObject.name) {
			case "floor":
				notify.SendMessage ("CollisionEvent", "Collider #1 - credits");
					//Destroy(col.gameObject);	// should the collider be removed
				break;
		}
	}

}
