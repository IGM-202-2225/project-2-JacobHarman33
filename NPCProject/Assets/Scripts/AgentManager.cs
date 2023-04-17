using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : Singleton<AgentManager>
{   
    public Agent agentPrefab;

    public int agentSpawnCount = 20;

    public List<Agent> Agents = new List<Agent>();

    Vector3 cameraPosition;
    float cameraHalfHeight;
    float cameraHalfWidth;
    Vector2 cameraSize;

    protected AgentManager() { }

    // Start is called before the first frame update
    void Start()
    {
        cameraPosition = Camera.main.transform.position;

        cameraHalfHeight = Camera.main.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * Camera.main.aspect;

        cameraSize = new Vector2(cameraHalfWidth * 2f, cameraHalfHeight * 2f);

        Vector3 spawnPos = Vector3.zero;

        for (int i = 0; i < agentSpawnCount; i++)
        {
            spawnPos.x = Random.Range(-cameraHalfWidth, cameraHalfWidth);
            spawnPos.y = Random.Range(-cameraHalfHeight, cameraHalfHeight);

            Agents.Add(Instantiate<Agent>(agentPrefab,
                spawnPos,
                Quaternion.identity));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}