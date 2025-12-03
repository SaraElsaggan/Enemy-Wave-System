using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 10f;  
    public float mouseSensitivity = 200f;

    public float minX = -10f;
    public float maxX = 10f;
    public float minZ = -10f;
    public float maxZ = 10f;

    void Update()
    {
        MoveCamera();
        RotateCamera();
        ClampPosition();
    }

    void MoveCamera()
    {
        float horizontal = Input.GetAxis("Horizontal");  // A / D
        float vertical = Input.GetAxis("Vertical");      // W / S

        Vector3 direction = (transform.forward * vertical) + (transform.right * horizontal);
        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    void RotateCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        // حركة يمين وشمال بس
        transform.Rotate(0f, mouseX, 0f);
    }

    void ClampPosition()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);
        transform.position = pos;
    }
}
