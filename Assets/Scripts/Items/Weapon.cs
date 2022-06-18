using UnityEngine;

public class Weapon : MonoBehaviour, IInteractableData
{
  public GameObject prefab;
  public float range;
  public float damage;
  public Sprite sprite;
  public float percentage;
  public float percentageLossPerSecond;
  public GameObject proyectil;
  public float upForce;
  public float frontForce;

  public void Recharge()
  {
    percentage = Mathf.Min(percentage + percentageLossPerSecond * Time.deltaTime, 100f);
  }

  public void Shoot(Vector3 initialPosition, Vector3 forward)
  {
    if (percentage <= 0)
    {
      percentage = 0;
      return;
    }

    percentage -= percentageLossPerSecond * Time.deltaTime;


    GameObject newObj = Instantiate(proyectil, initialPosition, Quaternion.identity);
    newObj.GetComponent<Rigidbody>().AddForce(Vector3.up * upForce, ForceMode.Impulse);
    newObj.GetComponent<Rigidbody>().AddForce(forward * frontForce, ForceMode.Impulse);
  }
}