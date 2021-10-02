using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour {

    #region Serialized Fields

    /// <summary>
    /// Animator component for fade-in & fade-out
    /// </summary>
    [SerializeField, Tooltip("Animator component for fade-in & fade-out")] 
    Animator animator = null;

    /// <summary>
    /// Canvas scaler component for sizing of square
    /// </summary>
    [SerializeField, Tooltip("Canvas scaler component for sizing of square")] 
    CanvasScaler canvasScaler = null;

    /// <summary>
    /// First scene name to be loaded on play
    /// </summary>
    [SerializeField, Tooltip("First scene name to be loaded on play"), Space] 
    string firstSceneName = "";

    /// <summary>
    /// Delay to change scene
    /// </summary>
    [SerializeField, Tooltip("Delay to change scene")] 
    float changeDelay = 0.5f;

    /// <summary>
    /// Animator speed
    /// </summary>
    [SerializeField, Tooltip("Speed of the fade-in/fade-out")]
    float animationSpeed = 1;

    #endregion

    #region Singleton

    /// <summary>
    /// Instance to call methods without direct reference
    /// </summary>
    public static SceneChanger Instance;

    #endregion

    #region Private Fields

    /// <summary>
    /// Scene name to change to
    /// </summary>
    string sceneName = "";

    /// <summary>
    /// Scene index to change to
    /// </summary>
    int sceneIndex = 0;

    /// <summary>
    /// Checks if scene is changing (Prevents opening multiple scenes)
    /// </summary>
    static bool changingScenes = false;

    #endregion

    #region Monobehaviour methods

    /// <summary>
    /// Start Monobehaviour
    /// </summary>
    /// <returns></returns>
    IEnumerator Start() {
        yield return new WaitForEndOfFrame();

        // Change square size according to screen size
        canvasScaler.referenceResolution = new Vector2(Screen.width, Screen.height);
        transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = canvasScaler.referenceResolution;

        animator.speed = animationSpeed;

        SceneManager.LoadScene(firstSceneName);
        Instance = this;
    }

    #endregion

    #region Private methods

    /// <summary>
    /// Change scene with fade in & fade out animations
    /// </summary>
    /// <returns></returns>
    IEnumerator SceneChange() {
        changingScenes = true;

        yield return new WaitForSeconds(changeDelay);

        animator.Play("fadeIn");

        AsyncOperation asyncOperation = !sceneName.Equals("") ? SceneManager.LoadSceneAsync(sceneName) : SceneManager.LoadSceneAsync(sceneIndex);
        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone) {
            AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

            if (asyncOperation.progress >= 0.9f
                && state.length < state.normalizedTime) {
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }

        animator.Play("fadeOut");
        changingScenes = false;
    }

    #endregion

    #region Public methods

    /// <summary>
    /// Reload current scene
    /// </summary>
    public void ReloadScene() {
        if (!changingScenes) {
            this.sceneName = SceneManager.GetActiveScene().name;
            StartCoroutine(SceneChange());
        }
    }

    /// <summary>
    /// Change Scene with scene's name
    /// </summary>
    /// <param name="sceneName"></param>
    public void ChangeScene(string sceneName) {
        if (!changingScenes) {
            this.sceneName = sceneName;
            StartCoroutine(SceneChange());
        }
    }

    /// <summary>
    /// Change Scene with scene's build index
    /// </summary>
    /// <param name="sceneIndex"></param>
    public void ChangeScene(int sceneIndex) {
        if (!changingScenes) {
            this.sceneIndex = sceneIndex;
            StartCoroutine(SceneChange());
        }
    }

    #endregion
}
