using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using static UnityEditor.Timeline.TimelinePlaybackControls;
using Unity.VisualScripting;

public class PlayerMovementBehavior : MonoBehaviour
{

    // Controls the player sphere
    // Takes inputs from the user to control the sphere

    // Rigidbody
    private Rigidbody _rb;

    // X and Y movements
    private float _movementX;
    private float _movementY;

    // Timer variables
    private float _timeLimit = 5;
    private float _counter = 0;
    private float _timer = 60;

    // Movement speed
    [SerializeField] private float _moveSpeed = 10;
    private float _copyMoveSpeed;

    // Variables for checking if an object has been collected
    private bool _blueObjectCollected = false;
    private bool _allPickupsCollected = false;

    // UI variables
    private float _pickupCollected = 0;

    // Initialize TMP
    [SerializeField] private TextMeshProUGUI _countText;
    [SerializeField] private TextMeshProUGUI _timerText;
    [SerializeField] private GameObject _winText;
    [SerializeField] private GameObject _loseText;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        SetCountText();
    }

    private void FixedUpdate()
    {
        Vector3 _movement = new Vector3(_movementX, 0.0f, _movementY);
        _rb.AddForce(_movement * _moveSpeed);

        // Increment counter
        _counter += Time.deltaTime;

        // Revert move speed back to original
        if (_counter >= _timeLimit && _blueObjectCollected)
        {
            _moveSpeed = _copyMoveSpeed;
            _blueObjectCollected = false;
        }

        // Counter increments down from 60 
        _timer -= Time.deltaTime;
        SetTimerText();

        // If timer reaches 0 and end goal not reached display lose text
        if (_timer <= 0 && !_allPickupsCollected)
        {
            _loseText.SetActive(true);
            Time.timeScale = 0f;
        }

        // Check if all the pick up's have been collected
        if (_pickupCollected == 5)
        {
            _allPickupsCollected = true;
        }
    }

    void OnMove(InputValue _inputVal)
    {
        // Set the movement vector
        Vector2 _movementVector = _inputVal.Get<Vector2>();
        _movementX = _movementVector.x;
        _movementY = _movementVector.y;
    }

    void SetCountText()
    {
        _countText.text = "Pick Up's Collected: " + _pickupCollected.ToString() + "/5";
    }

    // Set timer on the UI
    void SetTimerText()
    {
        _timerText.text = "Time Left: " + _timer.ToString() + "s";
    }

    // Controls what happens when player sphere hits differnt pick up items.
    void OnTriggerEnter(Collider _other)
    {
        // Increase the counter on the UI and also remove pick up when collected
        if (_other.gameObject.CompareTag("PickUp"))
        {
            _pickupCollected += 1;
            SetCountText();
            _other.gameObject.SetActive(false);
        }
        // Remove when collected
        // Reduce player speed my 1/2 for 5 seconds
        if (_other.gameObject.CompareTag("BlueObject"))
        {
            // Start the timer for 5 seconds and return movement speed to normal after
            _counter = 0;
            _copyMoveSpeed = _moveSpeed;
            _moveSpeed /= 2;

            _blueObjectCollected = true;
            _other.gameObject.SetActive(false);
        }
        // Remove when collected
        // Reset player to the start point
        if (_other.gameObject.CompareTag("RedObject"))
        {
            transform.position = new Vector3(22.2f, 0.93f, -21.1f);
            _other.gameObject.SetActive(false);
        }
        // End game and display either win message on UI
        // Only end game if all the pick ups are collected
        // If pick ups are not collected do nothing
        if (_other.gameObject.CompareTag("EndGoal"))
        {
            if (_allPickupsCollected)
            {
                // Display "You win" text
                _winText.SetActive(true);
                _other.gameObject.SetActive(false);
                Time.timeScale = 0f;
            }
        }
    }

}
