using System;
using UnityEngine;

public class Throwable : MonoBehaviour, IInteractableData
{
  public int count;
  public Sprite sprite;
  public GameObject obj;
  public float throwForce = 45.0f;

  public void Throw(Vector3 initialPos, Vector3 forward)
  {
    if (count == 0)
      return;

    count--;

    GameObject newObj = Instantiate(obj, initialPos, Quaternion.identity);
    newObj.GetComponent<Rigidbody>().AddForce(forward * throwForce, ForceMode.Impulse);
    Destroy(newObj, 7);
  }
}