using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] AudioClip _deathSound;
    EntityHealth _entityHealth;
    private void Awake()
    {
        _entityHealth = GetComponent<EntityHealth>();
    }
    void Start()
    {
        _entityHealth.OnDeath += DestroyEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyEnemy()
    {
        AudioManager.Instance.PlayAudio(_deathSound, AudioManager.SoundType.SFX, 1.0f, false);
        Destroy(gameObject);

    }

    void OnDisable()
    {
        _entityHealth.OnDeath -= DestroyEnemy;
    }
}
