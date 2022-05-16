public interface IInteractor
{
    public bool InteractOnce(IInteractable interactable);
    public bool InteractContinous(IInteractable interactable);
    public bool InteractEnd(IInteractable interactable);
}