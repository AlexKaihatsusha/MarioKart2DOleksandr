using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    //car movement variables
    [Header("[Car movement]")]
    public float speed;
    public float turnSpeed;
    public float brakeForce;
    private float _turnInput;
    
    //car data
    [Header("[Car data]")]
    public Rigidbody2D rig2D;
    public Transform carModel;
    private Vector2 _currentOffset;
    
    //bool variables to listen input from user for InputSystem
    private bool _bMoveForwardInput;
    private bool _bBrakeInput;
    
    //---------------------------------
    [Header("[Race data]")]
    public int maxCheckpointNumber= 0;
    
    private int _currentLap = 0;
    private int _nextCheckpoint = 1;
    //---------------------------------
    
    public int GetCurrentLap()
    {
        return _currentLap;
    }

    private void Start()
    {
        _currentOffset = carModel.localPosition;
    }
    private void FixedUpdate()
    {
        if(_bMoveForwardInput)
        {
            rig2D.AddForce(transform.up * speed,ForceMode2D.Force);
            
        }else{
            rig2D.AddForce(-rig2D.velocity * brakeForce, ForceMode2D.Force);
        }

        if (_bBrakeInput)
        {
            rig2D.AddForce(-rig2D.velocity * (brakeForce*4), ForceMode2D.Force);
        }
        
        if(_turnInput != 0)
            rig2D.rotation -= turnSpeed* _turnInput;
    }

    public void OnMoveForward(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.phase==InputActionPhase.Performed)
        {
            _bMoveForwardInput = true;
        }
        else
        {
            _bMoveForwardInput = false;
        }
    }

    public void OnBrakeInput(InputAction.CallbackContext callbackContext)
    {
        if(callbackContext.phase == InputActionPhase.Performed)
        {
            _bBrakeInput = true;
        }
        else
        {
            _bBrakeInput = false;
        }
    }
    
    public void OnTurnInput(InputAction.CallbackContext callbackContext) 
    {
        _turnInput = callbackContext.ReadValue<float>();
        //Debug.Log("Turn Input: " + _turnInput);
    }
  
    private void OnTriggerEnter2D(Collider2D other)
    {
        //get a reference of Checkpoint class
        Checkpoint checkpoint = other.GetComponent<Checkpoint>();
        
        //check the current checkpoint index
        if (checkpoint != null && checkpoint.Index == _nextCheckpoint)
        {
            // Increase the number of next expected checkpoint.
            _nextCheckpoint++;

            // Check if player went through all checkpoints and increase the current lap number.
            if (_nextCheckpoint > maxCheckpointNumber)
            {
                _currentLap++;
                Debug.Log($"Lap: {_currentLap}");
                //check if player reached the finish lap or not
                RaceManager.Instance.CheckWinPlayer(this);
                // Reset nextCheckpointNumber.
                _nextCheckpoint = 1;
            }
        }
    }
}
