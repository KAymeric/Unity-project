using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

[RequireComponent(typeof(CharacterController))]
public class InputSystemFirstPersonCharacter : MonoBehaviour
{
    private Custominputs inputActions;

    private CharacterController controller;

    [SerializeField] private Camera cam;
    [SerializeField] private float movementSpeed = 2.0f;
    [SerializeField] public float lookSensitivity = 1.0f;
    [SerializeField] public float jumpPower = 8.0f;
    [SerializeField] public float cooldownBaseValue = 1.0f;
    [SerializeField] public GameObject munition;
    [SerializeField] public float throwForce = 40f;
    [SerializeField] public int maxballs = 10;

    private float xRotation = 0f;

  
    private Vector3 velocity;
    public float gravity = -9.81f;
    private bool grounded;
    private float cooldown;
    private List<GameObject> balls ;

    private void Awake()
    {
        inputActions = new Custominputs();
        balls = new List<GameObject>();
    }
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void Update()
    {
        DoMovement();
        DoLooking();
        DoJump();
        DoSprint();
        DoFire();
    }

    private void DoLooking()
    {
        Vector2 looking = GetPlayerLook();
        float lookX = looking.x * lookSensitivity * Time.deltaTime;
        float lookY = looking.y * lookSensitivity * Time.deltaTime;
        
        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        
        transform.Rotate(Vector3.up * lookX);
    }

    private void DoMovement()
    {
        grounded = controller.isGrounded;
        if (grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector2 movement = GetPlayerMovement();
        Vector3 move = transform.right * movement.x + transform.forward * movement.y;
        controller.Move(move * movementSpeed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void DoJump()
    {
        if (inputActions.FPSController.jump.ReadValue<float>() > 0)
        {
            if (controller.isGrounded)
            {
                velocity.y = jumpPower;
            }
        }
    }

    private void DoSprint()
    {
        if (inputActions.FPSController.sprint.ReadValue<float>() > 0)
        {
            movementSpeed = 8f;
        }
        else
        {
            movementSpeed = 5f;
        }
    }

    private void DoFire()
    {
        if (inputActions.FPSController.fire.ReadValue<float>() > 0 && cooldown <= 0)
        {
            GameObject ball = Instantiate(munition,cam.transform.position, cam.transform.rotation);
            ball.GetComponent<Rigidbody>().velocity  = cam.transform.forward * throwForce;
            cooldown = cooldownBaseValue;
            balls.Add(ball);
        }
        if (balls.Count > maxballs)
        {
            Destroy(balls[0]);
            balls.RemoveAt(0);
        }
        cooldown -= Time.deltaTime;
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return inputActions.FPSController.Move.ReadValue<Vector2>();
    }

    public Vector2 GetPlayerLook()
    {
        return inputActions.FPSController.Look.ReadValue<Vector2>();
    }
}