using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements.Experimental;

public class PlayerControler : MonoBehaviour
{

    [Header("Jump")]
    [SerializeField] private int MaxCoyoteTime;
    [SerializeField] private int JumpAccelTime;
    [SerializeField] private int JumpAccelTimeMin;
    [SerializeField] private float jumpForce;
    int coyoteTime;
    int accelTime;

    [Header("Horzontal Movment")]
    [SerializeField] private float Speed;
    private Rigidbody2D rb;

    InputAction moveAction;


    #region Grounded
    private bool grounded;

     private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.8f)
            {
                grounded = true;

            }
            else if (collision.contacts[0].normal.y < -0.8f)
            {
                grounded = true;
            }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.y > 0.8f)
        {
            grounded = true;

        }
        else if (collision.contacts[0].normal.y < -0.8f)
        {
            grounded = true;
        }

    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
    }

    #endregion
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    #region Jump
    public void onJump()
    {
        if (coyoteTime > 0)
        {
            //sets accel time for the maximum time the force of the jump can be applied
            accelTime = JumpAccelTime;
            coyoteTime = 0;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        //While jump button is pressed and acceltime is still active make the y force equal to the jumpForce
        else if (accelTime > 0 || accelTime > JumpAccelTime - JumpAccelTimeMin)
        {
            rb.AddForce(new Vector2(0, jumpForce - rb.linearVelocity.y), ForceMode2D.Impulse);
            accelTime -= 1;
        }
        //when button is released set accel time to 0
        else
        {
            accelTime = 0;
        }
    }
    #endregion

    void Move()
    {
        Vector2 move = moveAction.ReadValue<Vector2>() * Speed;
        rb.linearVelocityX = move.x;
    }

    void FixedUpdate()
    {
        if (grounded && rb.linearVelocityY <= 0)
        {
            coyoteTime = MaxCoyoteTime;
        }
        Move();
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        moveAction = InputSystem.actions.FindAction("Move");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
