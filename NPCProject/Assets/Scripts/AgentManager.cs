using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : Singleton<AgentManager>
{   
    public Pirate piratePrefab;
    public Ally allyPrefab;

    public int pirateSpawnCount = 10;
    public int allySpawnCount = 10;

    public int currPirateCount = 10;
    public int currAllyCount = 10;

    private float spawnTime = 0;
    private float spawnDelay = 2f;

    private Vector3 spawnPos = Vector3.zero;

    public List<Ally> Allies = new List<Ally>();
    public List<Pirate> Pirates = new List<Pirate>();

    // Obstacle Vars

    public Obstacle obstaclePrefab;

    public int obstacleSpawnCount = 3;
    public int currObstacleCount = 3;

    public List<Obstacle> Obstacles = new List<Obstacle>();

    Vector3 cameraPosition;
    float cameraHalfHeight;
    float cameraHalfWidth;
    Vector2 cameraSize;

    public CollisionManager collisionManager;

    protected AgentManager() { }

    // Start is called before the first frame update
    void Start()
    {
        collisionManager = FindObjectOfType<CollisionManager>();

        cameraPosition = Camera.main.transform.position;

        cameraHalfHeight = Camera.main.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * Camera.main.aspect;

        cameraSize = new Vector2(cameraHalfWidth * 2f, cameraHalfHeight * 2f);

        // Spawn agents

        for (int i = 0; i < pirateSpawnCount; i++)
        {
            spawnPos.x = Random.Range(-cameraHalfWidth, cameraHalfWidth);
            spawnPos.y = Random.Range(-cameraHalfHeight, cameraHalfHeight);

            Pirate pirate = Instantiate<Pirate>(piratePrefab,
                spawnPos,
                Quaternion.identity);
            
            Pirates.Add(pirate);
            collisionManager.collidableObjects.Add(pirate.GetComponent<CollidableObject>());
        }
        for (int i = 0; i < allySpawnCount; i++)
        {
            spawnPos.x = Random.Range(-cameraHalfWidth, cameraHalfWidth);
            spawnPos.y = Random.Range(-cameraHalfHeight, cameraHalfHeight);

            Ally ally = Instantiate<Ally>(allyPrefab,
                spawnPos,
                Quaternion.identity);
           
            Allies.Add(ally);
            collisionManager.collidableObjects.Add(ally.GetComponent<CollidableObject>());
        }

        // Spawn obstacles
        
        for (int i = 0; i < obstacleSpawnCount; i++)
        {
            spawnPos.x = Random.Range(-cameraHalfWidth, cameraHalfWidth);
            spawnPos.y = Random.Range(-cameraHalfHeight, cameraHalfHeight);

            Obstacle obstacle = Instantiate<Obstacle>(obstaclePrefab,
                spawnPos,
                Quaternion.identity);
            
            Obstacles.Add(obstacle);
            collisionManager.collidableObjects.Add(obstacle.GetComponent<CollidableObject>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currPirateCount < 10)
        {
            spawnTime += Time.deltaTime;
            if (spawnTime > spawnDelay)
            {
                spawnPos.x = Random.Range(-cameraHalfWidth, cameraHalfWidth);
                spawnPos.y = Random.Range(-cameraHalfHeight, cameraHalfHeight);

                Pirate pirate = Instantiate<Pirate>(piratePrefab,
                    spawnPos,
                    Quaternion.identity);
                
                Pirates.Add(pirate);
                collisionManager.collidableObjects.Add(pirate.GetComponent<CollidableObject>());
                currPirateCount += 1;
                spawnTime = 0;
            }
        }
        if(currAllyCount < 10)
        {
            spawnTime += Time.deltaTime;
            if (spawnTime > spawnDelay)
            {
                spawnPos.x = Random.Range(-cameraHalfWidth, cameraHalfWidth);
                spawnPos.y = Random.Range(-cameraHalfHeight, cameraHalfHeight);

                Ally ally = Instantiate<Ally>(allyPrefab,
                    spawnPos,
                    Quaternion.identity);
                
                Allies.Add(ally);
                collisionManager.collidableObjects.Add(ally.GetComponent<CollidableObject>());
                currAllyCount += 1;
                spawnTime = 0;
            }
        }
        if(currObstacleCount < 3)
        {
            spawnTime += Time.deltaTime;
            if (spawnTime > spawnDelay)
            {
                spawnPos.x = Random.Range(-cameraHalfWidth, cameraHalfWidth);
                spawnPos.y = Random.Range(-cameraHalfHeight, cameraHalfHeight);

                Obstacle obstacle = Instantiate<Obstacle>(obstaclePrefab,
                spawnPos,
                Quaternion.identity);
            
                Obstacles.Add(obstacle);
                collisionManager.collidableObjects.Add(obstacle.GetComponent<CollidableObject>());
                currObstacleCount += 1;
                spawnTime = 0;
            }
        }
    }
}