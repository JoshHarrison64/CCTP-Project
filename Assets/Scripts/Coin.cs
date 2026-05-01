using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Coin collected!");
            ScoreManager.Instance.AddScore(1);
            SceneTransition.LoadQuestionScene(collision.transform);
            Destroy(gameObject);
        }
    }
}
