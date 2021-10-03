using UnityEngine;

public class MovingPlatformScript : MonoBehaviour
{
    public GameObject player;
    public Transform pos1, pos2;
    public float speed, distance;

    public Transform startPos;
    private Transform lastPos;
    Vector3 nextPos;

    void Start()
    {
        nextPos = startPos.position;
    }

    void Update()
    {
        if (transform.position == pos1.position)
        {
            nextPos = pos2.position;
        }
        if (transform.position == pos2.position)
        {
            nextPos = pos1.position;
        }
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);

        lastPos = transform;
        distance = Mathf.Abs(lastPos.position.x - transform.position.x);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            Vector3 targetPos = player.transform.position;
            targetPos.x += distance;
            player.transform.position = targetPos;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}
