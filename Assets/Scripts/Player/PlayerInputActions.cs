using UnityEngine;
using UnityEngine.InputSystem;

namespace BubbleBattle.Player
{
    [System.Serializable]
    public class PlayerInputActions
    {
        public Player1Map Player1;
        public Player2Map Player2;
        
        public PlayerInputActions()
        {
            Player1 = new Player1Map();
            Player2 = new Player2Map();
        }
        
        public void Enable()
        {
            Player1.Enable();
            Player2.Enable();
        }
        
        public void Disable()
        {
            Player1.Disable();
            Player2.Disable();
        }
        
        [System.Serializable]
        public class Player1Map
        {
            public InputAction Move;
            public InputAction UseItem;
            public InputAction SwitchItem;
            
            public Player1Map()
            {
                // Player 1 uses WASD for movement
                Move = new InputAction(binding: "<Keyboard>/wasd");
                UseItem = new InputAction(binding: "<Keyboard>/leftShift");
                SwitchItem = new InputAction(binding: "<Keyboard>/leftCtrl");
            }
            
            public void Enable()
            {
                Move.Enable();
                UseItem.Enable();
                SwitchItem.Enable();
            }
            
            public void Disable()
            {
                Move.Disable();
                UseItem.Disable();
                SwitchItem.Disable();
            }
        }
        
        [System.Serializable]
        public class Player2Map
        {
            public InputAction Move;
            public InputAction UseItem;
            public InputAction SwitchItem;
            
            public Player2Map()
            {
                // Player 2 uses Arrow Keys for movement
                Move = new InputAction();
                Move.AddBinding("<Keyboard>/leftArrow").WithProcessor("scale(x=-1)");
                Move.AddBinding("<Keyboard>/rightArrow");
                Move.AddBinding("<Keyboard>/downArrow").WithProcessor("scale(y=-1)");
                Move.AddBinding("<Keyboard>/upArrow");
                Move.WithProcessor("StickDeadzone");
                
                UseItem = new InputAction(binding: "<Keyboard>/rightShift");
                SwitchItem = new InputAction(binding: "<Keyboard>/rightCtrl");
            }
            
            public void Enable()
            {
                Move.Enable();
                UseItem.Enable();
                SwitchItem.Enable();
            }
            
            public void Disable()
            {
                Move.Disable();
                UseItem.Disable();
                SwitchItem.Disable();
            }
        }
    }
}