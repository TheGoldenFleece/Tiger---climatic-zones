using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEditor;
using UnityEngine;

public class SceneEditorLoader : MonoBehaviour
{
    private static string currentScenePath = "";

    //Load game from begin
    [MenuItem("Game Mode/Run from the start scene")]
    public static void StartGame() {
        // Задайте ім'я сцени, яку ви хочете завантажити
        string sceneName = GameScene.Menu.ToString();

        // Зберегти поточну сцену, щоб повернутися до неї пізніше
        currentScenePath = EditorSceneManager.GetActiveScene().path;

        // Відкрийте сцену за ім'ям
        EditorSceneManager.OpenScene(@"Assets/Tiger - climatic zones/Scenes/" + sceneName + ".unity");

        // Переключіться в режим гри
        EditorApplication.isPlaying = true;

        // Відновіть попередню сцену, коли гра закінчиться
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state) {
        if (state == PlayModeStateChange.ExitingPlayMode) {
            // Викличте метод для відновлення попередньої сцени
            RestorePreviousScene();
        }
    }

    private static void RestorePreviousScene() {
        // Зачекайте, поки гра закінчиться (можливо, вам знадобиться змінити цей час в залежності від вашого проекту)
        EditorApplication.update += WaitForGameToExit;
    }

    private static void WaitForGameToExit() {
        if (!EditorApplication.isPlaying) {
            // Відновіть попередню сцену за шляхом
            if (!string.IsNullOrEmpty(currentScenePath)) {
                EditorSceneManager.OpenScene(currentScenePath);
            }

            // Видаліть обробник подій, щоб уникнути повторного виклику
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;

            // Видаліть обробник подій для слідкування за зміною режиму гри
            EditorApplication.update -= WaitForGameToExit;
        }
    }
}
