using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed = 20f;

    void Start()
    {
        rb.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.AddRelativeForce(Vector3.forward * speed, ForceMode.VelocityChange);
    }
}
