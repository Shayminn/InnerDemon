using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public Color color;
    public PlayerText playerText;

    public void OnCollisionEnter2D(Collision2D collision) {
        InteractableObject obj = collision.gameObject.GetComponent<InteractableObject>();
        if (obj != null) {
            if (!obj.needsFlip) {
                obj.Touched(color);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        switch (collision.tag) {
            case nameof(Tags.Key):
                UISettings.Instance.GetKey();
                Destroy(collision.gameObject);
                break;

            case nameof(Tags.Spike):
                // death, switch to other character
                break;

            case nameof(Tags.Button):
            case nameof(Tags.ToggleButton):
                playerText.WriteText("(I hear something move)", true);
                break;

            case nameof(Tags.Door):
                if (GameControl.hasKey) {
                    GameControl.hasKey = false;
                    collision.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("OpenedDoor");
                    Destroy(collision.transform.GetChild(0).gameObject); // Destroys box collider
                }
                else {
                    playerText.WriteText("I need to find the key.", true);
                }
                break;

            case nameof(Tags.Portal):
                collision.GetComponent<Portal>().ChangeLevel();
                break;
        }
        if (collision.CompareTag(Tags.Key.ToString())) {
        }

    }
}
