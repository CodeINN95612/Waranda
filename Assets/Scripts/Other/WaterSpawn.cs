using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpawn : MonoBehaviour
{
  public GameObject agua;
  public GameObject pos;
  public float secondsBetweenSpawn;
  public float passedSeconds = 0f;

  void Update()
  {
    passedSeconds += Time.deltaTime;
    if (passedSeconds > secondsBetweenSpawn)
    {
      GameObject newObj = Instantiate(agua, pos.transform.position, Quaternion.identity);
      newObj.GetComponent<Rigidbody>().AddForce(Vector3.down * 0.1f, ForceMode.Impulse);
      Destroy(newObj, 3);
      passedSeconds = 0;
    }
  }
}
