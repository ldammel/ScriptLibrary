using System;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public static GroundCheck Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There can only be one GroundCheck in a scene!");
            Application.Quit();
        }
        Instance = this;
    }
    
    [SerializeField] private bool grounded = false;
    [SerializeField] private LayerMask whatIsGround = new LayerMask();

    public bool Grounded => grounded;

    private void OnCollisionStay(Collision other)
    {
        int layer = other.gameObject.layer;
        if (whatIsGround != (whatIsGround | (1 << layer))) return;
        grounded = true;
    }

    private void OnCollisionExit(Collision other)
    {
        grounded = false;
    }
}
