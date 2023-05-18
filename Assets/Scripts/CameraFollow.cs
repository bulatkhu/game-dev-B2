using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followSpeed = 2f;
    public Transform target;

    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x, transform.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed);
    }
}