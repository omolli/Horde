using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
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
        }
    }

    void Launch(Vector2 direction)
    {
        Vector2 movement = direction.normalized * _speed;
        _rb.linearVelocity = movement;
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
