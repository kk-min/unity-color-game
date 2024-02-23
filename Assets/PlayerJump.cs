using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [SerializeField] private InputActionProperty jumpButton;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private CharacterController cc;

    private float gravity = Physics.gravity.y;
    private Vector3 movement;
    public float distToGround = 0.000001f;

    private void Update()
    {
        bool _isGrounded = IsGrounded();
        Debug.Log(_isGrounded);
        if(jumpButton.action.WasPressedThisFrame()&&_isGrounded)
        {
            //Debug.Log("jumping");
            Jump();
        }
        movement.y += gravity * Time.deltaTime;

        cc.Move(movement*Time.deltaTime);
    }
    private void Jump()
    {
        movement.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
    }
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround); 
    }
}
