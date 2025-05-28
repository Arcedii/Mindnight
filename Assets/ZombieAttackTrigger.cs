using UnityEngine;

public class ZombieAttackTrigger : MonoBehaviour
{

    private Collider col;
    private Enemy enemy;

    private void Start()
    {
        col = GetComponent<Collider>();
        enemy = GetComponentInParent<Enemy>(); // находим врага в родителях
    }

    private void Update()
    {
        if (enemy != null && enemy.isAttacking)
        {
            if (!col.enabled)
                col.enabled = true;
        }
        else
        {
            if (col.enabled)
                col.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerDamageHandler handler = other.GetComponent<PlayerDamageHandler>();
            if (handler != null)
            {
                handler.TakeHit();
            }
        }
    }
}