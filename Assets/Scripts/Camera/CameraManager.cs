using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    public float CameraHeight = 11.5f;
    public float CameraAngle = 30f;

    public Camera mainCamera => Camera.main;
    public Vector3 projectCameraOnFloor => new Vector3(mainCamera.transform.position.x, 0f, mainCamera.transform.position.z);

    private int cameraCenterType = 0;
    public Vector3 cameraForwardVector
    {
        get
        {
            if (cameraCenterType == 0)
            {
                return new Vector3(3, -1, 3);
            }
            if (cameraCenterType == 1)
            {
                return new Vector3(12, -1, 12);
            }
            if (cameraCenterType == 2)
            {
                return new Vector3(21, -1, 21);
            }
            return Vector3.zero;
        }
    }

    private bool isRotating = false;
    private bool isMoving = false;
    private bool isMouseLeftPressing = false;
    private Vector3 lastMousePosition = new Vector3(0f, 0f, 0f);
    // 相对于初始方向顺时针移动了多少个45°
    private int _direction = 0;
    public int direction
    {
        get { return _direction; }
    }

    void Start()
    {
        float real_angle = CameraAngle / 180f * Mathf.PI;
        float dis = CameraHeight / Mathf.Tan(real_angle) * Mathf.Sin(Mathf.PI / 4f);
        Vector3 tmp = PlayerController.Instance.transform.position;
//        cameraForwardVector = new Vector3(tmp.x, tmp.y, tmp.z);
        mainCamera.transform.position = new Vector3(cameraForwardVector.x - dis, CameraHeight, cameraForwardVector.z - dis);
        mainCamera.transform.forward = cameraForwardVector - mainCamera.transform.position;
    }

    public void ResetMainCamera()
    {
        float real_angle = CameraAngle / 180f * Mathf.PI;
        float dis = CameraHeight / Mathf.Tan(real_angle) * Mathf.Sin(Mathf.PI / 4f);
        Vector3 tmp = PlayerController.Instance.transform.position;
//        cameraForwardVector = new Vector3(tmp.x, tmp.y, tmp.z);
        StartCoroutine(MovePosition(new Vector3(cameraForwardVector.x - dis, CameraHeight, cameraForwardVector.z - dis)));
        mainCamera.transform.forward = tmp - mainCamera.transform.position;
    }

    private void KeyboardMoveInput()
    {
        /*
        float speedX = Input.GetAxis("Horizontal") * GlobalSettings.Instance.MaxSpeedOfCameraMove[0];
        float speedZ = Input.GetAxis("Vertical") * GlobalSettings.Instance.MaxSpeedOfCameraMove[0];

        float horizontal = speedX * Time.deltaTime;
        float vertical = speedZ * Time.deltaTime;

        Vector3 tmp = (cameraForwardVector - projectCameraOnFloor).normalized * vertical
            + mainCamera.transform.right * horizontal;
        mainCamera.transform.position += tmp;
        cameraForwardVector += tmp;
        */
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            cameraCenterType = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cameraCenterType = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            cameraCenterType = 2;
        }

    }
    private void MouseMoveInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            ResetMainCamera();
            return;
        }

        for(int i = 0; i < 3; ++i)
        {
            float horizontal = 0f;
            float vertical = 0f;
            if (Input.mousePosition.x < Screen.width * GlobalSettings.Instance.moveFrameRangeRate[i])
            {
                horizontal -= GlobalSettings.Instance.MaxSpeedOfCameraMove[i] * Time.deltaTime;
            }
            if (Screen.width - Input.mousePosition.x < Screen.width * GlobalSettings.Instance.moveFrameRangeRate[i])
            {
                horizontal += GlobalSettings.Instance.MaxSpeedOfCameraMove[i] * Time.deltaTime;
            }
            if (Input.mousePosition.y < Screen.height * GlobalSettings.Instance.moveFrameRangeRate[i])
            {
                vertical -= GlobalSettings.Instance.MaxSpeedOfCameraMove[i] * Time.deltaTime;
            }
            if (Screen.height - Input.mousePosition.y < Screen.height * GlobalSettings.Instance.moveFrameRangeRate[i])
            {
                vertical += GlobalSettings.Instance.MaxSpeedOfCameraMove[i] * Time.deltaTime;
            }
            if (Mathf.Abs(horizontal) < 1e-5f && Mathf.Abs(vertical) < 1e-5f) continue;

            Vector3 tmp = (cameraForwardVector - projectCameraOnFloor).normalized * vertical
                + mainCamera.transform.right * horizontal;
            mainCamera.transform.position += tmp;
//            cameraForwardVector += tmp;
            break;
        }
    }
    private void MouseScaleInput()
    {
        float dis = Input.mouseScrollDelta.y * GlobalSettings.Instance.MaxForwardSpeedOfCameraMove * Time.deltaTime;
//        mainCamera.transform.position += mainCamera.transform.forward * dis;
        mainCamera.orthographicSize -= dis;
    }


    // playmode和buildmode都要用
    private void KeyboardRotateInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(Rotate(45f));
            _direction = (_direction + 1) % 8;
        } else if(Input.GetKeyDown(KeyCode.E)){
            StartCoroutine(Rotate(-45f));
            _direction = (_direction + 3) % 8;
        }

    }

    private void MouseRotateInput()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector3 curMousePosition = Input.mousePosition;
            if (isMouseLeftPressing)
            {
                Vector3 tmp = curMousePosition - lastMousePosition;
                float ang = (tmp.x / Screen.width) * 360f;

                mainCamera.transform.RotateAround(cameraForwardVector, Vector3.up, ang);
            }
            isMouseLeftPressing = true;
            lastMousePosition = curMousePosition;
        }
        else
        {
            isMouseLeftPressing = false;
        }
    }

    private IEnumerator MovePosition(Vector3 des)
    {
        float dtime = 0.01f;
        float ddist = GlobalSettings.Instance.MaxSpeedOfCameraDirectedMove * dtime;
        Vector3 x = des - mainCamera.transform.position;
        Vector3 tmp = x.normalized * ddist;
        isMoving = true;
        while(x.magnitude > ddist)
        {
            mainCamera.transform.position += tmp;
            yield return new WaitForSeconds(dtime);
            x = des - mainCamera.transform.position;
            tmp = x.normalized * ddist;
        }
        mainCamera.transform.position = des;
        yield return new WaitForSeconds(dtime);
        isMoving = false;
    }

    private IEnumerator Rotate(float angle)
    {
        float number = GlobalSettings.Instance.CameraRotateTime * 100f;
        float nextAngle = angle / number;
        Vector3 center = cameraForwardVector;
        isRotating = true;
        for(int i = 0; i < number; i++)
        {
            mainCamera.transform.RotateAround(center, Vector3.up, nextAngle);
            yield return new WaitForSeconds(0.01f);
        }
        isRotating = false;
    }

    void Update()
    {
        if (!isRotating && !isMoving && Time.timeScale > 0f)
        {
            KeyboardMoveInput();
//            KeyboardRotateInput();
            MouseRotateInput();
            MouseScaleInput();
//            MouseMoveInput();
        }
    }
}
