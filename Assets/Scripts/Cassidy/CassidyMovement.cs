using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CassidyMovement : MonoBehaviour {
    [SerializeField] private float baseHorizSpeed;
    [SerializeField] private float duckingHorizSpeed;
    [Space(10)]
    [SerializeField] private float baseJumpSpeed;
    [SerializeField] private float bodyBaseGravity;
    [SerializeField] private float baseHoverSpeed;
    [SerializeField] private float bodyHoverGravity;
    [Space(10)]
    [SerializeField] private float firstJumpTime;
    [SerializeField] private float secondJumpTime;
    [SerializeField] private bool secondJumpAvailable;
    [Space(10)]
    [SerializeField] public Transform groundCheck;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private LayerMask isGround;
    [Space(10)]
    [SerializeField] private float isFallingSpeed;
    [SerializeField] private float forceMultiplier;

    private bool left;
    private bool right;
    private bool up;
    private bool down;
    private bool hover;

    private bool started = false;
    
    private bool isJumping;
    private bool secondJumpReady;
    public float jumpTimeUsed;
    public float jumpLimit;
    private State state;

    private enum State
    {
        GROUNDED,
        RISING,
        FALLING,
        UNCONTROLLED
    }

    // Use this for initialization
    void Start () {
        secondJumpReady = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!started)
        {
            var body = gameObject.GetComponent<Rigidbody2D>();
            started = true;
            body.gravityScale = 7;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            ApplyKnockback(new Vector2(10, 10));
        }
        left = Input.GetKey(KeyCode.A);
        right = Input.GetKey(KeyCode.D);
        up = Input.GetKey(KeyCode.W);
        down = Input.GetKey(KeyCode.S);
        hover = Input.GetKey(KeyCode.Space);
	}

    private void FixedUpdate()
    {
        var body = gameObject.GetComponent<Rigidbody2D>();
        body.gravityScale = bodyBaseGravity;

        StateCheck();
        if (state == State.UNCONTROLLED)
        {
        }
        else if (hover)
        {
            body.gravityScale = bodyHoverGravity;
            HandleHover(body);
            secondJumpReady = false;
            state = State.RISING;
        }
        else
        {
            HandleMovement(body);
            HandleJumping(body);
        }
    }

    private void HandleMovement(Rigidbody2D body)
    {
        var effectiveHorizSpeed = baseHorizSpeed;
        if (down && state == State.GROUNDED)
        {
            effectiveHorizSpeed = duckingHorizSpeed;
        }
        Vector2 inputForce = GetInputForce(body, effectiveHorizSpeed);

        body.AddForce(inputForce);
    }

    private void HandleJumping(Rigidbody2D body)
    {
        var effectiveJumpSeed = baseJumpSpeed;
        if (up)
        {
            switch (state)
            {
                case State.GROUNDED:
                    if (!isJumping)
                    {
                        SetupJump(firstJumpTime);
                    }
                    break;
                case State.RISING:
                    jumpTimeUsed += Time.deltaTime;
                    if (jumpTimeUsed > jumpLimit)
                    {
                        state = State.FALLING;
                    }
                    else
                    {
                        body.velocity = new Vector2(body.velocity.x, effectiveJumpSeed);
                    }
                    break;
                case State.FALLING:
                    if (secondJumpReady && !isJumping)
                    {
                        SetupJump(secondJumpTime);
                        secondJumpReady = false;
                    }
                    break;

            }
            isJumping = true;
        }
        else
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

    private void HandleHover(Rigidbody2D body)
    {
        var effectiveHoverSpeed = baseHoverSpeed;
        Vector2 inputForce = GetHoverForce(body, effectiveHoverSpeed);
        body.AddForce(inputForce);
    }

    private Vector2 GetHoverForce(Rigidbody2D body, float effectiveHoverSpeed)
    {
        var vert = (up ? 1 : 0) - (down ? 1 : 0);
        var horiz = (right ? 1 : 0) - (left ? 1 : 0);
        var move = new Vector2(horiz, vert).normalized;
        var travelDir = body.velocity.normalized;
        var speed = Vector2.Dot(body.velocity, travelDir);
        var brakes = -travelDir * (speed / effectiveHoverSpeed);

        var inputForce = (move + brakes) * effectiveHoverSpeed * forceMultiplier;
        return inputForce;
    }

    private Vector2 GetInputForce(Rigidbody2D body, float effectiveHorizSpeed)
    {
        var horiz = (right ? 1 : 0) - (left ? 1 : 0);
        var move = new Vector2(horiz, 0);

        var travelDir = new Vector2(body.velocity.x, 0).normalized;
        var speed = Vector2.Dot(body.velocity, travelDir);
        var brakes = -travelDir * (speed / effectiveHorizSpeed);

        var inputForce = (move + brakes) * effectiveHorizSpeed * forceMultiplier;
        Debug.DrawRay(gameObject.transform.position + gameObject.transform.up, move, Color.cyan);
        Debug.DrawRay(gameObject.transform.position + gameObject.transform.up, brakes, Color.red);
        Debug.DrawRay(gameObject.transform.position + gameObject.transform.up / 2, body.velocity, Color.green);
        return inputForce;
    }

    private void StateCheck()
    {
        var body = gameObject.GetComponent<Rigidbody2D>();
        switch (state)
        {
            case State.UNCONTROLLED:
                if (IsGrounded() && body.velocity.y <= 0)
                {
                    state = State.FALLING;
                }
                break;
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
                    ResetJump();
                }
                break;
        }
    }

    private void ResetJump()
    {
        jumpLimit = 0;
        jumpTimeUsed = 0;
        secondJumpReady = secondJumpAvailable;
        state = State.GROUNDED;
    }

    private void SetupJump(float incomingJumpLimit)
    {
        jumpTimeUsed = 0;
        jumpLimit = incomingJumpLimit;
        state = State.RISING;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGround);
    }

    public void ApplyKnockback(Vector2 force)
    {
        var body = gameObject.GetComponent<Rigidbody2D>();
        body.AddForce(force, ForceMode2D.Impulse);
        state = State.UNCONTROLLED;
    }
}
