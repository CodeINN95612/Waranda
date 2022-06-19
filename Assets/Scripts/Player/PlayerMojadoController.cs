using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMojadoController : MonoBehaviour, IDamageable
{
  public float nivelActual = 0.0f;
  public float maximoNivel = 100.0f;

  public bool WasDamaged = false;
  public void Damage(float value)
  {
    nivelActual = Mathf.Min(maximoNivel, nivelActual + value);
    WasDamaged = true;
  }

  public void Curar(float val)
  {
    if (nivelActual <= 0f)
    {
      nivelActual = 0;
      return;
    }

    nivelActual -= val * Time.deltaTime;
    WasDamaged = true;
  }
}
