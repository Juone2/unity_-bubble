using UnityEngine;

namespace BubbleBattle.Items
{
    [RequireComponent(typeof(Collider2D))]
    public class ItemPickup : MonoBehaviour
    {
        [Header("Pickup Settings")]
        [SerializeField] private ItemBase item;
        [SerializeField] private float rotationSpeed = 90f;
        [SerializeField] private float bobSpeed = 2f;
        [SerializeField] private float bobHeight = 0.5f;
        
        [Header("Visual Effects")]
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private ParticleSystem pickupEffect;
        
        private Vector3 startPosition;
        private float timeOffset;
        
        public ItemBase Item => item;
        public System.Action<ItemPickup> OnPickedUp;
        
        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
                spriteRenderer = GetComponentInChildren<SpriteRenderer>();
                
            // Set up collider as trigger
            var collider = GetComponent<Collider2D>();
            if (collider != null)
                collider.isTrigger = true;
        }
        
        private void Start()
        {
            startPosition = transform.position;
            timeOffset = Random.Range(0f, Mathf.PI * 2f); // Random phase for bobbing
        }
        
        private void Update()
        {
            AnimatePickup();
        }
        
        public void Initialize(ItemBase itemData)
        {
            item = itemData;
            
            if (spriteRenderer != null && item.ItemIcon != null)
            {
                spriteRenderer.sprite = item.ItemIcon;
            }
            
            // Set pickup name for debugging
            gameObject.name = $"Pickup_{item.ItemName}";
        }
        
        private void AnimatePickup()
        {
            // Rotation
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            
            // Bobbing motion
            float newY = startPosition.y + Mathf.Sin((Time.time + timeOffset) * bobSpeed) * bobHeight;
            transform.position = new Vector3(startPosition.x, newY, startPosition.z);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            var player = other.GetComponent<MonoBehaviour>();
            if (player != null && player.GetType().Name == "PlayerController")
            if (player != null)
            {
                PickupItem(player);
            }
        }
        
        private void PickupItem(MonoBehaviour player)
        {
            if (item == null)
            {
                Debug.LogWarning("Tried to pick up item but item is null!");
                return;
            }
            
            // Try to add item to player's inventory
            bool added = false;
            var addItemMethod = player.GetType().GetMethod("AddItem");
            if (addItemMethod != null)
            {
                var result = addItemMethod.Invoke(player, new object[] { item.CreateInstance() });
                if (result is bool success) added = success;
            }
            
            if (added)
            {
                // Play pickup effect
                if (pickupEffect != null)
                {
                    pickupEffect.Play();
                }
                
                // Notify systems
                OnPickedUp?.Invoke(this);
                
                Debug.Log($"Player {player.name} picked up {item.ItemName}");
                
                // Destroy pickup object
                Destroy(gameObject);
            }
            else
            {
                Debug.Log($"Player {player.name}'s inventory is full!");
            }
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, 0.5f);
        }
    }
}