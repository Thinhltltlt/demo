using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 MoveDirection { get; private set; }
    public bool Jump { get; set; }
    public bool Spring { get; private set; }
    public bool Attack { get; private set; }
    
    public void OnMove(InputValue inputValue)
    {
        MoveDirection = inputValue.Get<Vector2>();
    }
    
    public void OnJump(InputValue inputValue)
    {
        Jump = inputValue.isPressed;
    }
    
    public void OnSpring(InputValue inputValue)
    {
        Spring = inputValue.isPressed;
    }
    
    public void OnAttack(InputValue inputValue)
    {
        Attack = inputValue.isPressed;
    }
}
