using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float patrolRadius = 5f;
    public float speed = 2f;
    public float waitTime = 2f;
    public float rotationSpeed = 5f;

    public int hp = 200;

    private Vector3 startPos;
    private Vector3 targetPos;
    private float waitTimer;

    private Animator animator;

    private bool isDead = false;


    private void Start()
    {
        startPos = transform.position;
        targetPos = GetRandomPoint();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isDead) return; // ❌ не патрулирует после смерти

        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
            animator.SetBool("IsWalking", false);
            return;
        }

        Vector3 direction = (targetPos - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetPos);

        if (distance > 0.1f)
        {
            animator.SetBool("IsWalking", true);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }
        }
        else
        {
            animator.SetBool("IsWalking", false);
            waitTimer = waitTime;
            targetPos = GetRandomPoint();
        }
    }


    private Vector3 GetRandomPoint()
    {
        Vector2 offset = Random.insideUnitCircle * patrolRadius;
        return new Vector3(startPos.x + offset.x, transform.position.y, startPos.z + offset.y);
    }

    public void TakeDamage(int amount, string hitPart)
    {
        hp -= amount;
        Debug.Log($"{hitPart} получил {amount} урона. Осталось HP: {hp}");

        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;
        animator.SetTrigger("Die");
        Debug.Log("Враг погиб");
        this.enabled = false; // отключает скрипт Enemy.cs полностью
    }

}
