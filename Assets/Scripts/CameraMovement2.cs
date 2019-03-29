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
    public float moveSpeed = 2f;
    public float rotateSpeedPitch = 2f;
    public float rotateSpeedRoll = 2f;
    public Button portButton;
    public Button starboardButton;
    public Text cannonActivatedText;
    public Text cannonAssignedText;

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
    public float upwardSpeed = 0.1f;  // rcc - rate of verticle movement
    private bool started = false;
    private string floorStatus = "down";
    public float subHighCutoff = 100.0f;
    public bool cannonEnabled = false;
    private float pitch;
    private float roll;
    private bool cannonAssigned = false;
    private bool portCannons = false;
    private bool starboardCannons = false;
    private bool cannonEnableFirst = false;
    public GameObject startTarget;

    public GameObject cannonPrefab;
    public Transform cannonSpawn;
    public float cannonSpeed = 30f;
    public float cannonLifetime = 3f;
    private bool startExperience = false;

    private bool cannonFired = false;
    public float fireDelay = 1f;
    public GameObject flashlight;
    private bool currentFlashlightState = false;

    [SerializeField]
    private float radius = 1.0F;
    [SerializeField]
    private float power = 10.0F;

    // Use this for initialization
    void Start () {
        portButton.interactable = false;
        starboardButton.interactable = false;
        cannonActivatedText.text += " " + cannonEnabled;
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
        flashlight.SetActive(false);
    }

    public void StartButton() {
        // this is called when the start button on the puppet screen is clicked by the tour guide
        if (!started) {
            started = true;
            triggerControl.TriggerEvent("Trigger #1 - experience start");
            print("i'm starting");
            startExperience = true;
        }
        
    }

    private void ToggleFlashlight(bool state)
    {
        currentFlashlightState = !state;
        flashlight.SetActive(currentFlashlightState);
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

    public void StarboardButton()
    {
        // this is called when the Starboard button on the puppet screen is clicked by the tour guide. The guest on the starboard side will man the cannons
        cannonAssigned = true;
        starboardCannons = true;
        portCannons = false;
        starboardButton.interactable = false;
        portButton.interactable = false;
        cannonAssignedText.text += " Starboard";
    }

    public void PortButton()
    {
        // this is called when the Port button on the puppet screen is clicked by the tour guide. The guest on the port side will man the cannons
        cannonAssigned = true;
        starboardCannons = false;
        portCannons = true;
        starboardButton.interactable = false;
        portButton.interactable = false;
        cannonAssignedText.text += " Port";
    }

    // Update is called once per frame
    void Update () {
        
        // help the facilatator tell what joystick is which
        if (!started) {
            starboardText.text = "Starboard Controller - ";
            portText.text = "Port Controller - ";
        } 

      
        if (!cannonEnabled)
        {
           // pitch = Input.GetAxis("P1-VerticalLeft");
           // roll = Input.GetAxis("P2-HorizontalRight");
        }
        if (cannonEnabled)
        {
            if (!cannonEnableFirst)
            {
                cannonActivatedText.text = "Cannon Activated - " + cannonEnabled;
                portButton.interactable = true;
                starboardButton.interactable = true;
                cannonEnableFirst = true;
            }
            if (starboardCannons)
            {
                //pitch = Input.GetAxis("P2-VerticalLeft");
                roll = Input.GetAxis("P2-HorizontalRight");
                if ((Mathf.Abs(Input.GetAxis("P1-LTrigger")) == 1 || Mathf.Abs(Input.GetAxis("P1-RTrigger")) == 1) && !cannonFired)
                {
                    Shoot();
                }
            }
            else if (portCannons)
            {
                //pitch = Input.GetAxis("P1-VerticalLeft");
                roll = Input.GetAxis("P1-HorizontalRight");
                if ((Mathf.Abs(Input.GetAxis("P2-LTrigger")) == 1 || Mathf.Abs(Input.GetAxis("P2-RTrigger")) == 1) && !cannonFired)
                {
                    Shoot();
                }
            }
        }

        if (Input.GetButtonDown("P1-A") || Input.GetButtonDown("P2-A"))
        {
            ToggleFlashlight(currentFlashlightState);
        }

        
        if (startExperience && !node.GetComponent<CollisionControl>().introStart)
            node.transform.position = Vector3.MoveTowards(node.transform.position, startTarget.transform.position, 6.0f * Time.deltaTime);
        // update the position of the node
        node.transform.Translate(Vector3.forward * forward * Time.deltaTime*6.0f);
        node.transform.Translate(Vector3.up * up * Time.deltaTime * 15.0f);     // rcc - add in rise and fall
        if (p1Enabled && p2Enabled)
        {
            node.transform.Rotate(Vector3.up, roll * Time.deltaTime * 10.0f * rotateSpeedRoll);
            //node.transform.Rotate(Vector3.right, pitch * Time.deltaTime * 10.0f * rotateSpeedPitch);
        }
        // rcc - cap the sub vertical height
        //if (node.transform.position.y > subHighCutoff) { node.transform.position = new Vector3(node.transform.position.x, subHighCutoff, node.transform.position.z); }

    }

    public IEnumerator DestroyObject(GameObject current, GameObject shatter)
    {
        shatter.transform.position = current.transform.position;
       
        current.SetActive(false);
        shatter.SetActive(true);
        //shatter.GetComponent<AudioSource>().Play();
        Vector3 explosionPos = shatter.transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null && rb.gameObject.tag != "Player")
            {
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
                //Debug.Log("Force Added");
                rb.useGravity = true;
            }
        }
        StartCoroutine(DisableShardPhysics(shatter));
        yield return null;
    }

    private IEnumerator DisableShardPhysics(GameObject shatter)
    {
        yield return new WaitForSeconds(2.5f);
        foreach (Rigidbody rb in shatter.GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }
        yield return new WaitForSeconds(1f);
        int shardCount = shatter.transform.childCount;
        for (int i = 0; i < shardCount; i += 2)
        {
            Destroy(shatter.transform.GetChild(i).gameObject);
        }
    }


    void Shoot() {
        cannonFired = true;
        Debug.Log("Shooting");
        GameObject cannon = Instantiate(cannonPrefab);

        Physics.IgnoreCollision(cannon.GetComponent<Collider>(), cannonSpawn.parent.GetComponent<Collider>());

        cannon.transform.position = cannonSpawn.position;
        Vector3 rotation = cannon.transform.rotation.eulerAngles;

        cannon.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);

        cannon.GetComponent<Rigidbody>().AddForce(cannonSpawn.forward * cannonSpeed, ForceMode.Impulse);
        StartCoroutine(ResetCannon(fireDelay));
        StartCoroutine(DestroyCannonAfterTime(cannon, cannonLifetime));
    }

    private IEnumerator DestroyCannonAfterTime(GameObject cannon, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(cannon);
    }

    private IEnumerator ResetCannon(float delay)
    {
        yield return new WaitForSeconds(delay);
        cannonFired = false;
    }

    void Position()
    {
        if (starboardCannons)
        {
            up = - Input.GetAxis("P2-VerticalLeft") * upwardSpeed;
            if (Mathf.Abs(Input.GetAxis("P2-LTrigger")) == 1 || Mathf.Abs(Input.GetAxis("P2-RTrigger")) == 1)
            {
                Debug.Log("Moving P2");
                forward = moveSpeed;
            }
            else
            {
                forward = 0.0f;
            }
        }
        else if (portCannons)
        {
            up = - Input.GetAxis("P1-VerticalLeft") * upwardSpeed;
            if (Mathf.Abs(Input.GetAxis("P1-LTrigger")) == 1 || Mathf.Abs(Input.GetAxis("P1-RTrigger")) == 1)
            {
                Debug.Log("Moving P1");
                forward = moveSpeed;
            }
            else
            {
                forward = 0.0f;
            }
        }

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
                if (!started) starboardText.text += " Left Stick pressed";

                break;
		case "P1-RightStick":
                // do something
                if (!started) starboardText.text += " Right Stick pressed";
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
