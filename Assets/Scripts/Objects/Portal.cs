using UnityEngine;

public class Portal : MonoBehaviour {
    public Scenes nextLevel;

    public void ChangeLevel() {
        StoryProgression.storySeen = false;

        if (nextLevel.Equals(Scenes.Ending)) {
            GameControl.ending = true;
        }

        SceneChanger.Instance.ChangeScene((int)nextLevel);
    }
}
