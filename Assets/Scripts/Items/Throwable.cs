using System;
using UnityEngine;

public class Throwable : MonoBehaviour, IInteractableData
{
  public int count;
  public Sprite sprite;
  public GameObject obj;
  public float upForce = 2.0f;
  public float frontForce = 3.0f;

  public void Throw(Vector3 initialPos, Vector3 forward)
  {
    if (count == 0)
      return;

    count--;

    GameObject newObj = Instantiate(obj, initialPos, Quaternion.identity);
    newObj.GetComponent<Rigidbody>().AddForce(Vector3.up * upForce, ForceMode.Impulse);
    newObj.GetComponent<Rigidbody>().AddForce(forward * frontForce, ForceMode.Impulse);
    Destroy(newObj, 7);
  }
}