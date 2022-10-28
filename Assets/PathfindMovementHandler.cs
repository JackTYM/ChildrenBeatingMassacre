using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;

public class PathfindMovementHandler : MonoBehaviour
{
    public Transform targetPosition;

    private Seeker seeker;
    private CharacterController controller;

    public Path path;

    public float speed = 2;

    public float nextWaypointDistance = 3;

    private int currentWaypoint = 0;

    public bool reachedEndOfPath;

    // Start is called before the first frame update
    void Start()
    {
        //Gets the seeker AI component
        Seeker seeker = GetComponent<Seeker>();

        //Gets the character controller
        controller = GetComponent<CharacterController>();

        //Starts a path calculation
        seeker.StartPath(transform.position, targetPosition.position, OnPathComplete);  
    }

    void OnPathComplete (Path p) {
        Debug.Log("Finished path. Error: " + p.error);

        if (!p.error) {
            path = p;

            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (path == null) return; //No path yet

        reachedEndOfPath = false;

        float distToNextPoint;
        while (true) {
            distToNextPoint = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
            if (distToNextPoint < nextWaypointDistance) {
                if (currentWaypoint + 1 < path.vectorPath.Count) {
                    currentWaypoint++;
                } else {
                    reachedEndOfPath = true;
                    break;
                }
            } else {
                break;
            }
        }

        //Slows down when closer to target
        var speedFactor = reachedEndOfPath ? Mathf.Sqrt(distToNextPoint/nextWaypointDistance) : 1f;

        //Direction to next point
        Vector3 direction = (path.vectorPath[currentWaypoint] - transform.position).normalized;

        Vector3 velocity = direction * speed * speedFactor;

        controller.SimpleMove(velocity);
    }
}
