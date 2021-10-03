using System.Collections;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FollowPlayer : MonoBehaviour {
    public static FollowPlayer Instance;

    public Camera cam;
    public Transform currentTarget;

    public float camSpeed;
    public float zoomSpeed;
    public float rotateSpeed;
    public bool rotate = false;

    float zTarget;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        currentTarget = FindObjectOfType<PlayerControls>().transform;
    }

    private void Update() {
        //cam.transform.position = new Vector3(currrentTarget.transform.position.x, currrentTarget.transform.position.y, cam.transform.position.z);
        cam.transform.position = Vector3.MoveTowards(cam.transform.position, new Vector3(currentTarget.transform.position.x, currentTarget.transform.position.y, cam.transform.position.z), Time.deltaTime * camSpeed);

        if (rotate) {
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, new Vector3(0, 0, zTarget), rotateSpeed * Time.deltaTime);

            if (Mathf.Abs(zTarget - transform.localEulerAngles.z) < 0.1f) {
                transform.eulerAngles = new Vector3(0, 0, zTarget);
                rotate = false;
            }
        }
    }

    public void Zoom(bool closer) {
        if (closer) {
            StartCoroutine(ZoomIn());
        }
        else {
            StartCoroutine(ZoomOut());
        }
    }

    public IEnumerator ZoomIn() {
        while (cam.orthographicSize > 5) {
            cam.orthographicSize -= Time.deltaTime * zoomSpeed;
            yield return null;
        }
        cam.orthographicSize = 5;
    }

    public IEnumerator ZoomOut() {
        Debug.Log(cam.orthographicSize);
        while (cam.orthographicSize < 10) {
            cam.orthographicSize += Time.deltaTime * zoomSpeed;
            yield return null;
        }
        cam.orthographicSize = 10;
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
        currentTarget = newTarget.transform;
    }
}
