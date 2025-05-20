using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Samp1 : MonoBehaviour
{
    private float rotationDuration = .5f;

    private bool isRotated = false;
    private bool isRotating = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        string name = transform.parent.name;
        Debug.Log($"touch on: {name}");

        if(!isRotating)
        {
            RotateBlock();
        }
    }

    private void RotateBlock()
    {
        float fromAngle = isRotated ? 180 : 0;
        float toAngle = isRotated ? 0 : 180;

        StartCoroutine(RotateOverY(fromAngle, toAngle, rotationDuration));
    }

    IEnumerator RotateOverY(float fromAngle, float toAngle, float duration)
    {
        isRotating = true;

        float elapsed = 0;

        while(elapsed < duration)
        {
            float yRotation = Mathf.Lerp(fromAngle, toAngle, elapsed / duration);
            transform.parent.rotation = Quaternion.Euler(0, yRotation, 0);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.parent.rotation = Quaternion.Euler(0, toAngle, 0);
        isRotating = false;

    }
}
