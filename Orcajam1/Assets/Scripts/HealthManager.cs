using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private Image Healthbar;

    [SerializeField] private int MaxHealth = 100;

    private int _health;

    public int Health
    {
        get { return _health; }

        set
        {
            _health = Mathf.Clamp(value, 0, MaxHealth);
            if (Healthbar != null)
            {
                Healthbar.fillAmount = ((float)Health) / ((float)MaxHealth);
            }
        }
    }

    private void Start()
    {
        Health = MaxHealth;
    }
}
