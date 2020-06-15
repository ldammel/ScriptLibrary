using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Jump : MonoBehaviour
{
    public static Jump Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There can only be one Jump in a scene!");
            Application.Quit();
        }
        Instance = this;
        rb = GetComponent<Rigidbody>();
    }
    
    [SerializeField] private float jumpForce = 550f;
    [SerializeField] private int maxJumpAmount = 2;
    [SerializeField] private bool inJump;
    
    private bool readyToJump = true;
    private const float JumpCooldown = 0.25f;
    private Rigidbody rb = null;
    private readonly Vector3 normalVector = Vector3.up;
    
    public int jumpAmount = 0;
    public bool InJump => inJump;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) PlayerJump();
        if (!GroundCheck.Instance.Grounded) return;
        inJump = false;
        jumpAmount = 0;
    }

    private void PlayerJump()
    {
        if (jumpAmount >= maxJumpAmount) inJump = false;
        if (!InJump)
        {
            if (!GroundCheck.Instance.Grounded || !readyToJump || jumpAmount >= maxJumpAmount) return;
        }
        
        readyToJump = false;
        inJump = true;
        jumpAmount++;

        //Add jump forces
        rb.AddForce(1.5f * jumpForce * Vector2.up);
        rb.AddForce(0.5f * jumpForce * normalVector);
        
        //If jumping while falling, reset y velocity.
        Vector3 vel = rb.velocity;
        if (rb.velocity.y < 0.5f)
            rb.velocity = new Vector3(vel.x, 0, vel.z);
        else if (rb.velocity.y > 0) 
            rb.velocity = new Vector3(vel.x, vel.y / 2, vel.z);

        StartCoroutine(ResetJump());
    }

    private IEnumerator ResetJump()
    {
        yield return new WaitForSeconds(JumpCooldown);
        readyToJump = true;
    }

    private void OnCollisionExit(Collision other)
    {
        inJump = true;
    }
}
