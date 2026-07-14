using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    private NumberManager numberManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        numberManager = FindFirstObjectByType<NumberManager>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Door: Player entered the trigger zone.");
        if (other.gameObject.CompareTag("Player"))
        {
            if (numberManager != null && numberManager.isCorrect)
            {
                ScoreManager.Instance.AddScore(1);
                SceneTransition.ReturnToStoredScene();
            }
        }
    }
}
