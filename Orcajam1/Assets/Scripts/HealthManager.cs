using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private int MaxHealth = 100;

    private int _health;

    public int Health
    {
        get { return _health; }

        set
        {
            _health = Mathf.Clamp(value, 0, MaxHealth);

            if (_health == 0)
            {
                //badstuff()
            }
        }
    }

    private void Start()
    {
        Health = MaxHealth;
    }
}
