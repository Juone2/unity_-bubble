using UnityEngine;
using BubbleBattle.Core;
using BubbleBattle.Items;

namespace BubbleBattle.Player
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Player Settings")]
        [SerializeField] private PlayerData playerData;
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float originalMoveSpeed = 5f;
        [SerializeField] private bool controlsReversed = false;
        [SerializeField] private int damageReduction = 0;
        
        [Header("Components")]
        private Rigidbody2D rb2d;
        private PlayerInput playerInput;
        private PlayerStats playerStats;
        
        [Header("Items")]
        [SerializeField] private ItemBase[] inventory = new ItemBase[3];
        [SerializeField] private int currentItemIndex = 0;
        
        public PlayerData PlayerData => playerData;
        public bool ControlsReversed { get => controlsReversed; set => controlsReversed = value; }
        public ItemBase CurrentItem => inventory[currentItemIndex];
        public ItemBase[] Inventory => inventory;
        public int CurrentItemIndex => currentItemIndex;
        
        public System.Action<Vector2> OnPlayerMoved;
        public System.Action<ItemBase> OnItemUsed;
        public System.Action<int> OnItemSwitched;
        public System.Action<int> OnHealthChanged;
        
        private void Awake()
        {
            rb2d = GetComponent<Rigidbody2D>();
            playerInput = GetComponent<PlayerInput>();
            playerStats = GetComponent<PlayerStats>();
            
            if (playerInput == null)
                playerInput = gameObject.AddComponent<PlayerInput>();
            if (playerStats == null)
                playerStats = gameObject.AddComponent<PlayerStats>();
        }
        
        private void Start()
        {
            InitializePlayer();
        }
        
        private void Update()
        {
            HandleMovement();
            HandleItemInput();
        }
        
        private void InitializePlayer()
        {
            transform.position = playerData.spawnPosition;
            playerStats.Initialize(playerData);
            originalMoveSpeed = moveSpeed;
            
            // Set player visual (color, etc.)
            var renderer = GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.color = playerData.playerColor;
            }
        }
        
        private void HandleMovement()
        {
            if (GameManager.Instance.CurrentGameState != GameState.Playing)
                return;
                
            Vector2 inputVector = playerInput.GetMovementInput();
            
            if (controlsReversed)
            {
                inputVector = -inputVector;
            }
            
            Vector2 movement = inputVector * moveSpeed;
            rb2d.velocity = movement;
            
            if (movement.magnitude > 0.1f)
            {
                OnPlayerMoved?.Invoke(transform.position);
            }
        }
        
        private void HandleItemInput()
        {
            if (GameManager.Instance.CurrentGameState != GameState.Playing)
                return;
                
            if (playerInput.GetItemUseInput())
            {
                UseCurrentItem();
            }
            
            if (playerInput.GetItemSwitchInput())
            {
                SwitchToNextItem();
            }
        }
        
        public void UseCurrentItem()
        {
            if (inventory[currentItemIndex] != null)
            {
                inventory[currentItemIndex].Use(this);
                OnItemUsed?.Invoke(inventory[currentItemIndex]);
                
                // Remove used item
                inventory[currentItemIndex] = null;
            }
        }
        
        public void SwitchToNextItem()
        {
            int startIndex = currentItemIndex;
            
            do
            {
                currentItemIndex = (currentItemIndex + 1) % inventory.Length;
            }
            while (inventory[currentItemIndex] == null && currentItemIndex != startIndex);
            
            OnItemSwitched?.Invoke(currentItemIndex);
        }
        
        public bool AddItem(ItemBase item)
        {
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == null)
                {
                    inventory[i] = item;
                    return true;
                }
            }
            return false; // Inventory full
        }
        
        public void TakeDamage(int damage)
        {
            // Apply damage reduction
            int reducedDamage = Mathf.Max(0, damage - (damage * damageReduction / 100));
            playerStats.TakeDamage(reducedDamage);
            OnHealthChanged?.Invoke(playerStats.CurrentHealth);
            
            if (playerStats.CurrentHealth <= 0)
            {
                HandlePlayerDeath();
            }
        }
        
        public void AddScore(int points)
        {
            playerStats.AddScore(points);
        }
        
        private void HandlePlayerDeath()
        {
            // Handle player death logic
            Debug.Log($"Player {playerData.playerId} died!");
        }
        
        public void SetMoveSpeed(float newSpeed)
        {
            moveSpeed = newSpeed;
        }
        
        public float GetMoveSpeed()
        {
            return moveSpeed;
        }
        
        public float GetOriginalMoveSpeed()
        {
            return originalMoveSpeed;
        }
        
        public void SetDamageReduction(int reduction)
        {
            damageReduction = Mathf.Clamp(reduction, 0, 100);
        }
        
        public int GetDamageReduction()
        {
            return damageReduction;
        }
        
        public void SetControlsReversed(bool reversed)
        {
            controlsReversed = reversed;
        }
        
        public void ResetPlayer()
        {
            transform.position = playerData.spawnPosition;
            playerStats.ResetStats();
            controlsReversed = false;
            moveSpeed = originalMoveSpeed;
            damageReduction = 0;
            
            // Clear inventory
            for (int i = 0; i < inventory.Length; i++)
            {
                inventory[i] = null;
            }
            currentItemIndex = 0;
        }
    }
}