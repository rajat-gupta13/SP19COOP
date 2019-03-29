using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallBehaviour : MonoBehaviour {

    public float cannonDamage = 10f;

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Cannon hit: " + other.gameObject.name);
        Target target = other.transform.GetComponent<Target>();
        if (target != null)
        {
            target.TakeDamage(cannonDamage);
        }
        Destroy(gameObject);
    }
}
