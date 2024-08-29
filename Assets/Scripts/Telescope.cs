using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telescope : MonoBehaviour
{

    public Vector3 cameraOffset;
    public Vector3 riderOffset;
    public Transform riderHorse;
    public Transform getoffPoint;
    public GameObject camera;
    private CameraController cam;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = camera.GetComponent<CameraController>();
    }

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

                cam.inTelescope = true;
                cam.offset = cameraOffset;
            }
        }
    }
}
