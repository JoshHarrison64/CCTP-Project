using UnityEngine;

public class NumberManager : MonoBehaviour
{
    [Header("Combination")]
    [SerializeField] private int correctCombination = 00;
    [SerializeField] private int currentCombination = 00;

    [Header("")]
    [SerializeField] private GameObject[] numberInputs;
    public bool isCorrect;

    public void SetCorrectCombination(int value)
    {
        correctCombination = value;
        UpdateInputs();
    }

    void Start()
    {
        System.Array.Reverse(numberInputs);
        UpdateInputs(); // Initial input update to set the values
    }
    public void UpdateInputs()
    {
        currentCombination = 0;
        for (int i = 0; i < numberInputs.Length; i++)
        {
            NumberInput numberInput = numberInputs[i].GetComponent<NumberInput>();
            if (numberInput != null)
            {
                currentCombination += numberInput.numberValue * (int)Mathf.Pow(10, i);
                Debug.Log("NumberManager: Current combination updated to " + currentCombination);
            }
        }

        if (currentCombination == correctCombination)
        {
            Debug.Log("Correct combination entered!");
            isCorrect = true;
            
        }
        else
        {
            Debug.Log("Incorrect combination. Current: " + currentCombination + ", Expected: " + correctCombination);
            isCorrect = false;
        }
    }
}
