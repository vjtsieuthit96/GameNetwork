using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;    
    [SerializeField] private float _speed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _jumpForce = 1f;
    private float _gravity = -9.81f;
    [SerializeField] private InputActionReference _lookInput;
    [SerializeField] private InputActionReference _jumpInput;
    [SerializeField] private InputActionReference _moveInput;   
    [SerializeField] private Animator _animator;

    private Vector3 _velocity;
    private bool _isGrounded;

    private void Update()
    {
        HandleJump();

        _characterController.Move(_speed * Time.deltaTime * GetMoveDirection() + _velocity * Time.deltaTime);
        Rotation();
        _animator.SetFloat("speed", GetMoveDirection().magnitude);
    }

    private void HandleJump()
    {
        _isGrounded = _characterController.isGrounded;

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        if (_isGrounded && _jumpInput.action.triggered)
        {
            _velocity.y = Mathf.Sqrt(_jumpForce * -2f * _gravity);
            _animator.SetTrigger("Jump");
        }

        _velocity.y += _gravity * Time.deltaTime;
    }

    private Vector3 GetMoveDirection()
    {
        var inputValue = _moveInput.action.ReadValue<Vector2>();
        var direction = transform.forward * inputValue.y + transform.right * inputValue.x;
        return direction = direction.normalized;
    }

    private void Rotation()
    {
        var lookValues = _moveInput.action.ReadValue<Vector2>();
        transform.Rotate(0, lookValues.x * _rotateSpeed * Time.deltaTime, 0);
    }
}