public interface IInteractable
{
    public InteractableType GetType();
    public T GetData<T>() where T : IInteractableData;
}