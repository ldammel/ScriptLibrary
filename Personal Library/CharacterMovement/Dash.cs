using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Dash : MonoBehaviour
{
    private Rigidbody rb = null;
    [SerializeField] private float dashForce = 550f;
    [SerializeField] private float dashCooldown = 2f;
    [SerializeField] private float dashTime = 0.2f;
    private bool inDash;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.V) && !inDash) StartCoroutine(PlayerDash());
    }

    private IEnumerator PlayerDash()
    {
        inDash = true;
        var vel = rb.velocity;
        rb.AddForce(Camera.main.transform.forward * dashForce, ForceMode.VelocityChange);
        yield return new WaitForSeconds(dashTime);
        rb.velocity = vel ;
        yield return new WaitForSeconds(dashCooldown);
        inDash = false;
    }
}
