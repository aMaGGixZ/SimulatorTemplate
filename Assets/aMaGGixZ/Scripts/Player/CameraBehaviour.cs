using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace aMaGGixZ
{
    public class CameraBehaviour : MonoBehaviour
    {
        #region Fields
        [SerializeField] private PlayerMovement _mover;
        [SerializeField] private Camera _camera;
        [SerializeField] private float _bobSpeed = 14;
        [SerializeField] private float _bobAmount = 0.05f;

        private float _defaultYPosition;
        private float _timer;
        private float _initialFoV;
        private float _currentFoV;
        #endregion

        #region Methods
        void Start()
        {
            _initialFoV = _camera.fieldOfView;
            _defaultYPosition = _camera.transform.localPosition.y;
            InputManager.st.OnZoomDown += OnZoomDown;
            InputManager.st.OnZoomUp += OnZoomUp;
        }

        void Update()
        {
            HandleBob();
        }

        private void OnZoomDown()
        {
            _camera.fieldOfView = _initialFoV - 20;
        }

        private void OnZoomUp()
        {
            _camera.fieldOfView = _initialFoV;
        }

        private void HandleBob()
        {
            if (!_mover.IsGrounded) return;

            if (Mathf.Abs(_mover.MoveDirection.magnitude) > 0.1f)
            {
                _timer += Time.deltaTime * _bobSpeed;

                float posX = _camera.transform.localPosition.x;
                float posY = _defaultYPosition + Mathf.Sin(_timer) * _bobAmount;
                float posZ = _camera.transform.localPosition.z;

                _camera.transform.localPosition = new Vector3(posX, posY, posZ);
            }
        }
        #endregion
    }
}