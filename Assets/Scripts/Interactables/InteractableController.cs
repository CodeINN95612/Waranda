using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableController : MonoBehaviour, IInteractable
{
    public InteractableType type = InteractableType.UnSet;

    IInteractor _interactor = null;
    IInteractableData _data = null;

    private void Start()
    {
        _data = GetComponent<IInteractableData>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        _interactor = other.GetComponent<IInteractor>();
        bool destroy = _interactor.InteractOnce(this);
        if (destroy)
        {
            _interactor.InteractEnd(this);
            GameObject.Destroy(this.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        bool destroy = _interactor.InteractContinous(this);
        if (destroy)
        {
            _interactor.InteractEnd(this);
            //GameObject.Destroy(this.gameObject);
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        bool destroy = _interactor.InteractEnd(this);
        if (destroy)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    public T GetData<T>() where T : IInteractableData
    {
        if (_data is null)
        {
            Debug.LogError("No existe Data seleccionada en el interactable");
        }
        return (T)_data;
    }

    InteractableType IInteractable.GetType()
    {
        return type;
    }
}
