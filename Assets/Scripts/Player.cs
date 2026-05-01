using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public bool dash = false;
    public bool moving = false;

    
    public float speed = 5f;
    public float jumpforce = 3f;
    public float jumpforcecon = 3f;
    public float maxspeed = 10f;

    public InputSystem_Actions playercontrols;
    public Rigidbody2D rb;

    public bool canjump = false;
    public bool doublejump = false;
    
    public bool smack = false;

    Vector2 move;
    private InputAction Moving;
    private InputAction jump;

    public float doublejumpcooldowntimer = 2;
    public float smackcooldowntimer = 2;
    private bool cooldowntimeron = false;

    public Vector2 velocity;
    public float acceleration = 10f;

    

    public float movementX;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colided2D");


        if (collision.gameObject.CompareTag("consumable")) 
        { 
            Destroy(collision.gameObject);
            dash = true;

        }

        if (collision.gameObject.CompareTag("Jumpable"))
        {
            canjump = true;

            smack = false;
            smackcooldowntimer = 2;
            if (doublejump == false)
            {
                
                cooldowntimeron=true;
                
            }
        }
        if (collision.gameObject.CompareTag("wall"))
        {
            smack = true;
            if (smack == true) 
            {
                Debug.Log("ran into wall");
                Moving.Disable();
                jump.Disable();
                
                
            }
        }


    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Jumpable")) 
        {
            doublejumpcooldowntimer = 2;
            cooldowntimeron = false;
        }
        
    }
    

    private void Awake()
    {
        playercontrols = new InputSystem_Actions();
        
    }
    private void Start()
    {
        velocity = rb.linearVelocity;
        jumpforce = 3;
    }

   
    //Update is called once per frame
    void Update()
    {
       
       move = Moving.ReadValue<Vector2>();
        
       
        
    }
    private void FixedUpdate()
    {
        
        if (canjump == false && doublejump == true)
        {
            canjump = true;
            doublejump = false;
        }
        rb.linearVelocity = new Vector2(move.x*speed, move.y=rb.linearVelocityY); 
        if (smack == false) 
        {
            Moving.Enable();
            jump.Enable();
        }
        movementX = rb.linearVelocityX;
       if (movementX >=1 && movementX !<=10 || movementX <= -1 && movementX !>= -10)
        {
            speed = speed +Time.fixedDeltaTime;
        }

       if(movementX != 0) 
        {
            moving = true;
        }
       if(movementX == 0)
            { moving = false; }

        if (moving == true) 
        {
            jumpforce =jumpforcecon+speed/3;
        }
        if (moving != true) 
        {
        jumpforce = jumpforcecon;
        }

        if (smack == true)
        {
            

            smackcooldowntimer -= Time.fixedDeltaTime;
            if (smackcooldowntimer < 0.0f)
            {
                TimerEnded();
            }   


        }
        if (cooldowntimeron == true)
        {
            doublejumpcooldowntimer -= Time.fixedDeltaTime;
            if(doublejumpcooldowntimer < 0.0f)
            {
                cooldowntimeron = false;
                doublejump = true;
            }
        }
    }
    void TimerEnded() 
    {
    smack = false;
    smackcooldowntimer = 2;
    }
    
    private void Jump(InputAction.CallbackContext context)
    {
        if (canjump == true) 
        {
            Debug.Log("Jumped");

            move.y = jumpforce;
            rb.linearVelocityY = move.y;
            canjump = false;
        }
        

       
    }
   
    void Test(InputAction.CallbackContext context) 
    {
    if (context.performed&& smack == false)
        {
            speed = 5;
        }
    }
    void Dash(InputAction.CallbackContext context) 
    {
        if(context.performed && dash == true) 
        {
            if(movementX <= 0) 
            {
                transform.position = transform.position + new Vector3(-speed, 0, 0);
            }
            if (movementX >= 0)
            {
                transform.position = transform.position + new Vector3(speed, 0, 0);
            }

        }
    }


    
}
