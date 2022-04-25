using UnityEngine;

namespace aMaGGixZ
{
    public class PlayerInteractions : MonoBehaviour
    {
        #region Fields
        [SerializeField] private Transform _center;
        [SerializeField] private Transform _pickPoint;
        [SerializeField] private LayerMask _interactableLayers;
        [SerializeField] private LayerMask _dropableLayers;
        [SerializeField] private float _interactionDistance = 3;

        private IInteractable _cachedInteractable;
        private PickUpItem _itemHeld;
        #endregion

        #region Properties
        public bool CanPickUpItem => _itemHeld == null;
        #endregion

        #region Fields
        public void PickItem(PickUpItem pickUpItem, PickableItem fromItem)
        {
            _itemHeld = Instantiate(pickUpItem, _pickPoint);
            _itemHeld.SetPickableItem(fromItem);
        }

        private void Start()
        {
            InputManager.st.OnInteract += OnInteractionPressed;
        }

        private void Update()
        {
            IInteractable target = InteractableDetection();

            if (target != null)
            {
                target.Highlight();
            }
            else
            {
                _cachedInteractable?.Highlight(false);
            }

            _cachedInteractable = target;
        }

        private void OnInteractionPressed()
        {
            if (_cachedInteractable != null)
            {
                _cachedInteractable.Interact(this);
            }
            else if (!CanPickUpItem)
            {
                TryDropItem();
            }
        }

        private IInteractable InteractableDetection()
        {
            RaycastHit hit;

            if (Physics.Raycast(_center.position, _center.forward, out hit, _interactionDistance, _interactableLayers))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    return interactable;
                }
            }

            return null;
        }

        private void TryDropItem()
        {
            RaycastHit hit;

            if (Physics.Raycast(_center.position, _center.forward, out hit, _interactionDistance, _dropableLayers))
            {
                float angle = Vector3.Angle(Vector3.forward, hit.normal);
                if (angle > 80 && angle < 100)
                {
                    _itemHeld.Drop(hit.point, transform.rotation);
                    _itemHeld = null;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Vector3 targetPos;

            // PICK UP RAY
            Gizmos.color = Color.blue;
            targetPos = _center.position + _center.forward * _interactionDistance;
            Gizmos.DrawLine(_center.position, targetPos);
        }
        #endregion
    }
}