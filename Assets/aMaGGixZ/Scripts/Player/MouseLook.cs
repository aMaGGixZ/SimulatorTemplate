using UnityEngine;

namespace aMaGGixZ
{
    public class MouseLook : MonoBehaviour
    {
        #region Fields
        [SerializeField] private float _mouseSensitivity = 100;
        [SerializeField] private Transform _playerBody;

        private float _xRotation;
        #endregion

        #region Methods
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            float relativeSensitivity = SetSensitivity();
            float mouseX = InputManager.st.LookInput.x * relativeSensitivity * Time.deltaTime;
            float mouseY = InputManager.st.LookInput.y * relativeSensitivity * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90, 90);

            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            _playerBody.Rotate(Vector3.up * mouseX);
        }

        private float SetSensitivity()
        {
            float sensitivity = _mouseSensitivity;

            if(InputManager.st.CurrentController == eControllerType.KEYBOARD)
            {
                sensitivity = _mouseSensitivity / 10;
            }

            return sensitivity;
        }
        #endregion
    }
}