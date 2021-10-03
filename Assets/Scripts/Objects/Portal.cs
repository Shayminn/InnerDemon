using UnityEngine;

public class Portal : MonoBehaviour {
    public Scenes nextLevel;
    public bool ending = false;
    public GameObject player2;

    public void ChangeLevel() {
        StoryProgression.storySeen = false;

        if (ending) {
            if (player2 == null) {
                SceneChanger.Instance.ChangeScene((int)Scenes.Alternate);
            }
            else {
                SceneChanger.Instance.ChangeScene((int)Scenes.Ending);
            }
        }

        SceneChanger.Instance.ChangeScene((int)nextLevel);
    }
}
