using UnityEngine;
using System.Collections.Generic;

public class Coin : MonoBehaviour
{
    private static readonly HashSet<string> collectedCoins = new HashSet<string>();

    [SerializeField] private string coinId;

    private void Awake()
    {
        if (string.IsNullOrWhiteSpace(coinId))
        {
            coinId = $"{gameObject.scene.name}:{gameObject.name}:{transform.position}";
        }

        if (collectedCoins.Contains(coinId))
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Coin collected!");
            collectedCoins.Add(coinId);
            Destroy(gameObject);
            SceneTransition.LoadQuestionScene(collision.transform);
        }
    }
}
