using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] ParticleSystem _hitParticles;
    [SerializeField] float _speed;
    [SerializeField] float _damage;

    public void Initialize(Vector2 direction)
    {
        Launch(direction);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            DestroyProjectile();
        } else if (collision.gameObject.CompareTag("Enemy"))
        {
            DealDamage(collision.gameObject);
            DestroyProjectile();
        }
    }

    void Launch(Vector2 direction)
    {
        Vector2 movement = direction.normalized * _speed;
        _rb.linearVelocity = movement;
    }
    void DealDamage(GameObject target)
    {
        if (target.TryGetComponent(out EntityHealth entityHealth))
        {
            entityHealth.loseHp(_damage);
        }
    }
    void DestroyProjectile()
    {
        ParticleSystem hitParticles = Instantiate(_hitParticles, transform.position, Quaternion.identity);
        Destroy(hitParticles.gameObject, 1f);
        Destroy(gameObject);
    }
}
