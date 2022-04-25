using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

namespace aMaGGixZ
{
    public static class InputActionButtonExtensions
    {
        public static bool GetButton(this InputAction action) => action.ReadValue<float>() > 0;
        public static bool GetButtonDown(this InputAction action) => action.triggered && action.ReadValue<float>() > 0;
        public static bool GetButtonUp(this InputAction action) => action.triggered && action.ReadValue<float>() == 0;
    }

    public enum eControllerType { XBOX, PLAYSTATION, KEYBOARD }
    public class InputManager : MonoBehaviour
    {
        public static InputManager st; // ToDo Zenject

        #region Events
        public delegate void BasicEvent();
        public BasicEvent OnControllerChanged;
        public BasicEvent OnInteract;
        public BasicEvent OnZoomDown;
        public BasicEvent OnZoomUp;
        #endregion

        #region Fields
        private InputMaster _controls;
        #endregion

        #region Properties
        public Vector2 MoveInput { get; private set; }
        public Vector2 LookInput { get; private set; }
        public eControllerType CurrentController { get; private set; }
        #endregion

        #region Fields
        private void Awake()
        {
            st = this;
            _controls = new InputMaster();
            _controls.Enable();

            _controls.Player.Movement.performed += ctx => SetMoveInput(ctx.ReadValue<Vector2>());
            _controls.Player.LookInput.performed += ctx => SetLookInput(ctx.ReadValue<Vector2>());
            _controls.Player.Interact.performed += ctx => { OnInteract?.Invoke(); };

            InputSystem.onEvent += OnInputEvent;
        }

        private void SetMoveInput(Vector2 input)
        {
            MoveInput = input;
        }

        private void SetLookInput(Vector2 input)
        {
            LookInput = input;
        }

        private void Update()
        {
            if (InputActionButtonExtensions.GetButtonDown(_controls.Player.Zoom))
            {
                OnZoomDown?.Invoke();
            }
            if (InputActionButtonExtensions.GetButtonUp(_controls.Player.Zoom))
            {
                OnZoomUp?.Invoke();
            }
        }

        private void OnInputEvent(InputEventPtr ptr, InputDevice device)
        {
            if (device.displayName.Contains("PlayStation"))
            {
                ChangeControllerType(eControllerType.PLAYSTATION);
            }
            else if (device.displayName.Contains("Mouse") || device.displayName.Contains("Keyboard"))
            {
                ChangeControllerType(eControllerType.KEYBOARD);
            }
            else
            {
                ChangeControllerType(eControllerType.XBOX);
            }
        }

        private void ChangeControllerType(eControllerType type)
        {
            if (type != CurrentController)
            {
                CurrentController = type;
                Debug.Log($"Controller switched to {type}");
                OnControllerChanged?.Invoke();
            }
        }
        #endregion
    }
}