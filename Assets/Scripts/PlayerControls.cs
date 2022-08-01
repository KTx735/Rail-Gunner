using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControls : MonoBehaviour
{

    [SerializeField] InputAction movement;
    [SerializeField] float controlSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xThrow = movement.ReadValue<Vector2>().x;
        float yThrow = movement.ReadValue<Vector2>().y;
        float xOffSet = xThrow * Time.deltaTime * controlSpeed;
        float yOffset = yThrow * Time.deltaTime * controlSpeed;;
        float newXPos = transform.localPosition.x + xOffSet;
        float newYPos = transform.localPosition.y + yOffset;

        //Debug:
        Debug.Log(xThrow);
        Debug.Log(yThrow);

        transform.localPosition = new Vector3(newXPos, newYPos, transform.localPosition.z);
    }

    void OnEnable()
    {
        movement.Enable();
    }

    void OnDisable()
    {
         
    }
}
