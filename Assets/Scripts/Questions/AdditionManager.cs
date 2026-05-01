using UnityEngine;

public class AdditionManager : QuestionManager
{
    public void GenerateQuestion(DifficultyLevel difficulty)
    {
        switch (difficulty)
        {
            case DifficultyLevel.Level1:
                GenerateLevel1Question();
                break;
            case DifficultyLevel.Level2:
                GenerateLevel2Question();
                break;
            case DifficultyLevel.Level3:
                // GenerateAddition(DifficultyLevel.Level3);
                break;
            case DifficultyLevel.Level4:
                // GenerateAddition(DifficultyLevel.Level4);
                break;
            default:
                GenerateLevel1Question();
                Debug.LogWarning("Invalid difficulty for addition. Defaulting to Level 1.");
                break;
        }
    }

    private void GenerateLevel1Question()
    {
        // level 1 addition: numbers add to 20 or less
        correctAnswer = Random.Range(1, 21);
        number1 = Random.Range(1, correctAnswer);
        number2 = correctAnswer - number1;
    }

    private void GenerateLevel2Question()
    {
        // level 2 addition: numbers add to 100 or less
        correctAnswer = Random.Range(1, 101);
        number1 = Random.Range(1, correctAnswer);
        number2 = correctAnswer - number1;
        GenerateLevel2Workings(number1, number2, correctAnswer);
    }

    private void GenerateLevel2Workings(int n1, int n2, int ans)
    {
        // get 'ones'
        // "I start by adding my ones"
        int n1_ones = n1 % 10;
        int n2_ones = n2 % 10;
        int ones_added = n1_ones + n2_ones;

        // get 'tens'
        // "Then I add my tens"
        int n1_tens = n1 / 10 * 10;
        int n2_tens = n2 / 10 * 10;
        int tens_added = n1_tens + n2_tens;

        // add two parts together
        // "Finally I add my tens and ones together to get the answer"
        int total_added = tens_added * 10 + ones_added;
    }
}
