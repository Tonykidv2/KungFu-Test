using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CameraControl : MonoBehaviour {

    private GameObject mPlayer;
    private float mRotationSpeed = 5f;
	// Use this for initialization
	void Start () {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        //if(mPlayer.GetComponent<NavMeshAgent>().velocity != Vector3.zero)
        //{
            TurnTo();
        //}
	}

    private void TurnTo()
    {
        //find the vector pointing from our position to the target
        Vector3  _direction = (mPlayer.transform.position - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        Quaternion _lookRotation = Quaternion.LookRotation(_direction);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * mRotationSpeed);
    }
}
