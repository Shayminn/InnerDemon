using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public Rigidbody2D rb2;
    public SpriteRenderer sprite;

    public bool needsFlip;
    public bool followPlayer;
    public Transform targetPlayer;
    float currentDistanceFromPlayer;

    private void Update() {
        if (followPlayer) {
            transform.position = new Vector3(targetPlayer.transform.position.x - currentDistanceFromPlayer, transform.position.y, transform.position.z);
        }
    }

    public void Touched(Color color) {
        sprite.color = color;
        needsFlip = true;
    }

    public void FlipGravity() {
        rb2.gravityScale = -rb2.gravityScale;

        needsFlip = false;
    }

    public void FollowPlayer(GameObject targetPlayer, Vector2 hitPoint) {
        followPlayer = true;
        currentDistanceFromPlayer = targetPlayer.transform.position.x - transform.position.x;
        this.targetPlayer = targetPlayer.transform;

        rb2.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
    }

    IEnumerator AddConstraint() {
        yield return new WaitForSeconds(0.05f);
        rb2.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
    }

    public void StopFollow() {
        followPlayer = false;
        rb2.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
