using UnityEngine;
using UnityEngine.AI;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform targetTransform;

    [SerializeField]
    private Camera mainCamera;

    private float movementSmoothSpeed = 0.125f,
        zoomSmoothSpeed = 0.125f,
        zoomOffset = 16f,
        minZoom = 10f,
        maxZoom = 100f;

    private Vector3 cameraOffset,
        previousMousePosition;

    void Start()
    {
        cameraOffset = transform.position - targetTransform.position;
    }

    // LateUpdate is run after Update once per frame
    private void LateUpdate()
    {
        // rotate camera around player
        if (Input.GetMouseButtonDown(1))
        {
            previousMousePosition = mainCamera.ScreenToViewportPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(1))
        {
            Vector3 direction = previousMousePosition - mainCamera.ScreenToViewportPoint(Input.mousePosition);
            Quaternion cameraTurnAngle = Quaternion.AngleAxis(-direction.x * 10, Vector3.up);
            cameraOffset = cameraTurnAngle * cameraOffset;
        }

        // zoom in and out
        if (Input.mouseScrollDelta.y != 0)
        {
            float currentFOV = mainCamera.fieldOfView;
            float targetFOV = Mathf.Clamp(currentFOV - Input.mouseScrollDelta.y * zoomOffset, minZoom, maxZoom);
            mainCamera.fieldOfView = Mathf.Lerp(currentFOV, targetFOV, zoomSmoothSpeed);
        }

        // update camera position to follow player
        transform.position = Vector3.Lerp(transform.position, targetTransform.position + cameraOffset, movementSmoothSpeed);
        transform.LookAt(targetTransform);
    }

}
