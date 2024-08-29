using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    public float accel;
    public float deccel;
    public float gravityScale;
    public Transform spawnLocation;

    public CharacterController controller;
    [SerializeField] private Vector3 moveDirection;
    private Vector3 retainedDirection;

    public Transform pivot;
    public float rotateSpeed;

    public GameObject playerModel;

    public bool onBoat = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (GameObject.Find("Pivot"))
        {
            pivot = GameObject.Find("Pivot").transform;
        }
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (onBoat)
        {
            float vertInput = Input.GetAxis("Vertical");
            float horizInput = Input.GetAxis("Horizontal");
            moveDirection = (pivot.forward * vertInput) + (pivot.right * horizInput);
            if(Mathf.Abs(vertInput) > 0 || Mathf.Abs(horizInput) > 0) {
                speed += accel;
                if(speed > maxSpeed) { speed = maxSpeed; }
                moveDirection = moveDirection.normalized * speed;
                retainedDirection = moveDirection;
            } else {
                speed -= deccel;
                moveDirection = retainedDirection.normalized * speed;
                if(speed < 0) { speed = 0; }
            }
            moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);

            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                //transform.rotation = Quaternion.Euler(0f, pivot.localEulerAngles.y, 0f);
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            }

            controller.Move(moveDirection * Time.deltaTime);

        }


        if(Input.GetButton("Continue") && onBoat) {
            onBoat = false;
            controller.enabled = false;
            transform.position = spawnLocation.position;
            controller.enabled = true;
        }
    }
}
