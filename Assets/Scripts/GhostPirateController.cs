using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostPirateController : MonoBehaviour {

    private GameObject player;
    public float minDistance = 30f;
    public float ghostMoveSpeed = 1.2f;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Camera Rig");
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(Vector3.Distance(player.transform.position, this.transform.position) );
        if (Vector3.Distance(player.transform.position, this.transform.position) <= minDistance)
        {
            transform.LookAt(player.transform);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, (player.transform.position - transform.position), out hit, minDistance))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    transform.Translate(Vector3.forward * ghostMoveSpeed * Time.deltaTime * 6.0f);
                }
            }
        }
	}
}
