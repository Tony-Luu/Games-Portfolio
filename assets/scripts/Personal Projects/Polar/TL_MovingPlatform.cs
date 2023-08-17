using UnityEngine;
using Polar.Obstacles;

public class TL_MovingPlatform : MonoBehaviour
{
    public GameObject Player;
    public int WaypointIndex = 0;
    public float PlatformSpeed;
    public Waymark[] Waypoints;
    public Vector3 PlatformDirection;
    private Rigidbody PlayerRigidbody;
    private Rigidbody PlatformRigidbody;


    void Start()
    {
        PlatformRigidbody = GetComponent<Rigidbody>();
    }

    //Make the platform follow a waypoint
    void FollowWaypoint()
    {
        var waypointPosition = Waypoints[WaypointIndex].transform.position;

        //Calculate the direction of the next waypoint
        PlatformDirection = waypointPosition - transform.position;

        //If the platform is close enough to its' destination
        if (Vector3.Distance(transform.position, waypointPosition) < 1f)
        {
            //Increase the index
            WaypointIndex++;

            //If the index of the waypoint is more than the maximum length of the array
            if (WaypointIndex > Waypoints.Length - 1)
            {
                //Reset the index
                WaypointIndex = 0;
            }
        }
    }

    //Move the platform
    void MovePlatform()
    {
        PlatformRigidbody.MovePosition(Vector3.Lerp(transform.position, Waypoints[WaypointIndex].transform.position, PlatformSpeed * Time.deltaTime));
    }

    void Update()
    {
        FollowWaypoint();
    }

    void FixedUpdate()
    {
        MovePlatform();
    }

    void OnCollisionStay(Collision collision)
    {
        //If the collided gameobject is a player
        if (collision.transform.CompareTag("Player"))
        {
            //Obtain the rigidbody from the player
            PlayerRigidbody = collision.transform.GetComponent<Rigidbody>();

            //Set the player's velocity to match the platform
            PlayerRigidbody.velocity = new Vector3(PlatformRigidbody.velocity.x, PlayerRigidbody.velocity.y, PlatformRigidbody.velocity.z);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(PlayerRigidbody == null)
            {
                //Obtain the rigidbody from the player
                PlayerRigidbody = collision.transform.GetComponent<Rigidbody>();
            }

            PlayerRigidbody.velocity = new Vector3(0, PlayerRigidbody.velocity.y, 0);
        }
    }

}