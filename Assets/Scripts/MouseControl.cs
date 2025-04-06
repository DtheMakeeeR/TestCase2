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
    bool Focused
    {
        get => Cursor.lockState == CursorLockMode.Locked;
        set
        {
            Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = value == false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        Focused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Focused)
        { 
            UpdateCamera();

            UpdateVerticalPos();

            if (Input.GetKeyDown(KeyCode.LeftAlt))
                Focused = false;
        }
        else if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Focused = true;
        }
        

       // velocity = Vector3.Lerp(velocity, Vector3.zero, dampingCoefficient * Time.deltaTime);
        //transform.position += velocity * Time.deltaTime;
    }
    private void UpdateVerticalPos()
    {
       // Vector3 input = default;
        if (Input.GetKey(KeyCode.Space) && transform.position.y < 20) transform.position += Vector3.up * verticalSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftControl) && transform.position.y > -20) transform.position += Vector3.down * verticalSpeed * Time.deltaTime;
        //  Vector3 direction = transform.TransformVector(input.normalized);
        //  velocity += direction * verticalSpeed * Time.deltaTime;
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
    public void LookAtPoint(Transform pos)
    {
        transform.LookAt(pos.position);
    }
}
