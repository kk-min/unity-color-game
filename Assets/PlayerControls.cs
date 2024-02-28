using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerControls : MonoBehaviour
{
    [SerializeField] private CharacterController cc;
    [SerializeField] private InputActionProperty XButton;
    [SerializeField] private InputActionProperty AButton;
    [SerializeField] private float jumpHeight = 3f;

    private CustomInput input = null;

    private Vector3 movement;
    private Vector3 pushDir;
    private Vector3 moveDir;

    public Vector3 checkPoint;

    public Rigidbody rb;

    private float gravity = Physics.gravity.y;
    private float pushForce;

    public float distToGround = 0.4f;
    public float speed = 10.0f;
    public float airVelocity = 8f;
    public float maxVelocityChange = 10.0f;
    public float maxFallSpeed = 20.0f;
    public float rotateSpeed = 25f; //Speed the player rotate
    
    private bool isStuned = false;
    private bool wasStuned = false; //If player was stunned before get stunned another time
    private bool slide = false;
    private bool canMove = true; //If player is not hitted

    public bool loadCheckPoint = false;



    void Start()
    {
        distToGround = GetComponent<Collider>().bounds.extents.y;
    }
    private void Update()
    {
        bool _isGrounded = IsGrounded();
        //Debug.Log(_isGrounded);
        if(loadCheckPoint)
        {
            Debug.Log("loading checkpt");
            transform.position = checkPoint;
            Debug.Log("loaded"+transform.position);
            loadCheckPoint = false;
        }
        if (AButton.action.WasPressedThisFrame() && _isGrounded)
        {
            //Debug.Log("jumping");
            Jump();
        }
        //Debug.Log(_isGrounded);
        if (XButton.action.WasPressedThisFrame())
        {
            Debug.Log("swapping");
            Swap();
        }
        movement.y += gravity * Time.deltaTime;

        cc.Move(movement * Time.deltaTime);

        
    }

    // Jumping Code
    private void Jump()
    {
        movement.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
    }
    //Make sure player is on the ground when jumping to prevent buggy jump
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f);
    }
    //Swap the player body color via changing layer mask and color will change due to color change script
    private void Swap()
    {
        int newLayer;
        //Debug.Log("inside swap");
        if (gameObject.layer == LayerMask.NameToLayer("Blue"))
        {
            //Debug.Log("inside swap red");
            newLayer = LayerMask.NameToLayer("Red");
        }
        else
        {
            //Debug.Log("inside swap blue");
            newLayer = LayerMask.NameToLayer("Blue");
        }

        gameObject.layer = newLayer;
        SetColor(transform, newLayer);
        //Debug.Log("Current layer: " + gameObject.layer);
    }
    private void SetColor(Transform node, int newLayer)
    {
        foreach (Transform child in node)
        {
            child.gameObject.layer = newLayer;
            SetColor(child, newLayer);
        }
    }

    // Load checkpoint code
    public void LoadCheckPoint()
    {
        //Debug.Log("inside playercontrol");
        //Debug.Log(transform.position);
        //Debug.Log(checkPoint);
        //transform.position = checkPoint;
        loadCheckPoint = true;
        Debug.Log(transform.position);
    }
    void Awake()
    {
        input = new CustomInput();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = false;

        checkPoint = transform.position;
        Cursor.visible = false;
    }

    // Physics when pendulum hit the player
    public void HitPlayer(Vector3 velocityF, float time)
    {
        rb.velocity = velocityF;

        pushForce = velocityF.magnitude;
        pushDir = Vector3.Normalize(velocityF);
        StartCoroutine(Decrease(velocityF.magnitude, time));
    }

    private IEnumerator Decrease(float value, float duration)
    {
        if (isStuned)
            wasStuned = true;
        isStuned = true;
        canMove = false;

        float delta = 0;
        delta = value / duration;

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            yield return null;
            if (!slide) //Reduce the force if the ground isnt slide
            {
                pushForce = pushForce - Time.deltaTime * delta;
                pushForce = pushForce < 0 ? 0 : pushForce;
                //Debug.Log(pushForce);
            }
            rb.AddForce(new Vector3(0, -gravity * GetComponent<Rigidbody>().mass, 0)); //Add gravity
        }

        if (wasStuned)
        {
            wasStuned = false;
        }
        else
        {
            isStuned = false;
            canMove = true;
        }
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            if (moveDir.x != 0 || moveDir.z != 0)
            {
                Vector3 targetDir = moveDir; //Direction of the character

                targetDir.y = 0;
                if (targetDir == Vector3.zero)
                    targetDir = transform.forward;
                Quaternion tr = Quaternion.LookRotation(targetDir); //Rotation of the character to where it moves
                Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, Time.deltaTime * rotateSpeed); //Rotate the character little by little
                transform.rotation = targetRotation;
            }

            if (IsGrounded())
            {
                // Calculate how fast we should be moving
                Vector3 targetVelocity = moveDir;
                targetVelocity *= speed;

                // Apply a force that attempts to reach our target velocity
                Vector3 velocity = rb.velocity;
                if (targetVelocity.magnitude < velocity.magnitude) //If I'm slowing down the character
                {
                    targetVelocity = velocity;
                    rb.velocity /= 1.1f;
                }
                Vector3 velocityChange = (targetVelocity - velocity);
                velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
                velocityChange.y = 0;
                if (!slide)
                {
                    if (Mathf.Abs(rb.velocity.magnitude) < speed * 1.0f)
                        rb.AddForce(velocityChange, ForceMode.VelocityChange);
                }
                else if (Mathf.Abs(rb.velocity.magnitude) < speed * 1.0f)
                {
                    rb.AddForce(moveDir * 0.15f, ForceMode.VelocityChange);
                    //Debug.Log(rb.velocity.magnitude);
                }

                
            }
            else
            {
                if (!slide)
                {
                    Vector3 targetVelocity = new Vector3(moveDir.x * airVelocity, rb.velocity.y, moveDir.z * airVelocity);
                    Vector3 velocity = rb.velocity;
                    Vector3 velocityChange = (targetVelocity - velocity);
                    velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
                    velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
                    rb.AddForce(velocityChange, ForceMode.VelocityChange);
                    if (velocity.y < -maxFallSpeed)
                        rb.velocity = new Vector3(velocity.x, -maxFallSpeed, velocity.z);
                }
                else if (Mathf.Abs(rb.velocity.magnitude) < speed * 1.0f)
                {
                    rb.AddForce(moveDir * 0.15f, ForceMode.VelocityChange);
                }
            }
        }
        else
        {
            rb.velocity = pushDir * pushForce;
        }
        // We apply gravity manually for more tuning control
        rb.AddForce(new Vector3(0, -gravity * GetComponent<Rigidbody>().mass, 0));
    }
}
