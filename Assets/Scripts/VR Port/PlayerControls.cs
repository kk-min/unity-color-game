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

    private AudioSource jumpSound;

    private Vector3 movement;
    private Vector3 pushDir;
    private Vector3 moveDir;

    public Vector3 checkPoint;

    public Rigidbody rb;

    private float gravity = Physics.gravity.y;
    private float pushForce;

    public float distToGround = 1f;
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
        jumpSound = GetComponent<AudioSource>();
    }
    private void Update()
    {
        bool _isGrounded = IsGrounded();
        //Debug.Log(_isGrounded);
        if(loadCheckPoint)
        {
            //Debug.Log("loading checkpt");
            transform.position = checkPoint;
            //Debug.Log("loaded"+transform.position);
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
            //Debug.Log("swapping");
            Swap();
        }
        movement.y += gravity * Time.deltaTime;

        cc.Move(movement * Time.deltaTime);

        
    }

    // Jumping Code
    private void Jump()
    {
        jumpSound.Play();
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
        //Debug.Log(transform.position);
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

}
