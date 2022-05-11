using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class HandleMovement : MonoBehaviour
{
    public Rigidbody _rb;
    public PathCreator pathCreator;
    public bool usePath;
    private float speed;

    public PlayerRB playerRB;
    // private AnotherScript anotherScript;

    private void Awake()
    {
        playerRB = GetComponent<PlayerRB>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        speed = playerRB.speed;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        Vector3 tangent = pathCreator.path.GetDirectionAtDistance(distance);
        transform.right = tangent;

        Vector3 correction = pathCreator.path.GetPointAtDistance(distance) - transform.position;
        transform.position += new Vector3(correction.x, 0f, correction.z);
    }

    void FixedUpdate()
    {
        Vector2 movementXY = playerRB.move;
        Vector3 movementXYZ = new Vector3(movementXY.x, movementXY.y, 0f);
        _rb.MovePosition(transform.position + Time.deltaTime * speed * movementXYZ);
    }
}
