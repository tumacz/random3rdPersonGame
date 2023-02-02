using UnityEngine;

public class DamageController : MonoBehaviour
{
    [SerializeField] private int damage = 10;
 
    private void OnCollisionEnter(Collision other)
    {
        HealthController healthController = other.gameObject.GetComponent<HealthController>();
        if (healthController != null)
        {
            healthController.TakeDamage(damage);
            Hit();
        }
    }

    private void Hit()
    {
        Destroy(gameObject);
    }
}
