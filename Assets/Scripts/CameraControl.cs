using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CameraControl : MonoBehaviour
{

    public Camera camera;


    public float near = 20.0f;
    public float far = 100.0f;

    public float sensitivityX = 10f;
    public float sensitivityY = 10f;
    public float sensitivetyZ = 2f;
    public float sensitivetyMouseWheel = 20f;


    void Update()
    {

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            //滚轮实现镜头缩进和拉远
            if (Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                float transZ = Input.GetAxis("Mouse ScrollWheel") * sensitivetyMouseWheel;

                transform.position += transform.forward * transZ;

            }
            //鼠标右键实现视角转动，类似第一人称视角
            if (Input.GetMouseButton(1))
            {
                float rotationX = Input.GetAxis("Mouse X") * sensitivityX;
                float rotationY = Input.GetAxis("Mouse Y") * sensitivityY;
                transform.RotateAround(Vector3.zero, Vector3.up, rotationX);
                transform.RotateAround(Vector3.zero, transform.right, -rotationY);
            }
            if (Input.GetMouseButton(2))
            {
                float transX = -Input.GetAxis("Mouse X") * sensitivityX;
                float transY = -Input.GetAxis("Mouse Y") * sensitivityY;

                Vector3 moveMent = transform.right * transX + transform.up * transY;

                transform.position += moveMent;

            }
        }
            

    }


}