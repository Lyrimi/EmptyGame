using System.Diagnostics;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D.IK;

public class EmptySpace : MonoBehaviour
{   
    InputAction pointAction;
    InputAction clickAction;

[SerializeField]
    colisonFuckery colisonFuckery;

    [SerializeField]
    private Camera cam;
    public bool Enabled;

    Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pointAction = InputSystem.actions.FindAction("Point");
        clickAction = InputSystem.actions.FindAction("Click");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Enabled)
        {
            return;
        }
        Vector2 point = pointAction.ReadValue<Vector2>();
        Vector2 worldSpace = cam.ScreenToWorldPoint(point);

        rb.linearVelocity = worldSpace - new Vector2(transform.position.x,transform.position.y);
    }

}
