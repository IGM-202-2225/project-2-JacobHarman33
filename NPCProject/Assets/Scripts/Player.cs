using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public CollisionManager collisionManager;
    private Vector3 playerPosition = new Vector3(0, 0, 0);
    public float turnSpeed = 5f;
    public Vector2 direction = Vector2.right;
    public Vector2 velocity = Vector2.zero;
    private Vector2 movementInput;

    private Camera cam;
    private float height;
    private float width;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        direction = movementInput;
        velocity = direction * turnSpeed * Time.deltaTime;
        transform.position += (Vector3)velocity;
        playerPosition = transform.position;

        // 7.5 is buffer for screen width, tailored for the build site
        if (playerPosition.x > width - 7.5f || playerPosition.x < -(width - 7.5f))
        {
            playerPosition.x = -playerPosition.x;
            transform.position = playerPosition;
        }

        // 4.5 is buffer for screen height, tailored for the build site
        if (playerPosition.y > height - 4.5f || playerPosition.y < -(height - 4.5f))
        {
            playerPosition.y = -playerPosition.y;
            transform.position = playerPosition;
        }

        if (direction != Vector2.zero)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.back, direction);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }
}
