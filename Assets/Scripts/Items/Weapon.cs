using UnityEngine;

public class Weapon : MonoBehaviour, IInteractableData
{
  public GameObject prefab;
  public float range;
  public float damage;
  public Sprite sprite;
  public float percentage;
  public float percentageLossPerSecond;
  public float betweenShotsTime = 0.1f;
  public GameObject proyectil;
  public float shootForce;

  public void Recharge()
  {
    percentage = Mathf.Min(percentage + percentageLossPerSecond * Time.deltaTime, 100f);
  }

  private float shootTime = 0f;
  public void Shoot(Vector3 initialPosition, Vector3 forward)
  {
    if (percentage <= 0)
    {
      percentage = 0;
      return;
    }

    shootTime += Time.deltaTime;
    percentage -= percentageLossPerSecond * Time.deltaTime;
    if (shootTime > betweenShotsTime)
    {
      GameObject newObj = Instantiate(proyectil, initialPosition, Quaternion.identity);
      newObj.GetComponent<Rigidbody>().AddForce(forward * shootForce, ForceMode.Impulse);
      Destroy(newObj, 5);
      shootTime = 0f;
    }
  }
}