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
        // ������� ��'� �����, ��� �� ������ �����������
        string sceneName = GameScene.Menu.ToString();

        // �������� ������� �����, ��� ����������� �� �� �����
        currentScenePath = EditorSceneManager.GetActiveScene().path;

        // ³������� ����� �� ��'��
        EditorSceneManager.OpenScene(@"Assets/Tiger - climatic zones/Scenes/" + sceneName + ".unity");

        // ������������� � ����� ���
        EditorApplication.isPlaying = true;

        // ³������ ��������� �����, ���� ��� ����������
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state) {
        if (state == PlayModeStateChange.ExitingPlayMode) {
            // �������� ����� ��� ���������� ���������� �����
            RestorePreviousScene();
        }
    }

    private static void RestorePreviousScene() {
        // ���������, ���� ��� ���������� (�������, ��� ����������� ������ ��� ��� � ��������� �� ������ �������)
        EditorApplication.update += WaitForGameToExit;
    }

    private static void WaitForGameToExit() {
        if (!EditorApplication.isPlaying) {
            // ³������ ��������� ����� �� ������
            if (!string.IsNullOrEmpty(currentScenePath)) {
                EditorSceneManager.OpenScene(currentScenePath);
            }

            // ������� �������� ����, ��� �������� ���������� �������
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;

            // ������� �������� ���� ��� ���������� �� ����� ������ ���
            EditorApplication.update -= WaitForGameToExit;
        }
    }
}
