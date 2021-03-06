using UnityEngine;

public class PlayerCollision : MonoBehaviour {
    public Color color;
    public PlayerText playerText;

    public void OnCollisionEnter2D(Collision2D collision) {
        InteractableObject obj = collision.gameObject.GetComponent<InteractableObject>();
        if (obj != null) {
            if (!obj.needsFlip && obj.sprite.color != color) {
                obj.Touched(color);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        switch (collision.tag) {
            case nameof(Tags.Key):
                if (Vector3.Distance(transform.position, collision.transform.position) < 1.5f) {
                    AudioManager.Instance.PlaySFX(SFX.Key);
                    UISettings.Instance.GetKey();
                    Destroy(collision.gameObject);
                }
                break;

            case nameof(Tags.Button):
                AudioManager.Instance.PlaySFX(SFX.Button);
                playerText.WriteText("(I hear something move)", true);
                break;

            case nameof(Tags.ToggleButton):
                AudioManager.Instance.PlaySFX(SFX.Toggle);
                playerText.WriteText("(I hear something move)", true);
                break;

            case nameof(Tags.Door):
                if (GameControl.hasKey) {
                    AudioManager.Instance.PlaySFX(SFX.Unlock);
                    collision.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("OpenedDoor");
                    Destroy(collision.transform.GetChild(0).gameObject); // Destroys box collider
                    GameControl.hasKey = false;
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
