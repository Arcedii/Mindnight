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

    public float aggroRadius = 10f;    // Радиус агра
    public float attackRange = 2f;     // Дистанция атаки

    private GameObject player;
    public bool isAttacking = false; // УБРАТЬ static




    private void Start()
    {
        startPos = transform.position;
        targetPos = GetRandomPoint();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");

    }

    private void Update()
    {
        if (isDead) return;

        float playerDistance = Vector3.Distance(transform.position, player.transform.position);

        // Если игрок в радиусе атаки
        if (playerDistance <= attackRange)
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("Attack", true);
            isAttacking = true;

            // Здесь можно вызывать урон игроку или таймер
            return;
        }
        else
        {
            animator.SetBool("Attack", false);
            isAttacking = false;
        }

        // Если игрок в радиусе агра — идём за ним
        if (playerDistance <= aggroRadius)
        {
            animator.SetBool("IsWalking", true);
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            RotateTowards(player.transform.position);
            return;
        }

        // Патрулирование
        if (waitTimer > 0)
        {
            waitTimer -= Time.deltaTime;
            animator.SetBool("IsWalking", false);
            return;
        }

        Vector3 patrolDirection = (targetPos - transform.position).normalized;
        float patrolDistance = Vector3.Distance(transform.position, targetPos);

        if (patrolDistance > 0.1f)
        {
            animator.SetBool("IsWalking", true);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            RotateTowards(targetPos);
        }
        else
        {
            animator.SetBool("IsWalking", false);
            waitTimer = waitTime;
            targetPos = GetRandomPoint();
        }
    }

    private void RotateTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
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
        GameManager.instance.EnemyDied(); // вызов после Debug.Log("Враг погиб");
        this.enabled = false; // отключает скрипт Enemy.cs полностью
    }

}
