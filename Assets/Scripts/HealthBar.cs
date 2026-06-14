using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Image _hpFill;
    [SerializeField] EntityHealth _entityHp;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnHpChange(float currentHp, float maxHp)
    {
        _hpFill.fillAmount = currentHp / maxHp;
    }

    private void OnEnable()
    {
        _entityHp.OnHpChange += OnHpChange;
    }

    private void OnDisable()
    {
        _entityHp.OnHpChange -= OnHpChange;
    }
}
