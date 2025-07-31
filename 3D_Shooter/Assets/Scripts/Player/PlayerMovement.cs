using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    private InputControls inputControls;
    private Rigidbody rb;

    [Header("Movement Variables")]
    [SerializeField] private int speed;
    [SerializeField] private int jumpForce;
    private Vector2 moveDir;
    private Vector3 currentMoveDir;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Start()
    {
        inputControls = GameManager.instance.inputManager.inputControls;
        
        inputControls.Move.Jump.performed += JumpHandler;
        inputControls.Move.Jump.canceled += JumpHandler;
    }

    void Update()
    {
        WalkHandler();
        RotateHanlder();
    }

    private void RotateHanlder()
    {
        Vector3 positionToLookAt = Camera.main.transform.rotation * Vector3.forward;

        // positionToLookAt.x = currentMovement.x;
        // positionToLookAt.y = 0.0f;
        // positionToLookAt.z = currentMovement.z;

        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(positionToLookAt);
        transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, 10 * Time.deltaTime);
    }


    private void WalkHandler()
    {
        moveDir = GameManager.instance.inputManager.MoveDir;

        currentMoveDir.x = moveDir.x;
        currentMoveDir.y = rb.linearVelocity.y;
        currentMoveDir.z = moveDir.y;
        
        Vector3 cameraRelativeDir = ConvertMoveDirectionToCameraSpace(moveDir);

        rb.linearVelocity = new Vector3(cameraRelativeDir.x * speed, currentMoveDir.y, cameraRelativeDir.z * speed);
    }
    
    private Vector3 ConvertMoveDirectionToCameraSpace(Vector3 directionMove)
    {
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        // Vector3 cameraForwardZ = cameraForward * directionMove.z;
        // Vector3 cameraRightX = cameraRight * directionMove.x;
        //
        // Vector3 directionToMovePlayer = cameraForwardZ + cameraRightX;
        
        Vector3 cameraMove = (cameraForward * directionMove.y + cameraRight * directionMove.x).normalized;

        return cameraMove;
    }
    
    private void JumpHandler(InputAction.CallbackContext obj)
    {
        if (obj.performed)
        {
            rb.linearVelocity = new Vector3(0, jumpForce, 0);

        }
        else if (obj.canceled && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }

}
