using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 5.0f;
    private Vector2 direction = new Vector2(1, 1);

    void Update()
    {
        // Déplace la balle selon la direction et la vitesse
        transform.Translate(direction * speed * Time.deltaTime);

        // Détecte les collisions avec les murs
        CheckCollisionWithWalls();
    }

    void CheckCollisionWithWalls()
    {
        Vector2 position = transform.position;

        // Inverse la direction si la balle touche les bords de l'écran
        if (position.x <= -8.5f || position.x >= 8.5f)
        {
            direction.x = -direction.x;
        }

        if (position.y >= 4.5f)
        {
            direction.y = -direction.y;
        }

        // Vous pouvez ajouter une condition pour détecter le bas de l'écran si nécessaire
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Inverse la direction en fonction de l'objet avec lequel la balle entre en collision
        if (collision.gameObject.CompareTag("Paddle"))
        {
            direction.y = -direction.y;
        }
        else if (collision.gameObject.CompareTag("Brick"))
        {
            direction.y = -direction.y;
            Destroy(collision.gameObject);  // Détruit la brique
        }
    }
}
