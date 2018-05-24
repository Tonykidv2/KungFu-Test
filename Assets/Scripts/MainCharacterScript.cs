using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MainCharacterScript : MonoBehaviour {


    private Animator mAnimator;
    public NavMeshAgent mNavMeshAgent;
    private Vector3 mEnemyPosition;
    public float mRotSpeed = 7f;
    public bool mAttackingEnemy = false;

    //this value will change for AR App later
    //Must match stopping distance from NavMeshAgent
    public float mStoppingDistance = 1.5f;

	// Use this for initialization
	void Start () {
        mAnimator = GetComponent<Animator>();
        mNavMeshAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        if (mNavMeshAgent.velocity != Vector3.zero)
        {
            mAnimator.SetBool("Moving", true);
            InstantlyTurn(mNavMeshAgent.destination);
		}
            
        else
            mAnimator.SetBool("Moving", false);

        if(mAttackingEnemy)
        {
            if(Vector3.Distance(mNavMeshAgent.destination, transform.position) < mStoppingDistance)
            {
                mNavMeshAgent.isStopped = true;
                AttackEnemyBehavior();
            }
        }
	}

    public void GoThere(Vector3 _Destination, bool _isEnemy){
        
        mNavMeshAgent.SetDestination(_Destination);
        mAttackingEnemy = _isEnemy;
        if (mAttackingEnemy)
            mEnemyPosition = _Destination;
    }

    void InstantlyTurn(Vector3 destination)
    {
        //When on target -> dont rotate!
        if ((destination - transform.position).magnitude < 0.1f) return;

        Vector3 direction = (destination - transform.position).normalized;
        Quaternion qDir = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, qDir, Time.deltaTime * mRotSpeed);
    }

    void AttackEnemyBehavior()
    {
        mNavMeshAgent.SetDestination(transform.position);
        mNavMeshAgent.isStopped = true;
        mAttackingEnemy = false;
        //////Enter Attack Logic Here///////
        transform.LookAt(mEnemyPosition);
        mAnimator.SetTrigger("Attack1Trigger");
        //////More Attack Logic/////////////
        ////////////////////////////////////

        mNavMeshAgent.isStopped = false;
    }

    public bool isAttackingEnemy()
    {
        return mAttackingEnemy;
    }
}