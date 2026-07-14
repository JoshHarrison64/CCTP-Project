using UnityEngine;

public class AdditionManager : QuestionManager
{
    protected override void GenerateQuestion(DifficultyLevel difficulty)
    {
        GenerateAdditionQuestion(difficulty);
    }
}
