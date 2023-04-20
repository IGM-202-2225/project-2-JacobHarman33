using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wanderer : Agent
{
    public float boundsScalar = 2f;
    public float wanderScalar = 2f;
    public float avoidanceScalar = 1f;

    private float delayTime = 0.5f;
    private float wanderTime;

    private Vector3 boundsForce;

    public float avoidanceFutureTime = 2f;

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
            totalForce += AvoidObstacles() * avoidanceScalar;
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw avoidance safety square
        Gizmos.color = Color.green;
        Vector3 futurePos = CalculateFuturePosition(2f);
        float dist = Vector3.Distance(transform.position, futurePos);
        Vector3 boxSize = new Vector3(physicsObject.radius * 2f,
                                        dist + physicsObject.radius,
                                        physicsObject.radius * 2f);
        Vector3 boxCenter = Vector3.zero;
        boxCenter.y += boxSize.y / 2f;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireCube(boxCenter, boxSize);
        Gizmos.matrix = Matrix4x4.identity;

        Gizmos.color = Color.yellow;
        foreach (Vector3 pos in foundObstaclePositions) 
        { 
            Gizmos.DrawLine(transform.position, pos);    
        }

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, physicsObject.velocity);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, wanderForce);

        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(transform.position, boundsForce);
    }
}
