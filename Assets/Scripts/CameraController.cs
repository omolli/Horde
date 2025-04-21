using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float speed;
    public float sqrThreshold;
    public float maxDistance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));
        mousePosition.z = player.position.z;

        Vector3 mouseOffset = mousePosition - player.position;
        Vector3 offsetDirection = mouseOffset.normalized;
        float offset = mouseOffset.sqrMagnitude;

        Vector3 cameraTarget = player.position;

        if (offset > sqrThreshold) {
            cameraTarget = player.position + offsetDirection * Mathf.Min(offset, maxDistance);
        }

        cameraTarget.z = -10;
        transform.position = Vector3.Lerp(transform.position, cameraTarget, speed * Time.deltaTime);
    }
}
