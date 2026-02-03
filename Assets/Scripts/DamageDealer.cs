using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] float _dmg;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    
    void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        if (collision.gameObject.TryGetComponent(out EntityHealth entityHealth))
        {
            entityHealth.LoseHp(Time.fixedDeltaTime * _dmg);
        }
    }
}
