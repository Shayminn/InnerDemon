using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Scenes nextLevel;

    public void ChangeLevel() {
        if (nextLevel == Scenes.None) {
            // Proceed to ending
        }
        else {
            SceneChanger.Instance.ChangeScene((int)nextLevel);
        }
    }
}