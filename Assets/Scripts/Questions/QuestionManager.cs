using Unity.Multiplayer.Center.Common;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    private AdditionManager additionManager;
    private SubtractionManager subtractionManager;
    private MultiplicationManager multiplicationManager;
    private DivisionManager divisionManager;

    public enum OperationType { Addition, Subtraction, Multiplication, Division }
    public enum DifficultyLevel { Level1, Level2, Level3, Level4, Level5, Level6 }
    [Header("Question Data")]
    public int number1;
    public int number2;
    public int correctAnswer;
    [Header("Question Type")]
    public OperationType currentOperation;
    public DifficultyLevel currentDifficulty;
    
    [Header("Question Sprites")]
    [SerializeField] private Sprite[] numberSprites;
    public SpriteRenderer[] questionBoxSprites;

    

    // public void GenerateAddition(DifficultyLevel difficulty)
    // {
    //     switch (difficulty)
    //     {
    //         case DifficultyLevel.Level1:
    //             // level 1 addition: numbers add to 20 or less
    //             correctAnswer = Random.Range(1, 21);
    //             number1 = Random.Range(1, correctAnswer);
    //             number2 = correctAnswer - number1;
    //             break;
    //         case DifficultyLevel.Level2:
    //             // level 2 addition: numbers add to 100 or less
    //             correctAnswer = Random.Range(1, 101);
    //             number1 = Random.Range(1, correctAnswer);
    //             number2 = correctAnswer - number1;
    //             break;
    //         case DifficultyLevel.Level3:
    //             // level 3 addition: 2 digit + 2 digit, no carrying numbers
    //             int number1_tens = Random.Range(0, 10);
    //             int number1_ones = Random.Range(0, 10);
    //             if (number1_tens == 0) 
    //             {
    //                 number1_ones = Random.Range(1, 10); // ensure final number isn't 0
    //             }
    //             int number2_tens = Random.Range(0, 10 - number1_tens); // ensure no carrying
    //             int number2_ones = Random.Range(0, 10 - number1_ones); // ensure no carrying
    //             if (number2_tens == 0 && number2_ones == 0) 
    //             {
    //                 number2_ones = Random.Range(1, 10 - number1_ones); // ensure final number isn't 0
    //             }
    //             // put the numbers together
    //             number1 = number1_tens * 10 + number1_ones;
    //             number2 = number2_tens * 10 + number2_ones;
    //             correctAnswer = number1 + number2;
    //             break;
    //         case DifficultyLevel.Level4:
    //             // level 6 addition: 2 digit + 2 digit, carrying numbers
    //             number1 = Random.Range(1, 100);
    //             number2 = Random.Range(1, 100);
    //             correctAnswer = number1 + number2;
    //             break;
    //     }
    //     Debug.Log($"Generated Addition Question: {number1} + {number2} = {correctAnswer}");
    // }

    // public void GenerateSubtraction(DifficultyLevel difficulty)
    // {
    //     switch (difficulty)
    //     {
    //         case DifficultyLevel.Level1:
    //             // level 1 subtraction: numbers subtract to 20 or less
    //             number1 = Random.Range(1, 21);
    //             number2 = Random.Range(1, number1); // ensure non-negative result
    //             correctAnswer = number1 - number2;
    //             break;
    //         case DifficultyLevel.Level2:
    //             // level 2 subtraction: numbers subtract to 100 or less
    //             number1 = Random.Range(1, 101);
    //             number2 = Random.Range(1, number1); // ensure non-negative result
    //             correctAnswer = number1 - number2;
    //             break;
    //         case DifficultyLevel.Level3:
    //             // level 3 subtraction: 2 digit - 2 digit, no borrowing numbers
    //             int number1_tens = Random.Range(0, 10);
    //             int number1_ones = Random.Range(0, 10);
    //             if (number1_tens == 0) 
    //             {
    //                 number1_ones = Random.Range(1, 10); // ensure final number isn't 0
    //             }
    //             int number2_tens = Random.Range(0, number1_tens + 1); // ensure no borrowing
    //             int number2_ones = Random.Range(0, number1_ones + 1); // ensure no borrowing
    //             if (number2_tens == 0 && number2_ones == 0) 
    //             {
    //                 number2_ones = Random.Range(1, number1_ones + 1); // ensure final number isn't 0
    //             }
    //             // put the numbers together
    //             number1 = number1_tens * 10 + number1_ones;
    //             number2 = number2_tens * 10 + number2_ones;
    //             correctAnswer = number1 - number2;
    //             break;
    //         case DifficultyLevel.Level4:
    //             // level 6 subtraction: 2 digit - 2 digit, borrowing numbers
    //             number1 = Random.Range(1, 100);
    //             number2 = Random.Range(1, number1); // ensure non-negative result
    //             correctAnswer = number1 - number2;
    //             break;
    //     }
    // }

    // public void GenerateMultiplication(DifficultyLevel difficulty)
    // {
    //     switch (difficulty)
    //     {
    //         case DifficultyLevel.Level1:
    //             // level 1 multiplication: 2, 5, 10 times tables
    //             number1 = new int[] { 2, 5, 10 }[Random.Range(0, 3)];
    //             number2 = Random.Range(1, 13);
    //             correctAnswer = number1 * number2;
    //             break;
    //         case DifficultyLevel.Level2:
    //             // level 2 multiplication: 1-12 times tables
    //             number1 = Random.Range(1, 13);
    //             number2 = Random.Range(1, 13);
    //             correctAnswer = number1 * number2;
    //             break;
    //         case DifficultyLevel.Level3:
    //             // level 3 multiplication: 2 digit x 1 digit
    //             number1 = Random.Range(10, 100);
    //             number2 = Random.Range(1, 10);
    //             correctAnswer = number1 * number2;

    //             break;
    //         case DifficultyLevel.Level4:
    //             // level 4 multiplication: 2 digit x 2 digit
    //             number1 = Random.Range(10, 100);
    //             number2 = Random.Range(10, 100);
    //             correctAnswer = number1 * number2;
    //             break;
    //     }
    // }

    // public void GenerateDivision(DifficultyLevel difficulty)
    // {
    //     switch (difficulty)
    //     {
    //         case DifficultyLevel.Level1:
    //             // level 1 division: 2, 5, 10 times tables
    //             number1 = new int[] { 2, 5, 10 }[Random.Range(0, 3)];
    //             number2 = Random.Range(1, 13);
    //             correctAnswer = number1 / number2;
    //             break;
    //         case DifficultyLevel.Level2:
    //             // level 2 division: 1-12 times tables
    //             number1 = Random.Range(1, 13);
    //             number2 = Random.Range(1, 13);
    //             correctAnswer = number1 / number2;
    //             break;
    //         case DifficultyLevel.Level3:
    //             // level 3 division: 3 digit ÷ 1 digit, no remainders
    //             break;
    //         case DifficultyLevel.Level4:
    //             // level 4 division: 3 digit ÷ 1 digit, with remainders
    //             break;
    //     }
    //     Debug.Log($"Generated Division Question: {number1} / {number2} = {correctAnswer}");
    // }

    private void Start() {
        additionManager = FindFirstObjectByType<AdditionManager>();
        subtractionManager = FindFirstObjectByType<SubtractionManager>();
        multiplicationManager = FindFirstObjectByType<MultiplicationManager>();
        divisionManager = FindFirstObjectByType<DivisionManager>();
        switch (currentOperation)
        {
            case OperationType.Addition:
                additionManager.GenerateQuestion(currentDifficulty);
                break;
            case OperationType.Subtraction:
                // subtractionManager.GenerateQuestion(currentDifficulty);
                break;
            case OperationType.Multiplication:
                // multiplicationManager.GenerateQuestion(currentDifficulty);
                break;
            case OperationType.Division:
                // divisionManager.GenerateQuestion(currentDifficulty);
                break;
        }
    }

    private void UpdateBoxSprites()
    {
        // update the question box sprites to match the numbers in the question
        questionBoxSprites[0].sprite = numberSprites[number1];
        questionBoxSprites[1].sprite = numberSprites[number2];
    }
}
