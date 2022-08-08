using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{

    [Header("General Setup Setting")]
    [SerializeField] InputAction movement;
    [SerializeField] InputAction fire;

    [Tooltip("How fast the ship moves up and down")] 
    [SerializeField] float controlSpeed = 20f;
    [Tooltip("How far the ship moves horizontally")]
    [SerializeField] float xRange = 8.6f;
    [Tooltip("How far the ship moves vertically")]
    [SerializeField] float yRangePos = 6.6f;
    [Tooltip("How far the ship moves vertically")]
    [SerializeField] float yRangeNeg = 3.1f;

    [Header("Screen Position Based Turning")]
    [SerializeField] float positionPitchFactor = -3f;
    [SerializeField] float positionYawFactor = -3f;

    [Header("Player Input Based Turning")]
    [SerializeField] float controlPitchFactor = -3f;
    [SerializeField] float controlRollFactor = -3;

    //Set up array for the lasers
    [Header("Laser Array Setup")]
    [Tooltip("Add lasers for ship here")]
    [SerializeField] GameObject[] lasers;

    float xThrow;
    float yThrow;

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void OnEnable()
    {
        movement.Enable();
        fire.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
        fire.Disable();
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

    void ProcessFiring()
    {
        if (fire.ReadValue<float>() > 0.5)
        {
            setLaserActive(true);
        }
        else
        {
            setLaserActive(false);
        }
    }

    void setLaserActive(bool state)
    {
        foreach (GameObject laser in lasers) //Object in array
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = state;
        }
    }

}
