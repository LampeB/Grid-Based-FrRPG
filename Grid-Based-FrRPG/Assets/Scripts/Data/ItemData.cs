using System.Collections.Generic;
using UnityEngine;

namespace JRPGGame.Data
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Game/Item")]
    public class ItemData : ScriptableObject
    {
        [Header("Basic Information")]
        public string itemId;
        public string displayName;
        [TextArea(3, 5)]
        public string description;
        public Sprite icon;
        public ItemTier tier = ItemTier.Common;
        
        [Header("Item Classification")]
        public ItemCategory category;
        public ItemType type;
        
        [Header("Grid Properties")]
        public ItemShape shape;
        public bool canRotate = true;
        
        [Header("Effects and Modifiers")]
        public List<StatModifier> statModifiers = new List<StatModifier>();
        public List<PassiveEffect> passiveEffects = new List<PassiveEffect>();
        public List<ItemSkill> itemSkills = new List<ItemSkill>();
        
        [Header("Modifier Properties")]
        public ModifierPattern modifierZones;
        
        [Header("Character Restrictions")]
        public List<string> allowedCharacterTypes = new List<string>();
        public List<string> restrictedCharacterTypes = new List<string>();
        
        [Header("Economic Properties")]
        public int basePrice = 10;
        public bool canBeSold = true;
        public bool canBeUpgraded = true;
        
        [Header("World Representation")]
        public GameObject worldModel;
        public Color tierColor = Color.white;

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(itemId))
            {
                itemId = name.Replace(" ", "").ToLower();
            }
            
            tierColor = GetTierColor(tier);
            
            if (shape == null)
            {
                shape = new ItemShape();
            }
        }

        public static Color GetTierColor(ItemTier tier)
        {
            return tier switch
            {
                ItemTier.Common => Color.white,
                ItemTier.Uncommon => new Color(0.3f, 0.7f, 1f),
                ItemTier.Rare => Color.yellow,
                ItemTier.Elite => new Color(1f, 0.5f, 0f),
                ItemTier.Legendary => Color.red,
                ItemTier.Unique => new Color(1f, 0.8f, 0f),
                _ => Color.white
            };
        }

        public bool CanCharacterUse(string characterType)
        {
            if (allowedCharacterTypes.Count == 0 && restrictedCharacterTypes.Count == 0)
                return true;

            if (restrictedCharacterTypes.Contains(characterType))
                return false;

            if (allowedCharacterTypes.Count > 0)
                return allowedCharacterTypes.Contains(characterType);

            return true;
        }
    }
}