using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private Rigidbody rb;
    private float speed;

    public void InitializeTarget(Transform playerPosition)
    {
        target = playerPosition;
    }

    void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        speed = (int)GetComponent<EnemyController>().enemyStats.Item2;
        rb = GetComponent<Rigidbody>();
        rb.AddForce(target.position.normalized * speed, ForceMode.Acceleration);
    }
}
