﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

    private GameObject mPlayer;
    private float mRotationSpeed = 5f;
    // Use this for initialization
    void Start () {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
    }

	// Update is called once per frame
	void Update () {
        TurnTo();
	}

    private void TurnTo()
    {
        //find the vector pointing from our position to the target
        Vector3 _direction = (mPlayer.transform.position - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        Quaternion _lookRotation = Quaternion.LookRotation(_direction);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * mRotationSpeed);
    }

    void OnTouchDown(Vector3 _postion)
    {
        if (mPlayer != null)
        {
            mPlayer.GetComponent<MainCharacterScript>().GoThere(transform.position, true);
        }
    }

	private void OnTriggerEnter(Collider other)
	{
        if(other.tag == "Hitter")
        {
            //Will uncomment once I have full control over animations
            //if (mPlayer.GetComponent<MainCharacterScript>().isApexAttack() == false)
            //return;

            // force is how forcefully we will push the player away from the enemy.
            float force = 100;

            // Calculate Angle Between the collision point and the player
            Vector3 dir = mPlayer.transform.position - transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            GetComponent<Rigidbody>().AddForce(dir * force);
            Invoke("DeactivateForce", .5f);
        }
            
	}

    void DeactivateForce()
    {
        Debug.Log("Attempting to Stop Enemy Stagger");

        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}
