using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CollisionManager : MonoBehaviour
{
    public List<CollidableObject> collidableObjects = new List<CollidableObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (CollidableObject collidableObject in collidableObjects)
        {
            collidableObject.ResetCollision();
        }

        for (int i = 0; i < collidableObjects.Count; i++)
        {
            for (int j = i + 1; j < collidableObjects.Count; j++)
            {
                if(collidableObjects[i] != null && collidableObjects[j] != null)
                {
                    if (CollisionCheck(collidableObjects[i], collidableObjects[j]))
                    {
                        collidableObjects[i].RegisterCollision(collidableObjects[j]);
                        collidableObjects[j].isCurrentlyColliding = true;
                    }
                }
            }
        }
    }

    bool CollisionCheck(CollidableObject objA, CollidableObject objB)
    {
        float radiusA = objA.GetComponent<PhysicsObject>().radius;
        float radiusB = objB.GetComponent<PhysicsObject>().radius;

        Bounds boundsA = objA.GetComponent<SpriteRenderer>().bounds;
        Bounds boundsB = objB.GetComponent<SpriteRenderer>().bounds;

        float radiusTotal = radiusA + radiusB;

        float x1 = boundsA.center.x;
        float y1 = boundsA.center.y;
        float x2 = boundsB.center.x;
        float y2 = boundsB.center.y;

        float distance = (float)Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2) * 1.0);

        if (distance < radiusTotal)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
