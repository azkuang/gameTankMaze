using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTankController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TankMovementBehavior();
        TankRotateBehavior();
    }


    private void TankMovementBehavior()
    {
        float __verticalAxisInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * __verticalAxisInput * _movementSpeed * Time.deltaTime);
    }

    private void TankRotateBehavior()
    {
        float _horizontalAxisInput = Input.GetAxis("Horizontal");

        transform.Rotate(Vector3.up * _horizontalAxisInput * _rotationSpeed * Time.deltaTime);
    }
}
