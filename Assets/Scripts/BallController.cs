using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 30f;
    public float angleVariance = 15f;
    private Rigidbody rb;
    private Vector3 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
    }

    void Update()
    {
        if (GameManager.instance.gameStarted && rb.velocity == Vector3.zero)
        {
            LaunchBall();
        }
    }

    void FixedUpdate()
    {
        if (GameManager.instance.gameStarted)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
    }

    public void LaunchBall()
    {
        // Détermine un angle aléatoire dans un cône de ±30 degrés autour de l'axe Z négatif
        float angle = Random.Range(-30f, 30f);
        float angleRad = angle * Mathf.Deg2Rad;

        // Calculer la direction avec l'angle par rapport à l'axe Z négatif
        direction = new Vector3(Mathf.Sin(angleRad), 0, -Mathf.Cos(angleRad));

        // Appliquer la vitesse à la direction
        rb.velocity = direction * speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision with: " + collision.gameObject.name);

        Brick brick = collision.gameObject.GetComponent<Brick>();
        if (brick != null)
        {
            brick.Hit();
            GameManager.instance.AddScore(10);
        }

        Vector3 normal = collision.contacts[0].normal;
        Vector3 reflectDir = Vector3.Reflect(new Vector3(rb.velocity.x, 0, rb.velocity.z), new Vector3(normal.x, 0, normal.z));

        float angle = Random.Range(-angleVariance, angleVariance);
        float angleRad = angle * Mathf.Deg2Rad;

        reflectDir = Quaternion.Euler(0, angle, 0) * reflectDir;

        rb.velocity = reflectDir.normalized * speed;
    }
}
