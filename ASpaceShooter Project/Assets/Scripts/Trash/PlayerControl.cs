using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerControl : MonoBehaviour
{
    [Header("General")] [Tooltip("метров в секунду")] [SerializeField]
    private float Speed = 4f;


    [SerializeField] private float xClamp;
    [SerializeField] private float yClamp;

    [Header("Rotation Factor")] [SerializeField]
    private float xRotFactor = -5f;

    [SerializeField] private float yRotFactor = 5f;

    [SerializeField] private ParticleSystem[] guns;

    //[SerializeField] private float zRotFactor = 4f;
    [Header("Move Rotation")] [SerializeField]
    private float xMoveRotation = -10f;

    [SerializeField] private float yMoveRotation = 10f;
    [SerializeField] private float zMoveRotation = -10f;

    private bool isControlEnabled = true;

    private float xMove, yMove;

    void Update()
    {
        if (isControlEnabled)
        {
            MoveShip();
            RotateShip();
            FireGuns();
        }
    }

    void OnPlayerDeath()
    {
        print("ControlOff");
        isControlEnabled = false;
    }



    void MoveShip()
    {
        xMove = CrossPlatformInputManager.GetAxis("Horizontal");
        yMove = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xMove * Speed * Time.deltaTime;
        float yOffset = yMove * Speed * Time.deltaTime;
        float newXpos = Mathf.Clamp(transform.localPosition.x + xOffset, -xClamp, xClamp);
        float newYpos = Mathf.Clamp(transform.localPosition.y + yOffset, -yClamp, yClamp);
        transform.localPosition = new Vector3(newXpos, newYpos, transform.localPosition.z);
    }

    void RotateShip()
    {
        float xRot = transform.localPosition.y * xRotFactor + yMove * xMoveRotation;
        float yRot = transform.localPosition.x * yRotFactor + xMove * yMoveRotation;
        float zRot = xMove * zMoveRotation;
        transform.localRotation = Quaternion.Euler(xRot, yRot, zRot);
    }

    void FireGuns()
    {
        if (CrossPlatformInputManager.GetButtonDown("Fire1"))
        {
            foreach (ParticleSystem gun in guns)
            {
                gun.Play(); 
            }
            
        }
        else if (CrossPlatformInputManager.GetButtonUp("Fire1"))
        {
            foreach (ParticleSystem gun in guns)
            {
                gun.Stop(); 
            }
        }
    }
}



