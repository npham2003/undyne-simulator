using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelMovement : MonoBehaviour
{
    public float rotationSpeed;


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

    }

   

}
