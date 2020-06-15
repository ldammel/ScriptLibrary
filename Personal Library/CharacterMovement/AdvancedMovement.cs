using UnityEngine;

public class AdvancedMovement : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    
    private Rigidbody rb = null;
    private float xRotation = 0;
    private float sensitivity = 50f;
    private float sensMultiplier = 1f;
    
    [SerializeField] private float moveForce = 4500;
    [SerializeField] private float maxSpeed = 20;
    [SerializeField] private float sprintSpeed = 30;
    private float curSpeed = 0;

    [SerializeField] private float counterMovement = 0.175f;
    [SerializeField] private float slideForce = 400;
    [SerializeField] private float slideCounterMovement = 0.2f;
    private float threshold = 0.01f;
    private Vector3 crouchScale = new Vector3(1, 0.5f, 1);
    private Vector3 playerScale;
    private float x, y;
    private bool sprinting, crouching;
    private Vector3 wallNormalVector;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start() 
    {
        playerScale =  transform.localScale;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    private void FixedUpdate() 
    {
        Movement();
    }

    private void Update() 
    {
        GetInput();
        Look();
    }

    private void GetInput() 
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        crouching = Input.GetKey(KeyCode.LeftControl);
        sprinting = Input.GetKey(KeyCode.LeftShift);
  
        //Crouching
        if (Input.GetKeyDown(KeyCode.LeftControl))
            StartCrouch();
        if (Input.GetKeyUp(KeyCode.LeftControl))
            StopCrouch();
    }

    private void StartCrouch() 
    {
        transform.localScale = crouchScale;
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        if (!(rb.velocity.magnitude > 0.5f)) return;
        if (GroundCheck.Instance.Grounded) 
        {
            rb.AddForce(gameObject.transform.forward * slideForce);
        }
    }

    private void StopCrouch() 
    {
        transform.localScale = playerScale;
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
    }

    private void Movement() 
    {
        Vector2 mag = FindVelRelativeToLook();
        float xMag = mag.x, yMag = mag.y;
        if(GroundCheck.Instance.Grounded)CounterMovement(x, y, mag);
        curSpeed = sprinting ? sprintSpeed : this.maxSpeed;
        
        if (crouching && GroundCheck.Instance.Grounded) 
        {
            rb.AddForce(Vector3.down * Time.deltaTime * 3000);
            return;
        }
        
        if (x > 0 && xMag > curSpeed) x = 0;
        if (x < 0 && xMag < -curSpeed) x = 0;
        if (y > 0 && yMag > curSpeed) y = 0;
        if (y < 0 && yMag < -curSpeed) y = 0;
        
        float multiplier = 1f, multiplierV = 1f;
        
        if (!GroundCheck.Instance.Grounded) {
            multiplier = 0.5f;
            multiplierV = 0.5f;
        }
        
        if (GroundCheck.Instance.Grounded && crouching) multiplierV = 0f;
        rb.AddForce(gameObject.transform.forward * y * moveForce * Time.deltaTime * multiplier * multiplierV);
        rb.AddForce(gameObject.transform.right * x * moveForce * Time.deltaTime * multiplier);
    }

    private float desiredX;
    private void Look() 
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime * sensMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime * sensMultiplier;
        desiredX = transform.localRotation.eulerAngles.y + mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        gameObject.transform.localRotation = Quaternion.Euler(0, desiredX, 0);
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

    private void CounterMovement(float x, float y, Vector2 mag) 
    {
        if (!GroundCheck.Instance.Grounded || Jump.Instance.InJump) return;
        
        if (crouching) 
        {
            rb.AddForce(moveForce * Time.deltaTime * -rb.velocity.normalized * slideCounterMovement);
            return;
        }
        
        if (Mathf.Abs(mag.x) > threshold && Mathf.Abs(x) < 0.05f || (mag.x < -threshold && x > 0) || (mag.x > threshold && x < 0)) 
        {
            rb.AddForce(moveForce * gameObject.transform.right * Time.deltaTime * -mag.x * counterMovement);
        }
        if (Mathf.Abs(mag.y) > threshold && Mathf.Abs(y) < 0.05f || (mag.y < -threshold && y > 0) || (mag.y > threshold && y < 0)) 
        {
            rb.AddForce(moveForce * gameObject.transform.forward * Time.deltaTime * -mag.y * counterMovement);
        }
        
        if (!(Mathf.Sqrt((Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2))) > curSpeed)) return;
        float fallspeed = rb.velocity.y;
        Vector3 n = rb.velocity.normalized * curSpeed;
        rb.velocity = new Vector3(n.x, fallspeed, n.z);
    }

    public Vector2 FindVelRelativeToLook() 
    {
        float lookAngle = gameObject.transform.eulerAngles.y;
        float moveAngle = Mathf.Atan2(rb.velocity.x, rb.velocity.z) * Mathf.Rad2Deg;

        float u = Mathf.DeltaAngle(lookAngle, moveAngle);
        float v = 90 - u;

        float magnitue = rb.velocity.magnitude;
        float yMag = magnitue * Mathf.Cos(u * Mathf.Deg2Rad);
        float xMag = magnitue * Mathf.Cos(v * Mathf.Deg2Rad);
    
        return new Vector2(xMag, yMag);
    }
}
