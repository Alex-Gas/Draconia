using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameObject playerObj;
    private Transform playerTransform;
    public float smoothTime = 0.1f;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        playerObj = GameObject.Find("Player");
        playerTransform = playerObj.transform;
    }
    private void LateUpdate()
    {
        Vector3 targetPosition = playerTransform.position;
        targetPosition.z = transform.position.z;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
