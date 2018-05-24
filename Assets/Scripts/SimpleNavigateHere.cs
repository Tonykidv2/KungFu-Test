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
        if (mPlayer.GetComponent<MainCharacterScript>().mAttackingEnemy)
            ClearFieldofMarkers();
    }

    void OnTouchDown(Vector3 _postion)
    {
        if (mPlayer != null)
        {
            if (mPlayer.GetComponent<MainCharacterScript>().isAttackingEnemy())
                return;
            
            mPlayer.GetComponent<MainCharacterScript>().GoThere(_postion, false);
            if(mMarker)
            {
                ClearFieldofMarkers();

                //Instantiate new Marker at new position
                Instantiate(mMarker, _postion, new Quaternion());
            }
        }
    }

    void ClearFieldofMarkers()
    {
        //Delete any old marker that may still be in the screen
        GameObject[] markers = GameObject.FindGameObjectsWithTag("Marker");
        foreach (var mark in markers)
        {
            Destroy(mark);
        }
    }
}
