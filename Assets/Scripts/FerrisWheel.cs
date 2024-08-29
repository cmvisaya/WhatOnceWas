using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FerrisWheel : MonoBehaviour
{

    public Transform wheel;
    public Transform[] cabins;
    public float speed;
    public Vector3 riderOffset;
    public Transform getoffPoint;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        wheel.Rotate(0, 0, speed);
        foreach(Transform cabin in cabins) {
            cabin.Rotate(0, 0, -speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Transform toRide = null;
            foreach(Transform cabin in cabins) {
                if(toRide == null || cabin.position.y <= toRide.position.y) {
                    toRide = cabin;
                }
            }
            GameObject playerGO = GameObject.Find("Player");
            PlayerController1 player = playerGO.GetComponent<PlayerController1>();
            if(player.isRiding == false) {
                playerGO.transform.SetParent(toRide);
                playerGO.transform.position = toRide.position + riderOffset;
                playerGO.transform.localEulerAngles = new Vector3(0, 0, 0);
                player.playerModel.transform.localEulerAngles = new Vector3(0, 0, 0);
                player.hasControl = false;
                player.isRiding = true;
                player.rideID = 1;
                player.rideGetoffPoint = getoffPoint;
            }
        }
    }
}
