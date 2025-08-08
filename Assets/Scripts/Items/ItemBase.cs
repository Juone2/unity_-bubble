using UnityEngine;
using BubbleBattle.Player;

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
        
        public virtual void Use(PlayerController user)
        {
            Debug.Log($"{user.PlayerData.playerName} used {itemName}");
            ApplyEffect(user);
        }
        
        protected virtual void ApplyEffect(PlayerController user)
        {
            // Override in derived classes
        }
        
        public virtual ItemBase CreateInstance()
        {
            return Instantiate(this);
        }
        
        protected PlayerController GetOtherPlayer(PlayerController currentPlayer)
        {
            var allPlayers = FindObjectsOfType<PlayerController>();
            foreach (var player in allPlayers)
            {
                if (player != currentPlayer)
                    return player;
            }
            return null;
        }
    }
}