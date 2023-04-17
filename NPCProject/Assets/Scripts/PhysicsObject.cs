using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public Vector3 velocity;

    public Vector3 position;
    public Vector3 direction;
    private Vector3 acceleration;

    public float mass = 1f;

    public float radius = 1f;

    private Camera cam;
    private float height;
    private float width;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        height = cam.orthographicSize;
        width = height * cam.aspect;
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        position = transform.position;
        velocity += acceleration * Time.deltaTime;
        direction = velocity.normalized;

        position += velocity * Time.deltaTime;

        transform.position = position;

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.back, direction);
        }

        acceleration = Vector3.zero;
    }

    public void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    void CheckForBounce()
    {
        // Check if past right edge
        if (position.x > width && velocity.x > 0)
        {
            velocity.x *= -1f;
        }
        // Check if past left edge
        if (position.x < -width && velocity.x < 0)
        {
            velocity.x *= -1f;
        }
        // Check if past top edge
        if (position.y > height && velocity.y > 0)
        {
            velocity.y *= -1f;
        }
        // Check if past bottom edge
        if (position.y < -height && velocity.y < 0)
        {
            velocity.y *= -1f;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(gameObject.transform.position, radius);
    }
}
