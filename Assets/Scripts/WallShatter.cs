using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallShatter : MonoBehaviour {

    private bool hasCollided = false;
    public GameObject current, shatter;
    public GameObject mainController;
    
    // Use this for initialization
    void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.tag == "CannonBall") && !hasCollided)
        {
            Debug.Log("Wall Collided with " + this.gameObject.name);
            hasCollided = true;
            //GetComponent<AudioSource>().Play();
            StartCoroutine(mainController.GetComponent<CameraMovement2>().DestroyObject(current, shatter));
        }
    }
}
