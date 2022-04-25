using UnityEngine;

namespace aMaGGixZ
{
    public class PickableItem : BaseInteractable
    {
        #region Fields
        [SerializeField] private float _yOffset;
        [SerializeField]  private PickUpItem _pickUpItem;
        #endregion

        #region Methods
        public void ReEnable(Vector3 position, Quaternion rotation)
        {
            transform.position = new Vector3(position.x, position.y + _yOffset, position.z);
            transform.rotation = rotation;
        }

        protected override void InteractInternal(PlayerInteractions agent)
        {
            if (agent.CanPickUpItem)
            {
                if(_pickUpItem == null)
                {
                    Debug.Log($"#PICKABLE ITEM# {name} pickable item has no pick up item set.");
                    return;
                }
                agent.PickItem(_pickUpItem, this);
                gameObject.SetActive(false);
            }
        }
        #endregion
    }
}