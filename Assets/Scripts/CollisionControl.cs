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
                //notify.SendMessage("TriggerEvent", "Trigger #2 - enable player1 tutorial");
                break;
            case "get inside trigger":
                notify.SendMessage("TriggerEvent", "Trigger #3 - not getting in keep");
                break;
            case "treasure chest":
                notify.SendMessage("TriggerEvent", "Trigger #8 - treasure chest picked");
                break;
            case "weird wall trigger":
               
                    notify.SendMessage("TriggerEvent", "Trigger #4 - shoot at wall");
                break;
            case "enter keep trigger":
                if (notify.GetComponent<CameraMovement2>().treasurePicked)
                {
                    notify.SendMessage("TriggerEvent", "Trigger #14 - exit keep");
                }
                else
                {
                    notify.SendMessage("TriggerEvent", "Trigger #6 - enter keep");
                }
                break;
            case "spawn ghost in room":
                notify.SendMessage("TriggerEvent", "Trigger #13 - spawn ghost in room");
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
