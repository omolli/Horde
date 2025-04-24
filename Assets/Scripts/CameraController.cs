using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    [SerializeField] float speed;
    [SerializeField] float sqrThreshold;
    [SerializeField] float maxDistance;
    [SerializeField] float z = -10;

    void Update()
    {

        //Vector3 mousePosition = Input.mousePosition;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);//Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));
        mousePosition.z = player.position.z;

        Vector3 mouseOffset = mousePosition - player.position;
        Vector3 offsetDirection = mouseOffset.normalized;
        float offset = mouseOffset.sqrMagnitude;

        Vector3 cameraTarget = player.position;

        if (offset > sqrThreshold) {
            cameraTarget = player.position + offsetDirection * Mathf.Min(offset, maxDistance);
        }

        cameraTarget.z = z;
        transform.position = Vector3.Lerp(transform.position, cameraTarget, speed * Time.deltaTime);
    }
}