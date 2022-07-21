using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agua : MonoBehaviour
{
  public ParticleSystem particles;
  public float damage = 3f;
  private CapsuleCollider _collider;
  private Rigidbody _rb;
  private MeshRenderer _renderer;

  private void Start()
  {
    _collider = GetComponent<CapsuleCollider>();
    _rb = GetComponent<Rigidbody>();
    _renderer = GetComponent<MeshRenderer>();

    particles.Stop();
  }

  private void OnCollisionEnter(Collision other)
  {
    if (other.gameObject.CompareTag("Agua"))
    {
      return;
    }
    var duration = particles.main.duration;
    particles.Play();

    Cop cop = other.gameObject.GetComponent<Cop>();
    if (cop != null)
    {
      cop.Damage();
    }

    Destroy(_collider);
    Destroy(_renderer);
    Destroy(_rb);
    Destroy(this.gameObject, duration);

    var damageable = other.gameObject.GetComponent<IDamageable>();
    damageable?.Damage(damage);
  }
}
