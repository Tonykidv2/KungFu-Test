using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNavigateHere : MonoBehaviour {

    private GameObject mPlayer;
    public GameObject mMarker;
    // Use this for initialization
    void Start()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTouchDown(Vector3 _postion)
    {
        if (mPlayer != null)
        {
            mPlayer.GetComponent<MainCharacterScript>().GoThere(_postion, false);
            if(mMarker)
            {
                Instantiate(mMarker, _postion, new Quaternion());
            }
        }
    }
}
