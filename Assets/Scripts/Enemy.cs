using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] AudioClip _deathSound;
    EntityHealth _entityHealth;
    NavMeshAgent _agent;
    GameObject _target;
    Canvas _canvas;
    
    private void Awake()
    {
        _entityHealth = GetComponent<EntityHealth>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _canvas = GetComponentInChildren<Canvas>();
    }
    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
        _entityHealth.OnDeath += DestroyEnemy;
        if (_canvas != null)
        {
            _canvas.worldCamera = Camera.main;
        }

    }

    // Update is called once per frame
    void Update()
    {
        _agent.SetDestination(_target.transform.position);
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
