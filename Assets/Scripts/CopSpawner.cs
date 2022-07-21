using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopSpawner : MonoBehaviour
{

  public GameObject prefab;
  public float seconds;

  private float timer;
  void Update()
  {
    timer += Time.deltaTime;
    if (timer >= seconds)
    {
      Spawn();
      timer = 0f;
    }
  }

  private void Spawn()
  {
    Instantiate(prefab, this.transform.position, prefab.transform.rotation);
  }
}
