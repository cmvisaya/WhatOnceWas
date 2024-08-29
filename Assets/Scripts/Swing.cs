using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{

    public Transform wheel;
    public Transform riderHorse;
    public float accel;
    [SerializeField] private float speed;
    private bool forward = true;
    public float maxSpeed;
    public float deadzone = 0.3f;
    public Vector3 riderOffset;
    public Transform getoffPoint;

    // Start is called before the first frame update
    void Start()
    {
        speed = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if((speed > maxSpeed && forward) || (speed < -maxSpeed && !forward)) { accel = -accel; forward = !forward; }
        speed += accel;
        wheel.Rotate(0, 0, speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject playerGO = GameObject.Find("Player");
            PlayerController1 player = playerGO.GetComponent<PlayerController1>();
            if(player.isRiding == false) {
                playerGO.transform.SetParent(riderHorse);
                playerGO.transform.position = riderHorse.position + riderOffset;
                playerGO.transform.localEulerAngles = new Vector3(0, 0, 0);
                player.playerModel.transform.localEulerAngles = new Vector3(0, 0, 0);
                player.hasControl = false;
                player.isRiding = true;
                player.rideID = 0;
                player.rideGetoffPoint = getoffPoint;
            }
        }
    }
}
