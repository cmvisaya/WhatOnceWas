using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float dashSpeed;
    public float dashTime = 0.25f;
    public float dashCooldown = 1f;
    private bool canDash = true;
    private bool isDashing = false;
    public float slopeSpeed;
    public float jumpForce;
    public float gravityScale;

    public CharacterController controller;
    private Vector3 moveDirection;

    public Transform pivot;
    public float rotateSpeed;

    public GameObject playerModel;

    private bool grounded = false;

    private bool willSlideOnSlopes = true;
    private Vector3 hitPointNormal;
    /*private bool isSliding //Refactor this code to a wallrun
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
    }*/

    public bool hasControl = true;

    public bool canFloat = false;
    public bool floating = false;
    public bool canAirJump = false;
    public bool airDashRefreshInAir = false;

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
            moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
            if (Input.GetButtonDown("Sprint") && canDash)
            {
                if(!airDashRefreshInAir) {
                    canDash = false;
                }
                StartCoroutine(Dash());
            }

            if (isDashing)
            {
                moveDirection = moveDirection.normalized * dashSpeed;
            }
            else
            {
                moveDirection = moveDirection.normalized * moveSpeed;
            }
            moveDirection.y = yStore;

            if (Input.GetButtonDown("Fire1") && canFloat) {
                floating = !floating;
                if(floating) {
                    Debug.Log("START FLOAT: " + transform.position.x + " | " + (transform.position.y - 1) + " | " + transform.position.z);
                }
            }

            /*if (Input.GetButtonDown("Fire2")) {
                canAirJump = !canAirJump;
            }*/

            if (controller.isGrounded)
            {
                canDash = true;
                moveDirection.y = -1f;
                if (Input.GetButton("Jump"))
                {
                    moveDirection.y = jumpForce;
                    Debug.Log(transform.position.x + " | " + transform.position.y + " | " + transform.position.z);
                }
            }
            /*else if (grounded && Input.GetButtonDown("Jump")) //Account for "sliding" jump
            {
                moveDirection.y = jumpForce;
            }*/

            if (floating) {
                moveDirection.y = 0;
            }

            if (Input.GetButtonDown("Jump") && canAirJump)
            {
                moveDirection.y = jumpForce;
                Debug.Log("JUMP: " + transform.position.x + " | " + (transform.position.y - 1) + " | " + transform.position.z);
            }

            moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);

            //More sliding stuff?
            /*
            if (willSlideOnSlopes && isSliding)
            {
                moveDirection += new Vector3(hitPointNormal.x, -hitPointNormal.y, hitPointNormal.z) * slopeSpeed;
            }*/

            controller.Move(moveDirection * Time.deltaTime);

            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
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

            if(transform.position.y < 0f) {
                transform.position = new Vector3(-25, 10, 1);
            }

            if(Input.GetButtonDown("Cancel")) {
                //GameObject.Find("GameManager").GetComponent<GameManager>().HandleSceneReturn();
            }
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
    }
}
