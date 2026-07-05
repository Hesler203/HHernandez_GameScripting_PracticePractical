using NUnit.Framework.Constraints;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Character player;
    public (Character.Health, Character.Speed, Character.Attack, int) playerStats;
    [SerializeField] private Character.Health playerHealth = Character.Health.none;
    [SerializeField] private Character.Speed playerSpeed = Character.Speed.none;
    [SerializeField] public Character.Attack playerAttack = Character.Attack.none;
    [SerializeField] float jumpForce = 20f;
    float speedMultiplier = 100f;
    bool grounded;
    bool playerDied;
    private Rigidbody rb;
    private float horizontalInput;
    private float depthInput;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform gunFirePoint;
    private int score;

    void Start()
    {
        player = new Character(Character.Type.player);

        if (playerHealth != Character.Health.none)
        {
            player.SetHealth(playerHealth);
        }
        if (playerSpeed != Character.Speed.none)
        {
            player.SetSpeed(playerSpeed);
        }
        if (playerAttack != Character.Attack.none)
        {
            player.SetAttack(playerAttack);
        }
        playerStats = player.GetStats();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        depthInput = Input.GetAxis("Depth");

        HandleShooting();
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement()
    {
        Vector3 moveDirection = new Vector3(horizontalInput, 0, depthInput).normalized;
        rb.AddForce(moveDirection * (int)playerSpeed * speedMultiplier, ForceMode.Acceleration);
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }

    private void HandleShooting()
    {
        if (Input.GetButtonDown("Shoot"))
        {
            Instantiate<GameObject>(bulletPrefab, gunFirePoint.position, bulletPrefab.transform.rotation);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            HandleEnemyCollision(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    private void HandleEnemyCollision(GameObject enemy)
    {
        Character.Attack enemyAttack = enemy.GetComponent<EnemyController>().enemyAttack;
        playerDied = IsPlayerDead();
        if (!playerDied)
        {
            TakeDamage(enemyAttack);

            playerDied = IsPlayerDead();
            if (playerDied)
            {
                DisableEnemySpawner();
            }
        }
        Destroy(enemy);
    }

    private void TakeDamage(Character.Attack damage)
    {
        int newPlayerHealth = (int)playerHealth - (int)damage;
        player.SetHealth((Character.Health)newPlayerHealth);
    }

    private bool IsPlayerDead()
    {
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void DisableEnemySpawner()
    {
        FindAnyObjectByType<EnemySpawner>().enabled = false;
    }

    public void IncreaseScore(int scorePoints)
    {
        score += scorePoints;
        if (score >= 300)
        {
            DisableEnemySpawner();
        }
    }
}
