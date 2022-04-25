using UnityEngine;

namespace aMaGGixZ
{
    [RequireComponent(typeof(Outline))]
    public class BaseInteractable : MonoBehaviour, IInteractable
    {
        #region Fields
        private Outline _outline;
        #endregion

        #region Fields
        private void Awake()
        {
            SetInitialState();
        }

        public void Interact(PlayerInteractions agent)
        {
            InteractInternal(agent);
        }

        public void Highlight(bool state = true)
        {
            HighlightInternal(state);
        }

        protected virtual void SetInitialState()
        {
            _outline = GetComponent<Outline>();
            _outline.enabled = false;
        }

        protected virtual void InteractInternal(PlayerInteractions agent)
        {
            Debug.Log($"Interacting with {name}");
        }

        protected virtual void HighlightInternal(bool state)
        {
            _outline.enabled = state;
        }
        #endregion
    }
}