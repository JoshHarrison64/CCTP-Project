using UnityEngine;

public class NumberInput : MonoBehaviour
{
    public int numberValue = 0; // number between 0 and 9
    [SerializeField] private Sprite[] numberSprites;
    private SpriteRenderer numberSpriteRenderer;
    [SerializeField] private NumberManager numberManager;
    void Start()
    {
        numberSpriteRenderer = GetComponent<SpriteRenderer>();
        numberManager = FindFirstObjectByType<NumberManager>();
        if (numberSprites != null && numberSprites.Length > 0)
        {
            numberSpriteRenderer.sprite = numberSprites[numberValue];
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            numberValue = (numberValue + 1) % 10; // increment number and wrap around to 0 after 9
            numberSpriteRenderer.sprite = numberSprites[numberValue];
            numberManager.UpdateInputs(); // notify the NumberManager to update the current input values
        }
    }

    void Update()
    {
        // if answer is correct, make numbers green
        if (numberManager.isCorrect)
        {
            numberSpriteRenderer.color = Color.green;
        }
        else
        {
            numberSpriteRenderer.color = Color.white;
        }
    }
}
