using Fusion;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : NetworkBehaviour, ISpawned
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private NetworkCharacterController networkCharacterController;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed;  
    [SerializeField] private InputActionReference _lookInput;
    [SerializeField] private InputActionReference _jumpInput;
    [SerializeField] private InputActionReference _moveInput;   
    [SerializeField] private Animator _animator;
    private bool _isJump;
    private float _rotateX;

    [Networked, OnChangedRender(nameof(OnVelocityChange))   ]
    private Vector3 Velocity { get; set; }
    private void OnVelocityChange()
    {
        _animator.SetFloat("speed", Velocity.magnitude);
    }

    public override void Spawned()
    {
        base.Spawned();
        if (!HasStateAuthority) return;
        _characterController.enabled = true;
    }
    void Update()
    {
        var direction = _speed * GetMoveDirection();
        Velocity = _speed * direction;
        if (_jumpInput.action.triggered)
        {
            _isJump = true;
        }
        var lookValues = _lookInput.action.ReadValue<Vector2>();
        _rotateX += lookValues.x * _rotateSpeed * Time.deltaTime;

    }
    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();
        if (!HasStateAuthority) return;
        UpdateMovement();
        UpdateRotation();

    }      

    private void UpdateMovement()
    {
       
        networkCharacterController.Move(Velocity);            
        if(_isJump)
        {
            networkCharacterController.Jump();
            _isJump = false;
        }
       
        
    }
    private Vector3 GetMoveDirection()
    {
        var inputValues = _moveInput.action.ReadValue<Vector2>();
        var direction = transform.forward * inputValues.y + transform.right * inputValues.x;
        return direction = direction.normalized;
    }

    private void UpdateRotation()
    {
        transform.Rotate(0, _rotateX, 0);
        _rotateX = 0; 
    }
   
}