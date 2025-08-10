using Fusion;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : NetworkBehaviour, ISpawned
{
    [SerializeField] private CharacterController _characterController;    
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _jumpForce = 1f;
    [SerializeField] private InputActionReference _lookInput;
    [SerializeField] private InputActionReference _jumpInput;
    [SerializeField] private InputActionReference _moveInput;   
    [SerializeField] private Animator _animator;

    private Vector3 _velocity; 

    public override void Spawned()
    {
        base.Spawned();
        if (!HasStateAuthority) return;
        _characterController.enabled = true;
    }
    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();
        if (!HasStateAuthority) return;
        UpdateFalling();
        UpdatMovement();

        _characterController.Move(_velocity*Runner.DeltaTime);
        _animator.SetFloat("speed", GetMoveDirection().magnitude);
        UpdateRotation();
        
    }      

    private void UpdatMovement()
    {
        var direction = _speed * GetMoveDirection();
        _velocity.x = direction.x;
        _velocity.z = direction.z;
        
        
    }
    private void UpdateFalling()
    {
        if (_characterController.isGrounded)
        {
            _velocity.y = -1f;
        }        
            _velocity.y += Physics.gravity.y * Runner.DeltaTime;       
    }

    private void UpdateRotation()
    {
        var lookValues = _moveInput.action.ReadValue<Vector2>();
        transform.Rotate(0, lookValues.x * _rotateSpeed * Runner.DeltaTime, 0);
    }


    private Vector3 GetMoveDirection()
    {
        var inputValue = _moveInput.action.ReadValue<Vector2>();
        var direction = transform.forward * inputValue.y + transform.right * inputValue.x;
        return direction = direction.normalized;
    }
   
}