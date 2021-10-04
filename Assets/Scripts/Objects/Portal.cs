using UnityEngine;

public class Portal : MonoBehaviour {
    public Scenes nextLevel;
    public bool ending = false;
    public GameObject player2;

    public void ChangeLevel() {
        StoryProgression.storySeen = false;

        if (ending) {
            if (player2 == null) {
                SceneChanger.Instance.ChangeScene(Scenes.Alternate.ToString());
            }
            else {
                SceneChanger.Instance.ChangeScene(Scenes.Ending.ToString());
            }
        }
        else {
            SceneChanger.Instance.ChangeScene(nextLevel.ToString());
        }
    }
}
