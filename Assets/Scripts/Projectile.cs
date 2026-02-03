using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] ParticleSystem _hitParticles;
    [SerializeField] float _speed;
    [SerializeField] float _damage;
    [SerializeField] AudioClip _hitEnemySound;

    public void Initialize(Vector2 direction)
    {
        Launch(direction);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            DestroyProjectile();
        } 
        else if (collision.gameObject.CompareTag("Enemy"))
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
            entityHealth.LoseHp(_damage);
            AudioManager.Instance.PlayAudio(_hitEnemySound, AudioManager.SoundType.SFX, 1.0f, false);
        }
    }
    void DestroyProjectile()
    {
        ParticleSystem hitParticles = Instantiate(_hitParticles, transform.position, Quaternion.identity);
        Destroy(hitParticles.gameObject, 1f);
        Destroy(gameObject);
    }
}
