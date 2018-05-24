using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour {

    private GameObject mPlayer;
    public Color mLightUpAttackColor;
    private NavMeshAgent mNavMeshAgent;
    private Animator mAnimator;
    private float mRotationSpeed = 5f;
    private bool inFlock = true;
    private bool mWillCounter;
    private bool mLightOn = false;
    // Use this for initialization
    void Start () {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
        mNavMeshAgent = GetComponent<NavMeshAgent>();
        mAnimator = GetComponent<Animator>();
    }

	// Update is called once per frame
	void Update () {
        TurnTo();
        Vector3 velocity = GetComponent<Rigidbody>().velocity;
        mWillCounter = false;
        if (/*mNavMeshAgent.velocity != Vector3.zero ||*/ velocity.magnitude > .5f)
        {
            mAnimator.SetBool("Moving", true);
        }
        else
        {
            mAnimator.SetBool("Moving", false);
        }
	}

    void OnTouchDown(Vector3 _postion)
    {
        if (mPlayer != null)
        {
            Color color = GetComponent<shaderGlow>().glowColor;
            Color yellow = new Color(1, 1, 0);
            Color blue = new Color(0, 0, 1);
            if (color == yellow)
            {
                CounterAttackGlow();
                return;
            }
            PlayerAttackGlow();
        }
    }

    void OnTouchSwipe(Vector3 _postion)
    {
        if (mPlayer != null)
        {
            Color color = GetComponent<shaderGlow>().glowColor;
            Color yellow = new Color(1, 1, 0);
            Color blue = new Color(0, 0, 1);
            if (color == blue)
            {
                CounterAttackGlow();
                return;
            }
            PlayerAttackGlow();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(mWillCounter)
            mWillCounter = false;

        if(other.tag == "Hitter")
        {
            //Will uncomment once I have full control over animations
            //if (mPlayer.GetComponent<MainCharacterScript>().isApexAttack() == false)
            //      return;
            
            // force is how forcefully we will push the enemy away from the player.
            float force = 100;
            
            // Calculate Angle Between the collision point and the player
            Vector3 dir = mPlayer.transform.position - transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            GetComponent<Rigidbody>().AddForce(dir * force);
            inFlock = true;
            Invoke("DeactivateForce", .5f);
            ReturnToNormal();
        }
        
    }

    void TurnTo()
    {
        //find the vector pointing from our position to the target
        Vector3 _direction = (mPlayer.transform.position - transform.position).normalized;
        
        //create the rotation we need to be in to look at the target
        Quaternion _lookRotation = Quaternion.LookRotation(_direction);
        
        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * mRotationSpeed);
    }

    void TurnOffGlow()
    {
        //if (!mLightOn)
        //    return;
        //mLightOn = false;
        GetComponent<shaderGlow>().lightOff();
        GetComponent<shaderGlow>().glowColor = Color.green;
    }

    void CounterAttackGlow()
    {
        TurnOffGlow();
        GetComponent<shaderGlow>().glowColor = Color.red;
        GetComponent<shaderGlow>().lightOn();
        inFlock = false;
        Invoke("TurnOffGlow", 1);
        mWillCounter = true;
    }

    void PlayerAttackGlow()
    {
        mPlayer.GetComponent<MainCharacterScript>().GoThere(transform.position, true);
        TurnOffGlow();
        GetComponent<shaderGlow>().lightOn();
        inFlock = false;
        Invoke("TurnOffGlow", 1);
    }

    void DeactivateForce()
    {
        Debug.Log("Attempting to Stop Enemy Stagger");

        GetComponent<Rigidbody>().velocity = Vector3.zero;
        //GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    public bool IsInFlock()
    {
        return inFlock;
    }

    public void GetReadyToAttack()
    {
        //Remove from flock behavior
        //Light up to Signify It's going to fight
        //Run up to attack 
        //Return to Flock afterwards
        inFlock = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        LightOnAttack();
        Invoke("ReturnToNormal", 5);
    }

    void LightOnAttack()
    {
        GetComponent<shaderGlow>().glowColor = mLightUpAttackColor;
        GetComponent<shaderGlow>().lightOn();
        mLightOn = true;
        //Later in Dev. Enemy will do some animation before attacking then run up to attack
        Invoke("TurnOffGlow", 5);

    }

    //To be deleted later
    void ReturnToNormal()
    {
        inFlock = true;
        mWillCounter = false;
    }
}
