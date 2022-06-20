using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Huevo : MonoBehaviour
{
  public ParticleSystem particles;
  public float explotionRadius;
  public float damage;
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
    Explode();

    Destroy(_collider);
    Destroy(_renderer);
    Destroy(_rb);
    Destroy(this.gameObject, duration);
  }

  private void OnDrawGizmos()
  {
    Gizmos.DrawWireSphere(transform.position, explotionRadius);
  }

  public void Explode()
  {
    Collider[] colliders = Physics.OverlapSphere(transform.position, explotionRadius);
    foreach (Collider col in colliders)
    {
      IDamageable damageable = col.gameObject.GetComponent<IDamageable>();
      if (damageable != null)
      {
        damageable.Damage(damage);
      }
    }
  }
}
