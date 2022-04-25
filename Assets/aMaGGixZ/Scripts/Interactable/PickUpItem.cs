using UnityEngine;

namespace aMaGGixZ
{
    public class PickUpItem : MonoBehaviour
    {
        #region Fields
        private PickableItem _pickableItem;
        #endregion

        #region Methods
        public void SetPickableItem(PickableItem pickableItem)
        {
            _pickableItem = pickableItem;
        }

        public void Drop(Vector3 position, Quaternion rotation)
        {
            _pickableItem.gameObject.SetActive(true);
            _pickableItem.ReEnable(position, rotation);
            Destroy(gameObject);
        }
        #endregion
    }
}