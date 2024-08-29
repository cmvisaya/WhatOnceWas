using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCanRide : MonoBehaviour
{

    public GameObject propeller;
    public GameObject ride;
    public Transform riderHorse;
    public Vector3 riderOffset;
    public Transform getoffPoint;

    // Start is called before the first frame update
    void Start()
    {
        propeller.GetComponent<Animator>().enabled = false;
        ride.GetComponent<Animator>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            propeller.GetComponent<Animator>().enabled = true;
            ride.GetComponent<Animator>().enabled = true;
            GameObject playerGO = GameObject.Find("Player");
            PlayerController1 player = playerGO.GetComponent<PlayerController1>();
            if(!player.isRiding) {
                playerGO.transform.SetParent(riderHorse);
                playerGO.transform.position = riderHorse.position + riderOffset;
                playerGO.transform.localEulerAngles = new Vector3(0, 0, 0);
                player.playerModel.transform.localEulerAngles = new Vector3(0, 0, 0);
                player.hasControl = false;
                player.isRiding = true;
                player.rideID = 2;
                player.rideGetoffPoint = getoffPoint;
            }
        }
    }
}
