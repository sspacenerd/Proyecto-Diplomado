using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PlayerController.cs
// Usando referencias de:
//youtube.com/watch?v=PmIPqGqp8UY

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Transform myCam = null;
    public static float mouseSensitivity;
    public float walkSpeed;
    public AudioSource footSteps;
    [SerializeField] private float gravity = -13f;
    [SerializeField][Range(0.0f, 0.5f)] private float moveSmoothTime = 0.3f; 
    [SerializeField][Range(0.0f, 0.5f)] private float mouseSmoothTime = 0.3f;

    [SerializeField] private bool lockCursor = true;

    private float cameraPitch = 0;
    float velocityY = 0;

    Vector2 currentDir = Vector2.zero;
    Vector2 currentDirVelocity = Vector2.zero;

    Vector2 currentMouseDelta = Vector2.zero;
    Vector2 currentMouseDeltaVelocity = Vector2.zero;

    CharacterController controller;


    private void Awake()
    {
        if (ES3.KeyExists("Sensibility"))
        {
            mouseSensitivity = (float)ES3.Load("Sensibility");
        }
    }
    
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    
    private void Update()
    {
        PlayerMovement();
        MouseLook();
       // Debug.Log(ES3.Load("Sensibility"));

    }
    void PlayerMovement()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.D))
        {
            footSteps.enabled = true;
        }
        else
        {
            footSteps.enabled = false;
        }
        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();
        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);
        if (controller.isGrounded)
        {
            velocityY = 0;
            velocityY += gravity * Time.deltaTime;
        }
        Vector3 velocity = (transform.forward * targetDir.y + transform.right * targetDir.x) * walkSpeed + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);
    }
    public void MouseLook()
    {
        if(GameManager.gameIsPaused == false)
        {
            Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);
            cameraPitch -= currentMouseDelta.y * mouseSensitivity;
            cameraPitch = Mathf.Clamp(cameraPitch, -90, 90f);
            myCam.localEulerAngles = Vector3.right * cameraPitch;
            transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Enemy")
        {
            StartCoroutine(GameManager.gameManagerInstance.GameOver());
        }
    }
}
