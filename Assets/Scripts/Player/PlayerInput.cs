using UnityEngine;

namespace BubbleBattle.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [Header("Input Settings")]
        [SerializeField] private int playerId = 1;
        
        private Vector2 movementInput;
        private bool itemUsePressed;
        private bool itemSwitchPressed;
        
        public int PlayerId => playerId;
        
        private void Update()
        {
            // Player 1 controls (WASD)
            if (playerId == 1)
            {
                float horizontal = 0f;
                float vertical = 0f;
                
                if (Input.GetKey(KeyCode.A)) horizontal -= 1f;
                if (Input.GetKey(KeyCode.D)) horizontal += 1f;
                if (Input.GetKey(KeyCode.S)) vertical -= 1f;
                if (Input.GetKey(KeyCode.W)) vertical += 1f;
                
                movementInput = new Vector2(horizontal, vertical).normalized;
                
                itemUsePressed = Input.GetKeyDown(KeyCode.LeftShift);
                itemSwitchPressed = Input.GetKeyDown(KeyCode.LeftControl);
            }
            // Player 2 controls (Arrow Keys)
            else
            {
                float horizontal = 0f;
                float vertical = 0f;
                
                if (Input.GetKey(KeyCode.LeftArrow)) horizontal -= 1f;
                if (Input.GetKey(KeyCode.RightArrow)) horizontal += 1f;
                if (Input.GetKey(KeyCode.DownArrow)) vertical -= 1f;
                if (Input.GetKey(KeyCode.UpArrow)) vertical += 1f;
                
                movementInput = new Vector2(horizontal, vertical).normalized;
                
                itemUsePressed = Input.GetKeyDown(KeyCode.RightShift);
                itemSwitchPressed = Input.GetKeyDown(KeyCode.RightControl);
            }
        }
        
        public Vector2 GetMovementInput()
        {
            return movementInput;
        }
        
        public bool GetItemUseInput()
        {
            bool pressed = itemUsePressed;
            itemUsePressed = false; // Reset after reading
            return pressed;
        }
        
        public bool GetItemSwitchInput()
        {
            bool pressed = itemSwitchPressed;
            itemSwitchPressed = false; // Reset after reading
            return pressed;
        }
        
        public void SetPlayerId(int id)
        {
            playerId = id;
        }
    }
}