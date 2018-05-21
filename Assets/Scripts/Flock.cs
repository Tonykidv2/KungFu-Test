using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour {

    public float AlignmentStrength;
    public float CohesionStrength;
    public float SeparationStrength;
    public List<GameObject> Boids;
    public Vector3 AveragePosition;
    protected Vector3 AverageForward;
    public float FlockRadius;

	// Use this for initialization
	void Start () {
        Boids.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        Boids.Add(GameObject.FindGameObjectWithTag("Player"));
	}
	
	// Update is called once per frame
	void Update () {
        
        CalculateAverages();

        foreach (var Boid in Boids)
        {
            if (Boid.tag == "Player")
                continue;
            Vector3 accel = Vector3.zero;
            accel += CalculateCohesionAcceleration(Boid);
            accel += CalculateSeparationAcceleration(Boid);
            float accelMultiplier = 3; //Objects MaxSpeed

            accel *= accelMultiplier * Time.deltaTime;

            //Boids[i].Velocity += accel;
            Boid.GetComponent<Rigidbody>().AddForce(accel);

            if (Boid.GetComponent<Rigidbody>().velocity.magnitude > 3) // 3 == Objects MaxSpeed
            {
                Boid.GetComponent<Rigidbody>().velocity.Normalize();
                Boid.GetComponent<Rigidbody>().AddForce(Boid.GetComponent<Rigidbody>().velocity * 3); // 3 == Objects MaxSpeed
            }
            //Boids[i].Update(deltaTime);
        }

        return;
	}

    private void CalculateAverages()
    {
        Vector3 sumForward = Vector3.zero;
        Vector3 sumPosition = Vector3.zero;
        int count = 0;
        foreach (var Boid in Boids)
        {
            count++;
            sumPosition += Boid.transform.position;
            sumForward += Boid.GetComponent<Rigidbody>().velocity;
        }

        sumForward /= count;
        sumPosition /= count;

        AveragePosition = sumPosition;
        AverageForward = sumForward;

    }

    private Vector3 CalculateCohesionAcceleration(GameObject boid)
    {

        //Vector3 vec = new Vector3(0, 0, 0);

        Vector3 vec = AveragePosition - boid.transform.position;

        float distance = vec.magnitude;

        vec.Normalize();

        if (distance < FlockRadius)
            vec *= distance / FlockRadius;


        return vec * CohesionStrength;
    }

    private Vector3 CalculateSeparationAcceleration(GameObject boid)
    {
        
        Vector3 sum = Vector3.zero;

        foreach (var Boid in Boids)
        {
            if (Boid == boid)
                continue;

            Vector3 vec = boid.transform.position - Boid.transform.position;
            float distance = vec.magnitude;
            float safeDistance = CalculateSafeRadius(boid.tag) + CalculateSafeRadius(Boid.tag);

            if (distance < safeDistance)
            {
                vec.Normalize();
                vec *= (safeDistance - distance) / safeDistance;
                sum += vec;
            }
        }

        if (sum.magnitude > 1.0f)
        {
            sum.Normalize();
        }

        return sum * SeparationStrength;
    }

    private float CalculateSafeRadius(string Tag)
    {
        switch (Tag)
        {
            case "Player":
                return 5;
            case "Enemy":
                return 2;
            default:
                return 2;
        }

    }


}
