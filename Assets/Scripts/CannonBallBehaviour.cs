using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallBehaviour : MonoBehaviour {

    public float cannonDamage = 10f;
    private GameObject notify;
    private AudioSource audioSource;
    public AudioClip shootSFX;
    public AudioClip impactSFX;


    private void Start()
    {
        audioSource = GameObject.Find("SFX").GetComponent<AudioSource>();
        audioSource.clip = shootSFX;
        audioSource.Play();
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
        audioSource.clip = impactSFX;
        audioSource.Play();
        Destroy(gameObject);
    }
}
