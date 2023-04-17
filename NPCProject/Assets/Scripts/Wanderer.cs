using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wanderer : Agent
{
    public float boundsScalar = 2f;
    public float wanderScalar = 2f;

    private float delayTime = 0.5f;
    private float wanderTime;

    private Vector3 boundsForce;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void CalculateSteeringForces()
    {
        if(Time.time > wanderTime)
        {
            wanderTime = Time.time + delayTime;
            totalForce = Wander() * wanderScalar;
            boundsForce = StayInBounds() * boundsScalar;
            totalForce += boundsForce;
            totalForce += Seperate(AgentManager.Instance.Agents);
        }
    }

    private void onDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, physicsObject.velocity);

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, wanderForce);

        Gizmos.color= Color.magenta;
        Gizmos.DrawRay(transform.position, boundsForce);
    }
}
