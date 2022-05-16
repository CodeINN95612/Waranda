using UnityEngine;

public class Weapon : MonoBehaviour, IInteractableData
{
    public GameObject prefab;
    public float range;
    public float damage;
    public Sprite sprite;
    public float percentage;
}