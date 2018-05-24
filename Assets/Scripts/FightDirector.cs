using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightDirector : MonoBehaviour {

    public List<GameObject> mFighters;
    public GameObject mPlayer;
    public float AttackTokenCooldown;
    private static float ResetTimer = 5;
	// Use this for initialization
	void Start () {
        mFighters.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        mPlayer = GameObject.FindGameObjectWithTag("Player");
        AttackTokenCooldown = ResetTimer;
	}
	
	// Update is called once per frame
	void Update () {
        AttackTokenCooldown -= Time.deltaTime;

        if(AttackTokenCooldown < 0f)
        {
            int numTries = 3;
            while(true)
            {
                int random = Random.Range(0, mFighters.Count);
                if(mFighters[random].GetComponent<EnemyScript>().IsInFlock())
                {
                    mFighters[random].GetComponent<EnemyScript>().GetReadyToAttack();
                    break;
                }
                numTries--;
                if (numTries <= 0)
                    break;
            }
            AttackTokenCooldown = ResetTimer;
        }
	}

}
