using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{

    [SerializeField] InputAction movement;
    [SerializeField] float controlSpeed = 20f;

    /*[SerializeField]*/ float xRange = 8.6f;
    /*[SerializeField]*/ float yRangePos = 6.6f;
    /*[SerializeField]*/ float yRangeNeg = 3.1f;

    [SerializeField] float positionPitchFactor = -3f;
    [SerializeField] float controlPitchFactor = -3f;
    [SerializeField] float positionYawFactor = -3f;
    [SerializeField] float controlRollFactor = -3;

    float xThrow;
    float yThrow;

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    void OnEnable()
    {
        movement.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
    }

    void ProcessTranslation() //Movement of the ship
    {
        xThrow = movement.ReadValue<Vector2>().x; //Movement in x
        yThrow = movement.ReadValue<Vector2>().y; //Movement in y

        float xOffSet = xThrow * Time.deltaTime * controlSpeed; //Movement * FR (To keep movement independent) * Speed
        float yOffset = yThrow * Time.deltaTime * controlSpeed; //Movement * FR (To keep movement independent) * Speed
        
        float rawXPos = transform.localPosition.x + xOffSet; //New position for x
        float rawYPos = transform.localPosition.y + yOffset; //New position for y

        //Clamp
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange); //Restrict Movement for x
        float clampedYPos = Mathf.Clamp(rawYPos, -yRangeNeg, yRangePos); //Restrict Movement for y

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessRotation()
    {

        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

}
