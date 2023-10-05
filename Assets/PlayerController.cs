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
    public float speed;
    public float turnSpeed;
    public float brakeForce;
    
    //car data
    public Rigidbody2D rig2D;
    public Transform carModel;
    private Vector2 _currentOffset;
    
    
    private float _turnInput;
    
    //bool variables to listen input from user
    private bool _bMoveForwardInput;
    private bool _bBrakeInput;
    
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
            rig2D.AddForce(-rig2D.velocity * (brakeForce*2), ForceMode2D.Force);
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
        Debug.Log("Turn Input: " + _turnInput);
    }
}
