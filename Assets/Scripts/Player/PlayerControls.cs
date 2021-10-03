using UnityEngine;

public class PlayerControls : MonoBehaviour {
    public static KeyCode left = KeyCode.A;
    public static KeyCode right = KeyCode.D;
    public static KeyCode jump = KeyCode.Space;
    public static KeyCode _switch = KeyCode.W;
    public static KeyCode reset = KeyCode.R;

    public GameObject otherSelf;

    public PlayerText playerText;
    public GameObject spotLight;
    public Rigidbody2D rb2;

    public float speed;
    public float jumpStrength;
    public int sign;

    [Space]
    public LayerMask inAirLayerMask;
    public bool inAir = false;
    public float distance = 0.6f;

    [Space]
    public LayerMask checkBoxLayerMask;
    public bool hasBox;
    public InteractableObject lastInteractableObject;

    private FollowPlayer cam;


    // Start is called before the first frame update
    void Start() {
        cam = FollowPlayer.Instance;
    }

    // Update is called once per frame
    void Update() {

        if (!GameControl.storyTime) {
            // Check if in the air
            RaycastHit2D hitFloor = Physics2D.Raycast(transform.position, -Vector2.up, distance, inAirLayerMask);
            inAir = hitFloor.collider == null;

            RaycastHit2D hitBox = Physics2D.Raycast(transform.position, Vector2.up, distance, checkBoxLayerMask);
            hasBox = hitBox.collider != null;
            if (hasBox) {
                if (lastInteractableObject == null) {
                    lastInteractableObject = hitBox.collider.GetComponent<InteractableObject>();
                    lastInteractableObject.FollowPlayer(gameObject, hitBox.point);
                }
            }
            else {
                DisableLastInteractableObject();
            }

            if (Input.GetKey(left)) {
                Vector3 move = -Vector3.right * sign * speed * Time.deltaTime;
                if (!RaycastHorizontal(move))
                    transform.Translate(move);

                spotLight.transform.localEulerAngles = new Vector3(0, -90 * sign, 0);
            }

            if (Input.GetKey(right)) {
                Vector3 move = Vector3.right * sign * speed * Time.deltaTime;
                if (!RaycastHorizontal(move))
                    transform.Translate(move);

                spotLight.transform.localEulerAngles = new Vector3(0, 90 * sign, 0);
            }

            if (!inAir) {
                if (Input.GetKeyDown(jump)) {
                    rb2.AddForce(Vector3.up * sign * jumpStrength, ForceMode2D.Impulse);

                    AudioManager.Instance.PlaySFX(SFX.Jump);
                }
            }

            if (Input.GetKeyDown(_switch)) {
                AudioManager.Instance.PlaySFX(SFX.Switch);
                DisableLastInteractableObject();
                Flip();
            }

            if (Input.GetKeyDown(reset)) {
                SceneChanger.Instance.ReloadScene();
            }
        }
    }

    private bool RaycastHorizontal(Vector3 dir) {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 0.15f, inAirLayerMask);
        if (hit.collider != null) {
            return true;
        }

        return false;
    }

    private void Flip() {
        cam.Flip();
        cam.ChangeTarget(otherSelf);

        otherSelf.transform.position = transform.position;
        otherSelf.SetActive(true);
        gameObject.SetActive(false);

        playerText.Clear();
    }

    private void DisableLastInteractableObject() {
        if (lastInteractableObject != null) {
            lastInteractableObject.StopFollow();
            lastInteractableObject = null;
        }
    }
}
