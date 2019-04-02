using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallBehaviour : MonoBehaviour {

    public float cannonDamage = 10f;
    private GameObject notify;

    private void Start()
    {
        notify = GameObject.Find("MainControl");
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Cannon hit: " + other.gameObject.name);
        if (other.gameObject.name == "BREAK Wall_02_Decorated (Front Door)")
        {
            notify.SendMessage("TriggerEvent", "Trigger #5 - break first wall");
        }
        Target target = other.transform.GetComponent<Target>();
        if (target != null)
        {
            target.TakeDamage(cannonDamage);
        }
        Destroy(gameObject);
    }
}
