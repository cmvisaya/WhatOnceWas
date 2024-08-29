using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController1 : MonoBehaviour
{

    public float moveSpeed;
    public float sprintSpeed;
    public float slopeSpeed;
    public float speed;
    public float accel = 0.5f;
    public float jumpForce;
    public float gravityScale;

    public CharacterController controller;
    private Vector3 moveDirection;

    public Animator anim;
    public Animator anim2;

    public Transform pivot;
    public float rotateSpeed;

    public GameObject playerModel;
    public GameObject playerModel2;

    private bool grounded = false;

    private bool willSlideOnSlopes = true;
    private Vector3 hitPointNormal;
    private bool isSliding
    {
        get
        {
            if (grounded && Physics.Raycast(transform.position, Vector3.down, out RaycastHit slopeHit, 2f))
            {
                hitPointNormal = slopeHit.normal;
                return Vector3.Angle(hitPointNormal, Vector3.up) > controller.slopeLimit;
            }
            else
            {
                return false;
            }
        }
    }

    public bool hasControl = true;
    public bool jumpEnabled = true;
    public bool sprintEnabled = true;
    public bool isRiding = false;
    public Transform rideGetoffPoint;
    public float rideID;

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
        if (hasControl)
        {
            float yStore = moveDirection.y;
            float vertInput = Input.GetAxis("Vertical");
            float horizInput = Input.GetAxis("Horizontal");
            moveDirection = (pivot.forward * vertInput) + (pivot.right * horizInput);
            if(Mathf.Abs(vertInput) > 0 || Mathf.Abs(horizInput) > 0) {
                speed += accel;
                if (Input.GetButton("Sprint") && sprintEnabled)
                {
                    if(speed > sprintSpeed) { speed = sprintSpeed; }
                }
                else
                {
                    if(speed > moveSpeed) { speed = moveSpeed; }
                }
            } else {
                speed -= accel;
                if(speed < 0) { speed = 0; }
            }
            moveDirection = moveDirection.normalized * speed;
            moveDirection.y = yStore;

            if (controller.isGrounded)
            {
                moveDirection.y = -1f;
                if (Input.GetButton("Jump") && jumpEnabled)
                {
                    moveDirection.y = jumpForce;
                }
            }
            else if (grounded && Input.GetButtonDown("Jump")) //Account for "sliding" jump
            {
                moveDirection.y = jumpForce;
            }

            moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);

            //More sliding stuff?
            if (willSlideOnSlopes && isSliding)
            {
                moveDirection += new Vector3(hitPointNormal.x, -hitPointNormal.y, hitPointNormal.z) * slopeSpeed;
            }

            controller.Move(moveDirection * Time.deltaTime);

            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                //transform.rotation = Quaternion.Euler(0f, pivot.localEulerAngles.y, 0f);
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
                if(playerModel2 != null) {
                    playerModel2.transform.rotation = Quaternion.Slerp(playerModel2.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
                }
            }

            //Distance to ground

            RaycastHit hit = new RaycastHit();
            var distanceToGround = 0f; ;
            if (Physics.Raycast(transform.position, -Vector3.up, out hit))
            {
                distanceToGround = hit.distance;
            }

            grounded = controller.isGrounded || distanceToGround < 1.5f;
            //Debug.Log("Sliding: " + isSliding);
            //Debug.Log("Grounded: " + grounded);

            //Debug.Log("distanceToGround: " + distanceToGround);

        }
        else {speed = 0;}
        if(Input.GetButton("Continue") && isRiding) {
            transform.parent = null;
            transform.position = rideGetoffPoint.position;
            transform.rotation = rideGetoffPoint.rotation;
            transform.localScale = new Vector3(1, 1, 1);
            hasControl = true;
            isRiding = false;
            rideID = 0;
            rideGetoffPoint = null;

            CameraController cam = GameObject.Find("Main Camera").GetComponent<CameraController>();
            cam.inTelescope = false;
            cam.offset.z = cam.storedOffsetZ;
        }

        if(Input.GetButtonDown("Cancel")) { GameObject.Find("GameManager").GetComponent<GameManager>().HandleSceneReturn(); }
        anim.SetBool("isGrounded", grounded);
        anim.SetBool("isSliding", isSliding);
        anim.SetFloat("Speed", speed);
        anim.SetBool("isRiding", isRiding);
        anim.SetFloat("animID", rideID);

        if(anim2 != null) {
            anim2.SetBool("isGrounded", grounded);
            anim2.SetBool("isSliding", isSliding);
            anim2.SetFloat("Speed", speed);
            anim2.SetBool("isRiding", isRiding);
            anim2.SetFloat("animID", rideID);
        }
    }
}
