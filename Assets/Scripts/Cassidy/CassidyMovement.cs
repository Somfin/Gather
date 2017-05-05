using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CassidyMovement : MonoBehaviour {
    public float horizSpeed;
    public float vertSpeed;
    public float firstJumpTime;
    public float secondJumpTime;
    public bool secondJumpAvailable;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask isGround;

    public float isFallingSpeed;

    private bool left;
    private bool right;
    private bool up;
    private bool down;
    private bool hover;

    private bool isJumping;
    private bool secondJumpReady;
    private float jumpTimeUsed;
    private State state;

    private enum State
    {
        GROUNDED,
        RISING,
        FALLING
    }

    // Use this for initialization
    void Start () {
        secondJumpReady = true;
	}
	
	// Update is called once per frame
	void Update () {
        left = Input.GetKey(KeyCode.A);
        right = Input.GetKey(KeyCode.D);
        up = Input.GetKey(KeyCode.W);
        down = Input.GetKey(KeyCode.S);
        hover = Input.GetKey(KeyCode.Space);
	}

    private void FixedUpdate()
    {
        var body = gameObject.GetComponent<Rigidbody2D>();
        var moveDir = (left ? -1 : 0) +  (right ? 1 : 0);
        body.velocity = new Vector2(moveDir * horizSpeed, body.velocity.y);

        GroundingCheck();
        
        if (up)
        {
            switch (state)
            {
                case State.GROUNDED:
                    if (!isJumping)
                    {
                        state = State.RISING;
                        body.velocity = new Vector2(body.velocity.x, vertSpeed);
                        jumpTimeUsed = firstJumpTime;
                    }
                    break;
                case State.RISING:
                    jumpTimeUsed -= Time.deltaTime;
                    if (jumpTimeUsed < 0)
                    {
                        state = State.FALLING;
                    } else
                    {
                        body.velocity = new Vector2(body.velocity.x, vertSpeed);
                    }
                    break;
                case State.FALLING:
                    if (secondJumpReady && !isJumping)
                    {
                        state = State.RISING;
                        body.velocity = new Vector2(body.velocity.x, vertSpeed);
                        jumpTimeUsed = secondJumpTime;
                        secondJumpReady = false;
                    }
                    break;

            }
            isJumping = true;
        } else
        {
            switch (state)
            {
                case State.RISING:
                    state = State.FALLING;
                    break;
            }
            isJumping = false;
        }
    }

    private void GroundingCheck()
    {
        var body = gameObject.GetComponent<Rigidbody2D>();
        switch (state)
        {
            case State.GROUNDED:
                if (!IsGrounded())
                {
                    if (body.velocity.y < -isFallingSpeed)
                    {
                        state = State.FALLING;
                    }
                }
                break;
            case State.FALLING:
                if (IsGrounded())
                {
                    state = State.GROUNDED;
                    secondJumpReady = secondJumpAvailable;
                }
                break;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGround);
    }
}
