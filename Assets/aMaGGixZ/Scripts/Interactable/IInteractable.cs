namespace aMaGGixZ
{
    public interface IInteractable
    {        
        void Interact(PlayerInteractions agent);
        void Highlight(bool state = true);
    }
}