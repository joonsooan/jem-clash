using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Transform cameraTransform;
    public float parallaxFactor;

    private Vector3 _lastCameraPosition;

    private void Start()
    {
        if (cameraTransform == null && Camera.main != null)
            cameraTransform = Camera.main.transform;

        _lastCameraPosition = cameraTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - _lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxFactor, 0, 0);

        _lastCameraPosition = cameraTransform.position;
    }
}