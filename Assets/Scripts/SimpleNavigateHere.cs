using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleNavigateHere : MonoBehaviour {

    private GameObject Player;
    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTouchDown(Vector3 _postion)
    {
        if (Player != null)
        {
            Player.GetComponent<MainCharacterScript>().GoThere(_postion, false);
        }
    }
}
