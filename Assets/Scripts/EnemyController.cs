using UnityEditor.Callbacks;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Character enemy;
    public (Character.Health, Character.Speed, Character.Attack, int) enemyStats;
    [SerializeField] private Character.Health enemyHealth;
    [SerializeField] private Character.Speed enemySpeed;
    [SerializeField] public Character.Attack enemyAttack;
    [SerializeField] private float damageBounceForce;
    private bool enemyDied;

    public void InitializeEnemy(Character.Type enemyType)
    {
        this.enemy = new Character(enemyType);
        if (enemyHealth != Character.Health.none)
        {
            enemy.SetHealth(enemyHealth);
        }
        if (enemySpeed != Character.Speed.none)
        {
            enemy.SetSpeed(enemySpeed);
        }
        if (enemyAttack != Character.Attack.none)
        {
            enemy.SetAttack(enemyAttack);
        }
        enemyStats = enemy.GetStats();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.AddRelativeForce(Vector3.back * damageBounceForce, ForceMode.VelocityChange);

            PlayerController player = FindAnyObjectByType<PlayerController>();
            HandleDamage(player);
        }
    }

    private void HandleDamage(PlayerController player)
    {
        enemyDied = IsEnemyDead();
        if (!enemyDied)
        {
            TakeDamage(player.playerAttack);
        }
        else
        {
            player.IncreaseScore(enemyStats.Item4);
            Destroy(this.gameObject);
        }
    }

    private void TakeDamage(Character.Attack damage)
    {
        int newEnemyHealth = (int)enemyHealth - (int)damage;
        enemy.SetHealth((Character.Health)newEnemyHealth);
    }

    private bool IsEnemyDead()
    {
        if (enemyHealth <= 0)
        {
            enemyHealth = 0;
            return true;
        }
        else
        {
            return false;
        }
    }
}