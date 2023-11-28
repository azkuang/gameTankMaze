using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Controlls the camera movement to follow the sphere

    // Get the player sphere game object 
    public GameObject _player;

    // Create a camera offset
    private Vector3 _offset;

    // Start is called before the first frame update
    void Start()
    {
        _offset = transform.position - _player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = _player.transform.position + _offset;
    }
}
