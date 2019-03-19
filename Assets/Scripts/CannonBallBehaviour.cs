using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallBehaviour : MonoBehaviour {

    public float cannonDamage = 10f;

    private void OnTriggerEnter(Collider other)
    {
        Target target = other.transform.GetComponent<Target>();
        if (target != null)
        {
            target.TakeDamage(cannonDamage);
        }
        Destroy(gameObject);
    }
}
