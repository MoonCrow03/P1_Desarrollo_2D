using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jumping : MonoBehaviour
{
    [SerializeField] private float JumpHeight;
    [SerializeField] private float TimeToPeak;
    [SerializeField] private float GravityMultiplier;
    [SerializeField] private float PressTimeToMaxJump;

    private bool _gravityDirection;
    private float _lastVelocity_Y;
    private float _jumpStartedTime;
    private Rigidbody2D _rigidbody2D;
    private GroundCheck _groundCheck;

    public static Action OnChangeGravity;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _groundCheck = GetComponentInChildren<GroundCheck>();
        _gravityDirection = false;
    }

    private void FixedUpdate()
    {
        if (PeakReached())
        {
            _gravityDirection = !_gravityDirection;
            OnChangeGravity?.Invoke();
        }
    }

    private void OnEnable()
    {
        PlayerInput.OnJump += OnJump;
    }
    private void OnDisable()
    {
        PlayerInput.OnJump -= OnJumpFinished;
    }
    private void OnJump()
    {
        if (CanJump())
        {
            OnJumpStarted();
        }
    }
    
    private bool PeakReached()
    {
        bool reached= ((_lastVelocity_Y * _rigidbody2D.velocity.y) < 0);
        _lastVelocity_Y = _rigidbody2D.velocity.y;
        return reached;
    }
    
    bool CanJump()
    {
        return _groundCheck.IsGrounded;
    }

    void OnJumpStarted()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, GetVelocity());
        _jumpStartedTime = Time.time;
        SetGravity();
    }

    void OnJumpFinished()
    {
        float fractionOfTimePressed =  1 / Mathf.Clamp01((Time.time - _jumpStartedTime) / PressTimeToMaxJump);
        _rigidbody2D.gravityScale *= fractionOfTimePressed;
    }
    
    private float GetVelocity()
    {
        return 2 * JumpHeight / TimeToPeak;
    }

    private void SetGravity()
    {
        float gravity = -2 * JumpHeight / (TimeToPeak * TimeToPeak);
        _rigidbody2D.gravityScale = gravity / Physics2D.gravity.y;
    }
}
