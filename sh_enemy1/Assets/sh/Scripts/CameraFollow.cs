using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 p = player.position;
            p.z = transform.position.z;
            transform.position = p;
        }
    }

}
