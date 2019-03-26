using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhaleCollisions : MonoBehaviour {

    [SerializeField]
    public GameObject move;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        print("Trigger hit: " + other.gameObject.name);
        switch (other.gameObject.name)
        {
            case "Target1":
                move.GetComponent<CameraMovement2>().targetCount++;
                break;
            case "Target2":
                move.GetComponent<CameraMovement2>().targetCount++;
                break;
            case "Target3":
                move.GetComponent<CameraMovement2>().whaleEnabled = false;
                move.GetComponent<CameraMovement2>().whale.SetActive(false);
                break;
        }
    }
}
