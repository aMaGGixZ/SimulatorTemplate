using UnityEngine;

namespace aMaGGixZ
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        #region Fields
        [SerializeField] private float _speed = 12f;

        private CharacterController _controller;
        private Vector3 _moveDirection;
        private Vector3 _gravity;
        #endregion

        #region Properties
        public Vector3 MoveDirection => _moveDirection;
        public bool IsGrounded => _controller.isGrounded;
        #endregion

        #region Methods
        private void Start()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void Update()
        {
            HandleMovement();
            HandleGravity();
        }

        private void HandleMovement()
        {
            float x = InputManager.st.MoveInput.x;
            float z = InputManager.st.MoveInput.y;

            _moveDirection = transform.right * x + transform.forward * z;
            _moveDirection = Vector3.ClampMagnitude(_moveDirection, 1);
            _controller.Move(_moveDirection * _speed * Time.deltaTime);
        }

        private void HandleGravity()
        {
            if (IsGrounded && _gravity.y < 0)
            {
                _gravity.y = -2f;
            }

            _gravity.y += Physics.gravity.y * Time.deltaTime;

            _controller.Move(_gravity * Time.deltaTime);
        }
        #endregion
    }
}