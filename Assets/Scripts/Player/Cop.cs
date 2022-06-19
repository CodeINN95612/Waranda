using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cop : MonoBehaviour {
    public Transform jugador;
    UnityEngine.AI.NavMeshAgent enemigo;
    public bool dentro = false;
Transform Player ;
 float MoveSpeed = 4;
 float MaxDist = 10;
 float MinDist = 5;
 float MidDist = 7;
 
	// Use this for initialization
	void Start () {
        enemigo = GetComponent<UnityEngine.AI.NavMeshAgent>();
        Player=GameObject.Find("Player").transform;

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
	void Update () {
	transform.LookAt(GameObject.Find("Player").transform);
     
     if(Vector3.Distance(transform.position,Player.position) >= MinDist){
     
          transform.position += transform.forward*MoveSpeed*Time.deltaTime;
     }

	}
}