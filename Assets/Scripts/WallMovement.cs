using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovement : MonoBehaviour
{
    public float rotationSpeed;
    private bool isRotating = false;

    private float upAngle = 180f;
    private float downAngle = 0f;
    private float rightAngle = 90f;
    private float leftAngle = 270f;
    private int currStatus = 2;
    // up: 0, right: 1, down: 2, left: 3


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow) && !isRotating)
        { 
            if(currStatus != 0)
            {
                StartCoroutine(RotateAngle(upAngle));
                currStatus = 0;
            }
           
        }  

        else if(Input.GetKey(KeyCode.RightArrow))
        {
            if(currStatus != 1)
            {
                StartCoroutine(RotateAngle(rightAngle));
                currStatus = 1;
            }
            
        } 

        else if(Input.GetKey(KeyCode.DownArrow))
        {
            
            if(currStatus != 2)
            {
                StartCoroutine(RotateAngle(downAngle));
                currStatus = 2;
            }
        }   

        else if(Input.GetKey(KeyCode.LeftArrow))
        {   
            if(currStatus != 3)
            {
                StartCoroutine(RotateAngle(leftAngle));
                currStatus = 3;
            }
        }  

        Debug.Log(gameObject.transform.rotation.eulerAngles);
    }

    IEnumerator RotateAngle(float rotationAngle)
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
