using UnityEngine;
using System.Linq;

namespace BubbleBattle.Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Bubble Battle/Items/Base Item")]
    public class ItemBase : ScriptableObject
    {
        [Header("Item Info")]
        [SerializeField] private string itemName;
        [SerializeField] private string description;
        [SerializeField] private Sprite itemIcon;
        [SerializeField] private ItemType itemType;
        [SerializeField] private ItemRarity rarity;
        
        [Header("Item Properties")]
        [SerializeField] private float cooldownTime = 1f;
        [SerializeField] private float duration = 5f;
        [SerializeField] private int uses = 1;
        [SerializeField] private bool affectsUser = true;
        [SerializeField] private bool affectsTarget = false;
        
        public string ItemName => itemName;
        public string Description => description;
        public Sprite ItemIcon => itemIcon;
        public ItemType ItemType => itemType;
        public ItemRarity Rarity => rarity;
        public float CooldownTime => cooldownTime;
        public float Duration => duration;
        public int Uses => uses;
        public bool AffectsUser => affectsUser;
        public bool AffectsTarget => affectsTarget;
        
        public virtual void Use(Component user)
        {
            Debug.Log($"{user.name} used {itemName}");
            ApplyEffect(user);
        }
        
        protected virtual void ApplyEffect(Component user)
        {
            // Override in derived classes
        }
        
        // Helper method to start coroutines from the user object
        protected void StartEffectCoroutine(Component user, System.Collections.IEnumerator routine)
        {
            var monoBehaviour = user as MonoBehaviour;
            if (monoBehaviour != null)
            {
                monoBehaviour.StartCoroutine(routine);
            }
        }
        
        public virtual ItemBase CreateInstance()
        {
            return Instantiate(this);
        }
        
        protected Component GetOtherPlayer(Component currentPlayer)
        {
            var allPlayers = FindObjectsOfType<MonoBehaviour>().Where(mb => mb.GetType().Name == "PlayerController");
            foreach (var player in allPlayers)
            {
                if (player != currentPlayer)
                    return player;
            }
            return null;
        }
    }
}