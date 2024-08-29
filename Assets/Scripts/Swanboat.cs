using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swanboat : MonoBehaviour
{
    public Transform riderHorse;
    public Vector3 riderOffset;
    public Transform getoffPoint;
    public Transform boat;
    public Transform spawnLocation;

    // Start is called before the first frame update
    void Start() {}

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject playerGO = GameObject.Find("Player");
            PlayerController1 player = playerGO.GetComponent<PlayerController1>();
            BoatController bc = boat.GetComponent<BoatController>();
            if(player.isRiding == false) {
                bc.enabled = false;
                boat.transform.position = spawnLocation.position;
                bc.enabled = true;
                playerGO.transform.SetParent(riderHorse);
                playerGO.transform.position = riderHorse.position + riderOffset;
                playerGO.transform.localEulerAngles = new Vector3(0, 0, 0);
                player.playerModel.transform.localEulerAngles = new Vector3(0, 0, 0);
                player.hasControl = false;
                player.isRiding = true;
                player.rideID = 0;
                player.rideGetoffPoint = getoffPoint;
                bc.onBoat = true;
            }
        }
    }
}
