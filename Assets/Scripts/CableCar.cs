using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cablecar : MonoBehaviour
{

    public Transform riderHorse;
    public Vector3 riderOffset;
    public Transform getoffPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject playerGO = GameObject.Find("Player");
            PlayerController1 player = playerGO.GetComponent<PlayerController1>();
            if(!player.isRiding) {
                playerGO.transform.SetParent(riderHorse);
                playerGO.transform.position = riderHorse.position + riderOffset;
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
