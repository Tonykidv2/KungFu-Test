using System.Collections;
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
            mPlayer.GetComponent<MainCharacterScript>().GoThere(_postion, true);
        }
    }
}
