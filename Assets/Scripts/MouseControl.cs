using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    public float camSens = 0.5f;
    public float verticalSpeed = 10f;
    public float dampingCoefficient = 5f;
    private bool zoomed = false;
    private Vector3 velocity;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCamera();

        UpdateVerticalPos();

        velocity = Vector3.Lerp(velocity, Vector3.zero, dampingCoefficient * Time.deltaTime);
        transform.position += velocity * Time.deltaTime;
    }

    private void UpdateVerticalPos()
    {
        Vector3 input = default;
        if (Input.GetKey(KeyCode.Space) && transform.position.y < 5) input += Vector3.up;
        if (Input.GetKey(KeyCode.LeftControl) && transform.position.y > 1) input += Vector3.down;
        Vector3 direction = transform.TransformVector(input.normalized);
        velocity += direction * verticalSpeed * Time.deltaTime;
    }

    private void UpdateCamera()
    {
        Vector2 mouseDelta = camSens * new Vector2(Input.GetAxis("Mouse X"), -Input.GetAxis("Mouse Y"));
        Quaternion rotation = transform.rotation;
        Quaternion horiz = Quaternion.AngleAxis(mouseDelta.x, Vector3.up);
        Quaternion vert = Quaternion.AngleAxis(mouseDelta.y, Vector3.right);
        transform.rotation = horiz * rotation * vert;
        if (Input.GetMouseButtonDown(1))
        {
            if (zoomed)
            {
                cam.fieldOfView = 60;
                zoomed = false;
            }
            else
            {
                cam.fieldOfView = 30;
                zoomed = true;
            }
        }
    }
}
