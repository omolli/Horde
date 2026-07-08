using UnityEngine;

public class PlayerStaffController : MonoBehaviour
{
    [SerializeField] Transform _tip;
    [SerializeField] Projectile _projectile;
    [SerializeField] AudioClip _shootSound;
    [SerializeField] AudioClip _blastSound;
    [SerializeField] float _fireRate;
    [SerializeField] ParticleSystem _blastParticles;
    [SerializeField] float _blastRate;
    [SerializeField] float _blastRadius;
    [SerializeField] float _blastDamage;


    float _nextFireTime;
    float _nextBlastTime;
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
        else if (Input.GetButton("Fire2") && Time.time >= _nextBlastTime)
        {
            _nextBlastTime = Time.time + _blastRate;
            StartBlast();
        }
    }

    void Shoot()
    {
        Projectile newProjectile = Instantiate(_projectile, _tip.position, Quaternion.identity);
        newProjectile.Initialize(_direction);
        AudioManager.Instance.PlayAudio(_shootSound, AudioManager.SoundType.SFX, 0.5f, false);

    }

    void StartBlast()
    {
        AudioManager.Instance.PlayAudio(_blastSound, AudioManager.SoundType.SFX, 0.5f, false);
        Invoke(nameof(Blast), 4f);
    }

    void Blast()
    {
        ParticleSystem blastParticles = Instantiate(_blastParticles, transform.position, Quaternion.identity);
        Destroy(blastParticles.gameObject, 2f);
        Collider2D[] hitEntities = Physics2D.OverlapCircleAll(transform.position, _blastRadius);

        foreach(Collider2D hit in hitEntities)
        {
            if (hit.CompareTag("Enemy")) {
                if (hit.TryGetComponent(out EntityHealth entityHealth))
                {
                    entityHealth.LoseHp(_blastDamage);
                }
            }
        }
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
