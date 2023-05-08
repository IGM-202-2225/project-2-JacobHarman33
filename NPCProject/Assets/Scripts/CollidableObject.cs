using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidableObject : MonoBehaviour
{
    public readonly List<CollidableObject> collisions = new List<CollidableObject>();
    public PointsManager pointsManager;
    public AgentManager agentManager;
    public bool isCurrentlyColliding = false;

    // Start is called before the first frame update
    void Start()
    {
        pointsManager = FindObjectOfType<PointsManager>();
        agentManager = FindObjectOfType<AgentManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (collisions.Count != 0)
        {
            if (isCurrentlyColliding)
            {
                if (collisions[0] != null && gameObject.GetComponent<Player>() != null && collisions[0].GetComponent<Pirate>() != null)
                {
                    pointsManager.allyPointsNum += 1;
                    Destroy(collisions[0].gameObject);
                    agentManager.currPirateCount -= 1;
                }
                if (collisions[0] != null && gameObject.GetComponent<PirateLeader>() != null && collisions[0].GetComponent<Ally>() != null)
                {
                    pointsManager.piratePointsNum += 1;
                    Destroy(collisions[0].gameObject);
                    agentManager.currAllyCount -= 1;
                }
                if (collisions[0] != null && gameObject.GetComponent<Pirate>() != null && collisions[0].GetComponent<Ally>() != null)
                {
                    pointsManager.piratePointsNum += 1;
                    Destroy(collisions[0].gameObject);
                    agentManager.currAllyCount -= 1;
                }
                if (collisions[0] != null && gameObject.GetComponent<Ally>() != null && collisions[0].GetComponent<Pirate>() != null)
                {
                    pointsManager.allyPointsNum += 1;
                    Destroy(collisions[0].gameObject);
                    agentManager.currPirateCount -= 1;
                }
                if (collisions[0] != null && collisions[0].GetComponent<Obstacle>() != null)
                {
                    if(gameObject.GetComponent<Ally>() != null)
                    {
                        pointsManager.piratePointsNum += 1;
                        Destroy(collisions[0].gameObject);
                        Destroy(gameObject);
                        agentManager.currAllyCount -= 1;
                        agentManager.currObstacleCount -= 1;
                    }
                    else if (gameObject.GetComponent<Pirate>() != null)
                    {
                        pointsManager.allyPointsNum += 1;
                        Destroy(collisions[0].gameObject);
                        Destroy(gameObject);
                        agentManager.currPirateCount -= 1;
                        agentManager.currObstacleCount -= 1;
                    }
                    else if (gameObject.GetComponent<Player>() != null)
                    {
                        pointsManager.piratePointsNum += 1;
                        Destroy(collisions[0].gameObject);
                        agentManager.currObstacleCount -= 1;
                    }
                    else if (gameObject.GetComponent<PirateLeader>() != null)
                    {
                        pointsManager.allyPointsNum += 1;
                        Destroy(collisions[0].gameObject);
                        agentManager.currObstacleCount -= 1;
                    }
                }
            }
        }
    }

    public void RegisterCollision(CollidableObject other)
    {
        isCurrentlyColliding = true;
        collisions.Add(other);
    }

    public void ResetCollision()
    {
        isCurrentlyColliding = false;
        collisions.Clear();
    }
}
