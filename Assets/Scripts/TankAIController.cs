using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAIController : MonoBehaviour
{
    // Make tanks automatically move away from the player tank
    // Apply rotations on the enemeny tanks
    // Make tanks only move away when player tank is in a certain radius
    // Tanks should turn smoothly

    // Create gameobject for player tank
    [SerializeField] private GameObject _playerTank;
    private float _moveDist = 60;

    // boolean to start moving the tanks
    private bool _active = false;

    // movement speed for the tanks
    private float _moveSpeed = 20;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        // Tanks will not move unless the player tank is within 60
        if (Vector3.Distance(transform.position, _playerTank.transform.position) <= _moveDist)
        {

            // Get the opposite direction of the player tank
            Vector3 _playerTankDirection = new Vector3(transform.position.x, _playerTank.transform.position.y, transform.position.z) - _playerTank.transform.position;

            // Get the rotation of the playerTank
            Quaternion _targetRotation = Quaternion.LookRotation(_playerTankDirection);

            // Rotate the tanks 
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, 2 * Time.deltaTime);

            _active = true;
        }

        if (_active)
        {
            // Move the tanks when the player tank is near
            transform.Translate(Vector3.forward * Time.deltaTime * _moveSpeed);
        }

    }


}
