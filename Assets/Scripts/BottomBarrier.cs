using UnityEngine;

public class BottomBarrier : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Ball hit the bottom barrier");
            if (GameManager.instance != null)
            {
                GameManager.instance.EndGame();
            }
            else
            {
                Debug.LogError("GameManager instance is not found");
            }
        }
    }
}
