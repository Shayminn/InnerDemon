using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    PlayerControls playerControls;
    GameObject otherSelf;

    public void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(Tags.Player.ToString())) {
            playerControls = collision.gameObject.GetComponent<PlayerControls>();
            otherSelf = playerControls.otherSelf;

            if (otherSelf != null) {
                StartCoroutine(Flip());
            }
            else {
                Gameover.Instance.DisplayGameOver(false);
            }

            AudioManager.Instance.PlaySFX(SFX.Death);
            Destroy(collision.gameObject);
        }
    }

    IEnumerator Flip() {
        BoxCollider2D boxCollider2D = otherSelf.GetComponent<BoxCollider2D>();

        boxCollider2D.enabled = false;
        playerControls.Flip();

        yield return new WaitForSeconds(0.5f);
        boxCollider2D.enabled = true;
    }
}
