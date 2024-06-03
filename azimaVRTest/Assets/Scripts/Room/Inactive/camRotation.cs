using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camRotation : MonoBehaviour
{
    public float mouseSens = 100f;
    float xRotation = 0f;
    float yRotation = 0f;
    public Quaternion startingRot;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = startingRot;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        yRotation += mouseX;

     
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
      //  transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);


    }
}
