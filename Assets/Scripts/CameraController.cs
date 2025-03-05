using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Restraints")] public float scrollSpeed = 5f;
    public float minX = -10f;
    public float maxX = 10f;

    [Header("Camera Movement Value")] public float xAmount;

    private void Update()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {
            var newPosition = transform.position + new Vector3(scroll * scrollSpeed, 0, 0);
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            transform.position = newPosition;
        }
    }

    public void CameraShiftToRight()
    {
        transform.position += new Vector3(xAmount, 0, 0);
    }
}