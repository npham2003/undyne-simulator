using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelMovement : MonoBehaviour
{
    public float rotationSpeed;
    private bool isRotating = false;

    private float upAngle = 180f;
    private float downAngle = 0f;
    private float rightAngle = 90f;
    private float leftAngle = 270f;
    private int currStatus = 2;

    private List<float> angles;
    // up: 0, right: 1, down: 2, left: 3


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator RotateAngle(float rotationAngle)
    {
        isRotating = true;

        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, rotationAngle);

        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime);
            // elapsedTime += Time.deltaTime * rotationSpeed;
            elapsedTime += rotationSpeed;
            yield return null;
        }

        transform.rotation = targetRotation;
        isRotating = false;
    }

   

}
