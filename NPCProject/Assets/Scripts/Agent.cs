using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicsObject))]
public abstract class Agent : MonoBehaviour
{
    public PhysicsObject physicsObject;

    public float maxSpeed = 5f;
    public float maxForce = 2f;
    
    public float wanderAngle = 0f;
    public float maxWanderAngle = 45f;
    public float maxWanderChangePerSecond = 10f;

    private Vector3 circleCenter;
    private Vector3 displacement;
    protected Vector3 wanderForce;

    protected Vector3 totalForce;

    public float radius = 1.5f;

    private Camera cam;
    private float height;
    private float width;

    public float personalSpace = 1f;
    
    public float visionRange = 4f;

    public bool foundLeader = false;

    protected List<Vector3> foundObstaclePositions = new List<Vector3>();

    public abstract void CalculateSteeringForces();

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        height = cam.orthographicSize;
        width = height * cam.aspect;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CalculateSteeringForces();
        totalForce = Vector3.ClampMagnitude(totalForce, maxForce);
        physicsObject.ApplyForce(totalForce);

        if(this.GetComponent<PirateLeader>() == null) {
            if (foundLeader)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
    }

    public Vector3 Seek(Vector3 targetPos)
    {
        // Calculate desired velocity
        Vector3 desiredVelocity = targetPos - transform.position;

        // Set desired = max speed
        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        // Calculate seek steering force
        Vector3 seekingForce = desiredVelocity - physicsObject.velocity;

        // Return seek steering force
        return seekingForce;
    }

    public Vector3 Flee(Vector3 targetPos)
    {
        // Calculate desired velocity
        Vector3 desiredVelocity = transform.position - targetPos;

        // Set desired = max speed
        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        // Calculate seek steering force
        Vector3 fleeingForce = desiredVelocity - physicsObject.velocity;

        // Return seek steering force
        return fleeingForce;
    }

    public Vector3 Wander(float angle = 0f)
    {
        circleCenter = physicsObject.velocity.normalized * radius;
        displacement = new Vector3(0, -1, 0) * radius;
        displacement.x = Mathf.Cos(wanderAngle) * radius;
        displacement.y = Mathf.Sin(wanderAngle) * radius;
        wanderAngle += Random.Range(-maxWanderChangePerSecond, maxWanderChangePerSecond);
        wanderAngle = Mathf.Clamp(wanderAngle, -maxWanderAngle, maxWanderAngle);
        wanderForce = circleCenter + displacement;
        return wanderForce;
    }

    public Vector3 StayInBounds()
    {
        Vector3 futurePos = CalculateFuturePosition();

        if ((futurePos.x > width && physicsObject.velocity.x > 0) ||
            (futurePos.x < -width && physicsObject.velocity.x < 0) ||
            (futurePos.y > height && physicsObject.velocity.y > 0) ||
            (futurePos.y < -height && physicsObject.velocity.y < 0))
        {
            return Seek(Vector3.zero);
        }

        return Vector3.zero;
    }

    public Vector3 CalculateFuturePosition(float time = 1f)
    {
        return physicsObject.position + physicsObject.velocity * time;
    }

    public Vector3 Seperate<T>(List<T> agents) where T : Agent
    {
        Vector3 totalSeperateForce = Vector3.zero;
        float sqrPersonalSpace = Mathf.Pow(personalSpace, 2);

        foreach(T other in agents){
            float sqrDistance = Vector3.SqrMagnitude(other.physicsObject.position - physicsObject.position);

            if(sqrDistance < float.Epsilon)
            {
                continue;
            }

            if(sqrDistance < sqrPersonalSpace)
            {
                float weight = sqrPersonalSpace / (sqrDistance + 0.1f);
                totalSeperateForce += Flee(other.physicsObject.position) * weight;
            }
        }
        
        return totalSeperateForce;
    }

    public Vector3 Evade<T>(List<T> agents) where T : Agent
    {
        Vector3 totalEvadeForce = Vector3.zero;
        float sqrPersonalSpace = Mathf.Pow(personalSpace, 2);

        foreach(T other in agents){
            float sqrDistance = Vector3.SqrMagnitude(other.physicsObject.position - physicsObject.position);

            if(sqrDistance < float.Epsilon)
            {
                continue;
            }

            if(sqrDistance < sqrPersonalSpace)
            {
                float weight = sqrPersonalSpace / (sqrDistance + 0.1f);
                totalEvadeForce += Flee(other.physicsObject.position) * weight;
            }
        }
        
        return totalEvadeForce;
    }

    public Vector3 Pursue<T>(List<T> agents) where T : Agent
    {
        Vector3 totalPursueForce = Vector3.zero;
        float sqrPersonalSpace = Mathf.Pow(personalSpace, 2);

        foreach(T other in agents){
            float sqrDistance = Vector3.SqrMagnitude(other.physicsObject.position - physicsObject.position);

            if(sqrDistance < float.Epsilon)
            {
                continue;
            }

            if(sqrDistance < sqrPersonalSpace)
            {
                float weight = sqrPersonalSpace / (sqrDistance + 0.1f);
                totalPursueForce += Seek(other.physicsObject.position) * weight;
            }
        }
        
        return totalPursueForce;
    }

    public void FindLeader<T>(List<T> objects) where T : CollidableObject
    {
        float sqrPersonalSpace = Mathf.Pow(personalSpace, 2);

        foreach(T other in objects){
            if (other != null)
            {
                float sqrDistance = Vector3.SqrMagnitude(other.GetComponent<PhysicsObject>().position - physicsObject.position);

                if (sqrDistance < float.Epsilon)
                {
                    continue;
                }

                if (sqrDistance < sqrPersonalSpace)
                {
                    if (other.GetComponent<Player>() != null && this.GetComponent<Ally>() != null)
                    {
                        foundLeader = true;
                    }
                    if(other.GetComponent<PirateLeader>() != null && this.GetComponent<Pirate>() != null)
                    {
                        foundLeader = true;
                    }
                }
            }
        }
    }

    public Vector3 AvoidObstacles()
    {
        foundObstaclePositions.Clear();

        // Vector from agent to obstacle
        Vector3 AtoO = Vector3.zero;
        float forwardDot = Vector3.Dot(AtoO, physicsObject.direction);
        float rightDot = Vector3.Dot(physicsObject.transform.right, physicsObject.direction);
        Vector3 desiredVelocity = Vector3.zero;
        Vector3 finalForce = Vector3.zero;

        foreach (Obstacle obstacle in AgentManager.Instance.Obstacles)
        {
            if (obstacle != null)
            {
                AtoO = obstacle.transform.position - transform.position;

                forwardDot = Vector3.Dot(AtoO, physicsObject.direction);
                rightDot = Vector3.Dot(physicsObject.transform.right, physicsObject.direction);

                // Checks
                if (forwardDot >= -(obstacle.radius + physicsObject.radius)
                    && Mathf.Abs(rightDot) < physicsObject.radius + obstacle.radius
                    && forwardDot < visionRange)
                {
                    foundObstaclePositions.Add(obstacle.transform.position);

                    if (rightDot > 0)
                    {
                        desiredVelocity = physicsObject.transform.right * -maxSpeed;
                    }
                    else
                    {
                        desiredVelocity = physicsObject.transform.right * maxSpeed;
                    }

                    float weight = visionRange / (forwardDot + 0.1f);

                    Vector3 steeringForce = (desiredVelocity - physicsObject.velocity) * weight;

                    finalForce += steeringForce;
                }
            }
        }

        return finalForce;
    }
}
