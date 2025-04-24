using UnityEngine;

public class PlayerStaffController : MonoBehaviour
{
    [SerializeField] Transform _tip;
    [SerializeField] Projectile _projectile;
    [SerializeField] AudioClip _shootSound;
    [SerializeField] float _fireRate;


    float _nextFireTime;
    Vector2 _direction;

    void Update()
    {
        if (Time.timeScale == 0f)
        {
            return;
        }

        SetDirection();
        RotateStaff();
        if (Input.GetButton("Fire1") && Time.time >= _nextFireTime)
        {
            _nextFireTime = Time.time + 1f / _fireRate;
            Shoot();
        }

    }

    void Shoot()
    {
        Projectile newProjectile = Instantiate(_projectile, _tip.position, Quaternion.identity);
        newProjectile.Initialize(_direction);
        AudioManager.Instance.PlayAudio(_shootSound, AudioManager.SoundType.SFX, 0.5f, false);

    }

    void RotateStaff() 
    {

        float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void SetDirection()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        _direction = (mousePosition - (Vector2)transform.position).normalized;
    }
}
