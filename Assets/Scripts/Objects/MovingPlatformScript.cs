using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    public Transform pos1, pos2;
    public float speed, distance;

    public Transform startPos;
    private Vector3 lastPos;

    bool toStart = true;

    void FixedUpdate()
    {
        if (transform.position == pos1.position) {
            toStart = false;
        }
        else if (transform.position == pos2.position) {
            toStart = true;
        }

        if (toStart)
            transform.position = Vector3.MoveTowards(transform.position, pos1.position, speed * Time.fixedDeltaTime);
        else
            transform.position = Vector3.MoveTowards(transform.position, pos2.position, speed * Time.fixedDeltaTime);


        distance = lastPos.x - transform.position.x;
        lastPos = transform.position;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        switch (collision.gameObject.tag) {
            case nameof(Tags.Player):
            case nameof(Tags.Box):
                Vector3 targetPos = collision.transform.position;
                targetPos.x -= distance;
                collision.transform.position = targetPos;
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
