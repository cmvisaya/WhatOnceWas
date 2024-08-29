using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carousel : MonoBehaviour
{

    public Transform wheel;
    public Transform riderHorse;
    public float speed;
    public Vector3 riderOffset;
    public Transform getoffPoint;

    // Start is called before the first frame update
    void Start()
    {
    }

    /* Update is called once per frame
    void Update()
    {
        wheel.Rotate(0, -speed, 0);
    } */

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
                player.rideID = 0;
                player.rideGetoffPoint = getoffPoint;
            }
        }
    }
}
