using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiedPersonCamera : MonoBehaviour
{
    [Header("Ÿ�� ����")]
    public Transform target;

    [Header("ī�޶� �Ÿ� ����")]
    public float distance = 8.0f;
    public float height = 5.0f;

    [Header("���콺 ����")]
    public float mouseSensivitiy = 2.0f;
    public float minVecticalAngle = -30.0f;
    public float maxVectcalAngle = 60.0f;

    [Header("�ε巯�� ����")]
    public float positionSmoothTime = 0.2f;
    public float rotationSmoothTime = 0.1f;

    private float horizontalAngle = 0.0f;
    private float verticalAngle = 0.0f;

    private Vector3 currenVelocity;
    private Vector3 currentPosition;
    private Quaternion currenRotation;

    // Start is called before the first frame update
    void Start()
    {
       if(target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
                target = player.transform;
        }

       currentPosition = transform.position;
        currenRotation = transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { ToggleCursor(); } 
    }

    void LateUpdate()
    {
            if (target == null) return;
            HandleMouseInput();
            UpdateCameraSmooth();
    }
    void HandleMouseInput()
    {
        if (Cursor.lockState != CursorLockMode.Locked) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensivitiy;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivitiy;

        horizontalAngle += mouseX;
        verticalAngle -= mouseY;

        verticalAngle = Mathf.Clamp(verticalAngle, minVecticalAngle, maxVectcalAngle);
    }

    void UpdateCameraSmooth()
    {
        Quaternion rotation = Quaternion.Euler(verticalAngle, horizontalAngle, 0);
        Vector3 rotareOffset = rotation * new Vector3(0, height, -distance);
        Vector3 targetPosition = target.position + rotareOffset;

        Vector3 looktarget = target.position + Vector3.up * height;
        Quaternion targetRptation = Quaternion.LookRotation(looktarget - targetPosition);

        currentPosition = Vector3.SmoothDamp(currentPosition, targetPosition, ref currenVelocity, positionSmoothTime);
        currenRotation = Quaternion.Slerp(currenRotation, targetRptation, Time.deltaTime / rotationSmoothTime);

        transform.position = currentPosition;
        transform.rotation = currenRotation;
    }

    void ToggleCursor()
    {
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
