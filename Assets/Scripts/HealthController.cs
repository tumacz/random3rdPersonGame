using System;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public event Action<HealthController> OnDisactive;

    [SerializeField] private int health = 50;
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            gameObject.SetActive(false);
            OnDisactive?.Invoke(this);
            health = 50;
        }
    }

    public void IncreaseHealth(int healthBonus)
    {
        health += healthBonus;
    }
}
