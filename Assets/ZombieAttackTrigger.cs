using UnityEngine;

public class ZombieAttackTrigger : MonoBehaviour
{

    private Collider col;

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

    private void Start()
    {
        col = GetComponent<Collider>();
    }

    private void Update()
    {
        if (Enemy.isAttacking)
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
}