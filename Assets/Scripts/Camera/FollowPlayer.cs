using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FollowPlayer : MonoBehaviour {
    public static FollowPlayer Instance;

    public Camera cam;
    public Transform currrentTarget;

    public float rotateSpeed;
    public bool rotate = false;

    float zTarget;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        currrentTarget = FindObjectOfType<PlayerControls>().transform;
    }

    private void Update() {
        cam.transform.position = new Vector3(currrentTarget.transform.position.x, currrentTarget.transform.position.y, cam.transform.position.z);

        if (rotate) {
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, 0, zTarget), rotateSpeed * Time.deltaTime);

            if (Mathf.Abs(zTarget - transform.localEulerAngles.z) < 0.1f) {
                transform.eulerAngles = new Vector3(0, 0, zTarget);
                rotate = false;
            }
        }
    }

    public void Flip() {
        rotate = true;

        GameControl.flipped = !GameControl.flipped;

        if (GameControl.flipped) {
            zTarget = 180;
        }
        else {
            zTarget = 0;
        }

        FlipAllInteractableObjects();
    }

    public void FlipAllInteractableObjects() {
        foreach(InteractableObject obj in FindObjectsOfType<InteractableObject>().Where(x => x.needsFlip).ToList()) {
            obj.FlipGravity();
        }
    }

    public void ChangeTarget(GameObject newTarget) {
        currrentTarget = newTarget.transform;
    }
}
