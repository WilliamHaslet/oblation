using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public static PlayerController sharedPlayerControllerInstance;
    public CharacterController characterController;
    public float moveSpeed;
    private float moveSpeedStore;
    public float sprintSpeed;
    public float sneakSpeed;
    public float jumpHeight;
    public GameObject cameraHolder;
    public GameObject playerMesh;
    public float cameraTurnSpeed;
    private bool isJumping;
    private bool isFloating;
    public float jumpTimer;
    public float gravity;
    private Vector3 motion;
    public bool isOnLadder;
    public float ladderClimbSpeed;
    public Animator playerAnimator;
    public LayerMask groundTerrain;
    public Vector3 groundCheckPos;
    public bool canJump;
    public bool canMove;
    
    private void Start()
    {

        sharedPlayerControllerInstance = this;
        
        moveSpeedStore = moveSpeed;

    }

    private void Update()
    {

        if (canMove)
        {

            CheckForGround();
            
            if (Input.GetKey(KeyCode.LeftShift))
            {

                playerAnimator.SetBool("running", true);
                playerAnimator.SetBool("sneaking", false);
                moveSpeed = sprintSpeed;

            }
            else if (Input.GetKey(KeyCode.LeftAlt))
            {

                playerAnimator.SetBool("sneaking", true);
                playerAnimator.SetBool("running", false);
                moveSpeed = sneakSpeed;

            }
            else
            {

                playerAnimator.SetBool("sneaking", false);
                playerAnimator.SetBool("running", false);

                moveSpeed = moveSpeedStore;

            }

            cameraHolder.transform.Rotate(0, Input.GetAxis("Mouse X") * cameraTurnSpeed, 0);

            Camera.main.transform.Rotate(Input.GetAxis("Mouse Y") * -cameraTurnSpeed / 2, 0, 0);

            motion = Vector3.zero;

            if (Input.GetAxis("Vertical") != 0)
            {

                motion += cameraHolder.transform.forward * Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

                if (isOnLadder)
                {

                    motion.y += ladderClimbSpeed * Time.deltaTime;

                }

            }

            if (Input.GetAxis("Horizontal") != 0)
            {

                motion += cameraHolder.transform.right * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

            }

            if (motion != Vector3.zero)
            {

                playerAnimator.SetBool("moving", true);

                playerMesh.transform.forward += motion;

            }
            else
            {

                playerAnimator.SetBool("moving", false);

            }

            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {

                StartCoroutine(Jump());

            }

            if (isJumping && !isFloating)
            {

                motion.y += jumpHeight * Time.deltaTime;

            }
            else if (isFloating)
            {

                motion.y += 0;

            }
            else
            {

                motion.y -= gravity * Time.deltaTime;

            }

            characterController.Move(motion);

        }

    }

    private IEnumerator Jump()
    {

        isJumping = true;
        
        yield return new WaitForSeconds(jumpTimer);

        isFloating = true;

        yield return new WaitForSeconds(jumpTimer / 7);

        isJumping = false;

        isFloating = false;

    }

    private void CheckForGround()
    {
        if (Physics.CheckSphere(transform.position + groundCheckPos, 0.05f, groundTerrain))
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
    }
    
}
