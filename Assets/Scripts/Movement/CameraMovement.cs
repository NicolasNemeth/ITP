using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float panSpeed = 50f;
    [SerializeField] private float panBorderThickness = 10f;
    [SerializeField] private Vector2 panLimit = new Vector2(100f, 100f);
    [SerializeField] private float scrollSpeed = 10000f;
    [SerializeField] private float rotationSpeed = 75f;
    [SerializeField] private float minY = 10f;
    [SerializeField] private float maxY = 60f;

    private void Update()
    {
        Vector3 position = transform.position;

        //Move forward
        if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            position += Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized * panSpeed * Time.deltaTime;
        }

        //Move backwards
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBorderThickness)
        {
            position -= Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized * panSpeed * Time.deltaTime;
        }

        //Move right
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            position += transform.right * panSpeed * Time.deltaTime;
        }

        //Move left
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBorderThickness)
        {
            position -= transform.right * panSpeed * Time.deltaTime;
        }

        //Rotate left
        if (Input.GetKey(KeyCode.Q))
        {
            transform.RotateAround(transform.position, Vector3.up, -rotationSpeed * Time.deltaTime);
        }

        //Rotate right
        if (Input.GetKey(KeyCode.E))
        {
            transform.RotateAround(transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }

        //Move up
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        position.y -= scroll * scrollSpeed * Time.deltaTime;

        position.x = Mathf.Clamp(position.x, -panLimit.x, panLimit.x);
        position.y = Mathf.Clamp(position.y, minY, maxY);
        position.z = Mathf.Clamp(position.z, -panLimit.y, panLimit.y);

        transform.position = position;
    }
}
