using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneTransition
{
    public const string QuestionSceneName = "You Do Question Area";

    private static string returnSceneName;
    private static Vector3 returnPosition;
    private static bool hasReturnPoint;

    public static void LoadQuestionScene(Transform playerTransform)
    {
        if (playerTransform == null)
        {
            return;
        }

        returnSceneName = SceneManager.GetActiveScene().name;
        returnPosition = playerTransform.position;
        hasReturnPoint = true;

        SceneManager.LoadScene(QuestionSceneName);
    }

    public static void ReturnToStoredScene()
    {
        if (!hasReturnPoint || string.IsNullOrWhiteSpace(returnSceneName))
        {
            return;
        }

        SceneManager.LoadScene(returnSceneName);
    }

    public static void RestoreReturnPoint(Transform playerTransform)
    {
        if (!hasReturnPoint || playerTransform == null)
        {
            return;
        }

        if (SceneManager.GetActiveScene().name != returnSceneName)
        {
            return;
        }

        playerTransform.position = returnPosition;
        hasReturnPoint = false;
    }
}