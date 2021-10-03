using System.Collections.Generic;
using UnityEngine;

public class ButtonObject : MonoBehaviour {
    public bool toggle = false;
    public Sprite on;
    public Sprite off;
    SpriteRenderer sprite;

    [Space]
    public bool triggered = false;
    public float moveSpeed = 5;

    public List<ObjectsToMoveList> objsToMove;

    private void Start() {
        InitializeFromPositions();

        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = off;
    }

    private void Update() {
        if (triggered) {
            foreach (ObjectsToMoveList objs in objsToMove) {
                if (objs.objToMove.position != objs.toPosition) {
                    objs.objToMove.position = Vector3.MoveTowards(objs.objToMove.position, objs.toPosition, Time.deltaTime * moveSpeed);
                }
            }
        }
        else {
            foreach (ObjectsToMoveList objs in objsToMove) {
                if (objs.objToMove.position != objs.FromPosition) {
                    objs.objToMove.position = Vector3.MoveTowards(objs.objToMove.position, objs.FromPosition, Time.deltaTime * moveSpeed);
                }
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        if (toggle) {
            if (collision.CompareTag(Tags.Player.ToString())) {
                triggered = !triggered;

                sprite.sprite = triggered
                              ? on
                              : off;
            }
        }
        else {
            triggered = true;

            sprite.sprite = on;
        }
    }

    public void OnTriggerStay2D(Collider2D collision) {
        if (!toggle)
            if (!triggered)
                triggered = true;
    }

    public void OnTriggerExit2D(Collider2D collision) {
        if (!toggle) {
            triggered = false;

            sprite.sprite = off;
        }
    }

    public void InitializeFromPositions() {
        foreach (ObjectsToMoveList objs in objsToMove) {
            objs.FromPosition = objs.objToMove.position;
        }
    }
}

[System.Serializable]
public class ObjectsToMoveList {
    public Transform objToMove;
    public Vector3 FromPosition { get; set; }
    public Vector3 toPosition;
}