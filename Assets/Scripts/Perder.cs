using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perder : MonoBehaviour
{
  private void OnTriggerEnter(Collider other)
  {
    PlayerMojadoController mojado = other.gameObject.GetComponent<PlayerMojadoController>();
    if (mojado != null)
    {
      Debug.Log("PruebaZ!!");
      mojado.Damage(float.MaxValue);
      return;
    }

    Cop cop = other.gameObject.GetComponent<Cop>();
    if (cop != null)
    {
      cop.Die();
      return;
    }

  }
}
