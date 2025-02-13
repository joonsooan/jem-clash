using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float scrollSpeed = 5f;
    public float minX = -10f;
    public float maxX = 10f;

    private void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {
            Vector3 newPosition = transform.position + new Vector3(scroll * scrollSpeed, 0, 0);
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            transform.position = newPosition;
        }
    }
}