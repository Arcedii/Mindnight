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

    private void Start()
    {
        startPos = transform.position;
        targetPos = GetRandomPoint();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
            animator.SetBool("IsWalking", false); // ⛔ стоит — idle
            return;
        }

        Vector3 direction = (targetPos - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetPos);

        if (distance > 0.1f)
        {
            animator.SetBool("IsWalking", true); // ✅ движется — walk
            

            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
            }
        }
        else
        {
            animator.SetBool("IsWalking", false); // ⛔ дошёл — idle
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
        Debug.Log("Враг погиб");
        // Воспроизвести анимацию, отключить AI и т.д.
    }
}
