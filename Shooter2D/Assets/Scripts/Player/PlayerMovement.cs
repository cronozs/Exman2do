using UnityEngine;
using UnityEngine.InputSystem;

namespace Shooter
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed;

        private PlayerInput _playerInput;
        private InputAction _move;
        
        void Start()
        {
            _playerInput = GetComponent<PlayerInput>();
            _move = _playerInput.actions.FindAction("Player/Move");
        }

        void Update()
        {
            Move();
        }

        private void Move()
        {
            Vector2 direction = _move.ReadValue<Vector2>();
            Vector3 moveDir = direction.normalized;
            transform.position += moveDir * (speed * Time.deltaTime);
        }
    }
}
