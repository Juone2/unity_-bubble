using UnityEngine;
using UnityEngine.InputSystem;

namespace BubbleBattle.Player
{
    public class PlayerInput : MonoBehaviour
    {
        [Header("Input Settings")]
        [SerializeField] private int playerId = 1;
        [SerializeField] private bool useNewInputSystem = true;
        
        [Header("Input Actions")]
        private PlayerInputActions inputActions;
        private InputAction moveAction;
        private InputAction useItemAction;
        private InputAction switchItemAction;
        
        private Vector2 movementInput;
        private bool itemUsePressed;
        private bool itemSwitchPressed;
        
        public int PlayerId => playerId;
        
        private void Awake()
        {
            if (useNewInputSystem)
            {
                SetupNewInputSystem();
            }
        }
        
        private void SetupNewInputSystem()
        {
            inputActions = new PlayerInputActions();
            
            // Map actions based on player ID
            if (playerId == 1)
            {
                moveAction = inputActions.Player1.Move;
                useItemAction = inputActions.Player1.UseItem;
                switchItemAction = inputActions.Player1.SwitchItem;
            }
            else
            {
                moveAction = inputActions.Player2.Move;
                useItemAction = inputActions.Player2.UseItem;
                switchItemAction = inputActions.Player2.SwitchItem;
            }
        }
        
        private void OnEnable()
        {
            if (useNewInputSystem && inputActions != null)
            {
                inputActions.Enable();
                
                useItemAction.performed += OnUseItemPerformed;
                switchItemAction.performed += OnSwitchItemPerformed;
            }
        }
        
        private void OnDisable()
        {
            if (useNewInputSystem && inputActions != null)
            {
                useItemAction.performed -= OnUseItemPerformed;
                switchItemAction.performed -= OnSwitchItemPerformed;
                
                inputActions.Disable();
            }
        }
        
        private void Update()
        {
            if (useNewInputSystem)
            {
                UpdateNewInputSystem();
            }
            else
            {
                UpdateLegacyInputSystem();
            }
        }
        
        private void UpdateNewInputSystem()
        {
            if (moveAction != null)
            {
                movementInput = moveAction.ReadValue<Vector2>();
            }
        }
        
        private void UpdateLegacyInputSystem()
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
        
        private void OnUseItemPerformed(InputAction.CallbackContext context)
        {
            itemUsePressed = true;
        }
        
        private void OnSwitchItemPerformed(InputAction.CallbackContext context)
        {
            itemSwitchPressed = true;
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
            if (useNewInputSystem)
            {
                SetupNewInputSystem();
            }
        }
    }
}