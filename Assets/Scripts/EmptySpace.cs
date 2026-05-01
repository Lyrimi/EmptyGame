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
    private Camera cam; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pointAction = InputSystem.actions.FindAction("Point");
        clickAction = InputSystem.actions.FindAction("Click");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = pointAction.ReadValue<Vector2>();
        Vector2 worldSpace = cam.ScreenToWorldPoint(point);
        transform.position = worldSpace;
    }
}
