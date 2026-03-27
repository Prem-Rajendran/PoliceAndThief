using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private GameObject thiefHolder;
    [SerializeField] private GameObject cameraHolder;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float deceleration = 12f;
    [SerializeField] private float bounceForce = 10f;

    private Vector3 currentVelocity = Vector3.zero;
    private float stunTimer = 0f;

    private Vector3 cameraInitalRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (cameraHolder == null)
        {
            Debug.LogError("Camera Holder is not assigned in the inspector.");
        }

        if (thiefHolder == null)
        {
            Debug.LogError("Thief Holder is not assigned in the inspector.");
        }

        cameraInitalRotation = cameraHolder.transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {

        if (stunTimer > 0f)
        {
            stunTimer -= Time.deltaTime;
            currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero, deceleration * Time.deltaTime);
            transform.Translate(currentVelocity * Time.deltaTime, Space.World);
            return; // Skip movement while stunned
        }

        Vector3 inputDirection = Vector3.zero;
        var keyboard = UnityEngine.InputSystem.Keyboard.current;

        // 1. Capture Input Direction
        if (keyboard.upArrowKey.isPressed)    inputDirection = Vector3.left;
        else if (keyboard.downArrowKey.isPressed)  inputDirection = Vector3.right;
        else if (keyboard.leftArrowKey.isPressed)  inputDirection = Vector3.back;
        else if (keyboard.rightArrowKey.isPressed) inputDirection = Vector3.forward;

        // 2. Define Target Velocity
        Vector3 targetVelocity = inputDirection * movementSpeed;

        // 3. Smoothly Interpolate Velocity
        // We use a higher value for deceleration to make stopping feel "snappier" than starting
        float lerpFactor = (inputDirection != Vector3.zero) ? acceleration : deceleration;
        
        currentVelocity = Vector3.MoveTowards(
            currentVelocity, 
            targetVelocity, 
            lerpFactor * Time.deltaTime
        );

        // 5. Final Movement
        transform.Translate(currentVelocity * Time.deltaTime, Space.World);

        // if Q key is pressed rotate the player to the left
        if (UnityEngine.InputSystem.Keyboard.current.qKey.isPressed)
        {
            Debug.Log("Q Key Pressed");
            cameraHolder.transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        // if E key is pressed rotate the player to the right
        else if (UnityEngine.InputSystem.Keyboard.current.eKey.isPressed)
        {
            Debug.Log("E Key Pressed");
            cameraHolder.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
        else if (UnityEngine.InputSystem.Keyboard.current.rKey.isPressed)
        {
            Debug.Log("R Key Pressed - Resetting Camera Position");
            cameraHolder.transform.rotation = Quaternion.Euler(cameraInitalRotation);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Player collided with {collision.gameObject.tag}");

        // Apply a bounce force when colliding with an object
        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector3 bounceDirection = collision.contacts[0].normal;
            currentVelocity = bounceDirection * bounceForce;
            currentVelocity.y = 0f; // Keep the bounce horizontal
            stunTimer = 0.1f;
            Debug.Log($"Collided with {collision.gameObject.name}, applying currentVelocity and stun.");
        }
    }
}
