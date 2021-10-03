using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

// Comment out the line below to stop the editor from starting in DontDestroyOnLoad scene
//[InitializeOnLoad]
public static class StartingSceneOnPlay {
    private static string oldScene;
    private const string FIRST_SCENE_NAME = "DontDestroyOnLoad";

    static StartingSceneOnPlay() {
        EditorApplication.playModeStateChanged += StateChange;
    }

    static void StateChange(PlayModeStateChange state) {
        if (EditorApplication.isPlaying) {
            EditorApplication.playModeStateChanged -= StateChange;

            //Load First scene only if it's not the first scene
            if (SceneManager.GetActiveScene().name != FIRST_SCENE_NAME) {
                if (!EditorApplication.isPlayingOrWillChangePlaymode) {
                    //We're in playmode, just about to change playmode to Editor
                    EditorSceneManager.OpenScene(oldScene);
                }
                else {
                    //We're in playmode, right after having pressed Play
                    oldScene = SceneManager.GetActiveScene().name;
                    SceneManager.LoadScene(FIRST_SCENE_NAME);

                    // Clear logs/errors from Start/Awake that is logged before the first scene.
                    ClearLog();

                    if (SceneManager.GetSceneByBuildIndex(0).name != FIRST_SCENE_NAME) {
                        Debug.LogWarning("Make sure your starting scene is the first one on the list in the build settings.");
                    }
                }
            }
        }
    }

    /// <summary>
    /// Clears the Console Log
    /// </summary>
    static void ClearLog() {
        var logEntries = System.Type.GetType("UnityEditor.LogEntries, UnityEditor.dll");
        var method = logEntries.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
}