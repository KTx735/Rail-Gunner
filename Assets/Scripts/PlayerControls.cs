using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{

    [SerializeField] InputAction movement;
    [SerializeField] float controlSpeed = 20f;
    [SerializeField] float xRange = 5f;
    [SerializeField] float yRange = 5f;

    // Update is called once per frame
    void Update()
    {
        float xThrow = movement.ReadValue<Vector2>().x; //Movement in x
        float yThrow = movement.ReadValue<Vector2>().y; //Movement in y

        float xOffSet = xThrow * Time.deltaTime * controlSpeed; //Movement * FR (To keep movement independent) * Speed
        float yOffset = yThrow * Time.deltaTime * controlSpeed; //Movement * FR (To keep movement independent) * Speed
        
        float rawXPos = transform.localPosition.x + xOffSet; //New position for x
        float rawYPos = transform.localPosition.y + yOffset; //New position for y

        //Clamp
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange); //Restrict Movement for x
        //float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange); //Restrict Movement for y

        transform.localPosition = new Vector3(clampedXPos, rawYPos, transform.localPosition.z);
    }

    void OnEnable()
    {
        movement.Enable();
    }

    void OnDisable()
    {
         
    }
}
