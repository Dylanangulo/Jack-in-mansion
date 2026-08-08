using UnityEngine;

public class take_health : MonoBehaviour
{
    [SerializeField] private float health;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IHealing>(out var healer))
        {
            healer.Take_health(health);
            Debug.Log("Curacion aplicada: " + healer);

            Destroy(gameObject);
        }
        else
        {
            Debug.Log("El objeto no tiene IHealt");
        }
    }
}
