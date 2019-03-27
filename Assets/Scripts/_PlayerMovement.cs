using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _PlayerMovement : MonoBehaviour
{
    private Rigidbody2D Rb2D;
    public float speed = 10f;
    private float Move;

    //For Jump
    public float GroundRadius;
    public float JumpForce;
    private bool IsGround;
    public Transform GroundCheck;
    public LayerMask Grounded;

    private bool CanDoubleJump;
    private bool StoppedJump;

    public float Jumptime;
    private float JumptimeCounter;

    //fallingspeed
    private float landingSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        IsGround = Physics2D.OverlapCircle(GroundCheck.transform.position, GroundRadius, Grounded);
        Walk();
        Jump();
        FallSpeed();
    }

    private void Walk()
    {
        Move = Input.GetAxis("Horizontal");
        if (Move > 0 || Move < 0)
        {
            Rb2D.velocity = new Vector2(Move * speed, Rb2D.velocity.y);
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {

            if (IsGround)
            {
                Rb2D.velocity = new Vector2(Rb2D.velocity.x, JumpForce);
                StoppedJump = false;
            }

            if (!IsGround && CanDoubleJump)
            {
                Rb2D.velocity = new Vector2(Rb2D.velocity.x, JumpForce);
                JumptimeCounter = Jumptime;
                StoppedJump = false;
                CanDoubleJump = false;
            }

        }

    }

    private void FallSpeed()
    {
        if (Input.GetButtonDown("Vertical"))
        {
            JumpForce = landingSpeed;
            Rb2D.velocity = new Vector3(0, -10, 0);
            JumpForce = 5f;
        }
        
    }
}

