using System.Collections.Generic;
using UnityEngine;
using BubbleBattle.Core;

namespace BubbleBattle.Items
{
    public class ItemManager : MonoBehaviour
    {
        public static ItemManager Instance { get; private set; }
        
        [Header("Item Spawning")]
        [SerializeField] private ItemBase[] availableItems;
        [SerializeField] private Transform[] itemSpawnPoints;
        [SerializeField] private float spawnInterval = 10f;
        [SerializeField] private int maxItemsOnField = 5;
        [SerializeField] private GameObject itemPickupPrefab;
        
        [Header("Item Pool")]
        private List<ItemPickup> activeItemPickups = new List<ItemPickup>();
        private float lastSpawnTime;
        
        public ItemBase[] AvailableItems => availableItems;
        public System.Action<ItemBase, Vector3> OnItemSpawned;
        public System.Action<ItemBase> OnItemPickedUp;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        private void Start()
        {
            InitializeItemManager();
        }
        
        private void Update()
        {
            if (GameManager.Instance.CurrentGameState == GameState.Playing)
            {
                CheckItemSpawning();
            }
        }
        
        private void InitializeItemManager()
        {
            // Find item spawn points if not assigned
            if (itemSpawnPoints == null || itemSpawnPoints.Length == 0)
            {
                var spawnPointObjects = GameObject.FindGameObjectsWithTag("ItemSpawnPoint");
                itemSpawnPoints = new Transform[spawnPointObjects.Length];
                
                for (int i = 0; i < spawnPointObjects.Length; i++)
                {
                    itemSpawnPoints[i] = spawnPointObjects[i].transform;
                }
            }
        }
        
        private void CheckItemSpawning()
        {
            if (Time.time - lastSpawnTime >= spawnInterval && 
                activeItemPickups.Count < maxItemsOnField)
            {
                SpawnRandomItem();
            }
        }
        
        public void SpawnRandomItem()
        {
            if (availableItems.Length == 0 || itemSpawnPoints.Length == 0)
                return;
                
            // Choose random item
            ItemBase randomItem = availableItems[Random.Range(0, availableItems.Length)];
            
            // Choose random spawn point
            Transform spawnPoint = itemSpawnPoints[Random.Range(0, itemSpawnPoints.Length)];
            
            // Check if spawn point is occupied
            if (IsSpawnPointOccupied(spawnPoint.position))
                return;
                
            SpawnItem(randomItem, spawnPoint.position);
        }
        
        public void SpawnItem(ItemBase item, Vector3 position)
        {
            if (itemPickupPrefab == null)
            {
                Debug.LogError("Item pickup prefab not assigned!");
                return;
            }
            
            GameObject pickupObject = Instantiate(itemPickupPrefab, position, Quaternion.identity);
            ItemPickup pickup = pickupObject.GetComponent<ItemPickup>();
            
            if (pickup != null)
            {
                pickup.Initialize(item);
                activeItemPickups.Add(pickup);
                pickup.OnPickedUp += OnItemPickupCollected;
                
                OnItemSpawned?.Invoke(item, position);
                lastSpawnTime = Time.time;
                
                Debug.Log($"Spawned {item.ItemName} at {position}");
            }
        }
        
        private void OnItemPickupCollected(ItemPickup pickup)
        {
            activeItemPickups.Remove(pickup);
            OnItemPickedUp?.Invoke(pickup.Item);
        }
        
        private bool IsSpawnPointOccupied(Vector3 position, float checkRadius = 1f)
        {
            Collider2D[] overlapping = Physics2D.OverlapCircleAll(position, checkRadius);
            foreach (var collider in overlapping)
            {
                if (collider.GetComponent<ItemPickup>() != null)
                    return true;
            }
            return false;
        }
        
        public void ClearAllItems()
        {
            foreach (var pickup in activeItemPickups)
            {
                if (pickup != null)
                    Destroy(pickup.gameObject);
            }
            activeItemPickups.Clear();
        }
        
        public ItemBase GetRandomItem()
        {
            if (availableItems.Length == 0)
                return null;
                
            return availableItems[Random.Range(0, availableItems.Length)];
        }
        
        public ItemBase GetItemByType(ItemType type)
        {
            foreach (var item in availableItems)
            {
                if (item.ItemType == type)
                    return item;
            }
            return null;
        }
    }
}