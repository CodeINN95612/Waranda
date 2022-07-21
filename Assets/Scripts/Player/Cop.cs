using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cop : MonoBehaviour
{
  public Transform jugador;
  UnityEngine.AI.NavMeshAgent enemigo;
  public bool dentro = false;
  Transform Player;
  float MoveSpeed = 4;
  //float MaxDist = 15;
  float MinDist = 10;
  //float MidDist = 7;

  public GameObject huevo;
  private float Health = 100f;

  // Use this for initialization
  void Start()
  {
    enemigo = GetComponent<UnityEngine.AI.NavMeshAgent>();
    Player = GameObject.Find("Player").transform;

  }

  void OnTriggerEnter(Collider other)
  {
    if (other.tag == "Player")
    {
      dentro = true;
    }

  }

  void OnTriggerExit(Collider other)
  {
    if (other.tag == "Player")
    {
      dentro = false;
    }
  }
  // Update is called once per frame

  float time = 0f;
  void Update()
  {
    transform.LookAt(GameObject.Find("Player").transform);

    time += Time.deltaTime;
    if (Vector3.Distance(transform.position, Player.position) >= MinDist)
    {
      transform.position += transform.forward * MoveSpeed * Time.deltaTime;

      if (time >= 3f)
      {
        Vector3 frente = transform.TransformDirection(Vector3.forward);
        Vector3 arriba = transform.TransformDirection(Vector3.up);
        Vector3 inicial = transform.position + frente * 1.5f + arriba * 1.0f;
        GameObject newObj = Instantiate(huevo, inicial, Quaternion.identity);
        newObj.GetComponent<Rigidbody>().AddForce(frente * 10f, ForceMode.Impulse);
        newObj.GetComponent<Rigidbody>().AddForce(Vector3.up * 5f, ForceMode.Impulse);
        Destroy(newObj, 7);
        time = 0f;
      }

    }

  }

  public void Damage()
  {
    Health -= 15f;
    if (Health <= 0)
    {
      Die();
    }
  }

  public void Die()
  {
    Destroy(this.gameObject);
  }
}