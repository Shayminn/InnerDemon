using UnityEngine;

public class Tutorial : MonoBehaviour {
    public bool controlTutorial = true;

    public string move1;
    public string move2;
    public string tutorial;

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(Tags.Player.ToString())) {
            string txt = "";
            if (controlTutorial) {
                txt = "(" + StringToKeyCode(move1);

                if (!string.IsNullOrEmpty(move2))
                    txt += "/" + StringToKeyCode(move2) + " " + tutorial + ")";
                else
                    txt += " " + tutorial + ")";
            }
            else {
                txt = tutorial;
            }
            collision.GetComponent<PlayerText>().WriteText(txt, true);
        }
    }

    KeyCode StringToKeyCode(string str) {
        switch (str) {
            case "left":
                return PlayerControls.left;

            case "right":
                return PlayerControls.right;

            case "jump":
                return PlayerControls.jump;

            case "switch":
                return PlayerControls._switch;

            case "reset":
                return PlayerControls.reset;
        }
        return KeyCode.None;
    }
}
