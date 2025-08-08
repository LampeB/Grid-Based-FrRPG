using System.Collections.Generic;
using UnityEngine;

namespace JRPGGame.Data
{
    [System.Serializable]
    public class Character
    {
        [Header("Character Identity")]
        public string characterId;
        public string displayName;
        public string characterType;
        
        [Header("Core Stats")]
        public BaseStats baseStats;
        
        [Header("Current State")]
        public int currentHP;
        public int currentMP;
        
        [Header("Inventory System")]
        public GridInventory inventory;
        
        [Header("Resistances")]
        public CharacterResistances resistances;
        
        [Header("Status Effects")]
        public List<StatusEffect> activeStatusEffects = new List<StatusEffect>();
        
        [Header("Character Restrictions")]
        public List<ItemCategory> allowedItemCategories = new List<ItemCategory>();
        public List<string> itemSkillRestrictions = new List<string>(); 

        public Character()
        {
            Initialize();
        }

        public Character(string id, string name, string type)
        {
            characterId = id;
            displayName = name;
            characterType = type;
            Initialize();
        }

        private void Initialize()
        {
            if (baseStats == null) baseStats = new BaseStats();
            if (inventory == null) inventory = new GridInventory();
            if (resistances == null) resistances = new CharacterResistances();
            if (activeStatusEffects == null) activeStatusEffects = new List<StatusEffect>();
            if (allowedItemCategories == null) allowedItemCategories = new List<ItemCategory>();
            if (itemSkillRestrictions == null) itemSkillRestrictions = new List<string>();
            
            currentHP = baseStats.maxHP;
            currentMP = baseStats.maxMP;
        }

        public BaseStats CalculateFinalStats()
        {
            BaseStats finalStats = baseStats.Clone();
            
            foreach (PlacedItem placedItem in inventory.placedItems)
            {
                ApplyItemStats(finalStats, placedItem);
            }
            
            foreach (StatusEffect effect in activeStatusEffects)
            {
                ApplyStatusEffectStats(finalStats, effect);
            }
            
            return finalStats;
        }

        private void ApplyItemStats(BaseStats stats, PlacedItem placedItem)
        {
            ItemData item = placedItem.item;
            
            foreach (StatModifier modifier in item.statModifiers)
            {
                ApplyStatModifier(stats, modifier);
            }
            
            if (item.type == ItemType.PassiveGear)
            {
                foreach (PassiveEffect effect in item.passiveEffects)
                {
                    foreach (StatModifier modifier in effect.statModifiers)
                    {
                        ApplyStatModifier(stats, modifier);
                    }
                }
            }
            
            if (item.type == ItemType.ActiveTool)
            {
                foreach (ModifierApplication modApp in placedItem.appliedModifiers)
                {
                    if (modApp.modifierItem != null && modApp.modifierItem.modifierZones != null)
                    {
                        foreach (StatModifier modifier in modApp.modifierItem.modifierZones.modifierEffects)
                        {
                            ApplyStatModifier(stats, modifier);
                        }
                    }
                }
            }
        }

        private void ApplyStatModifier(BaseStats stats, StatModifier modifier)
        {
            int currentValue = stats.GetStat(modifier.statType);
            
            if (modifier.isPercentage)
            {
                int newValue = Mathf.RoundToInt(currentValue * modifier.multiplier);
                stats.SetStat(modifier.statType, newValue);
            }
            else
            {
                int newValue = currentValue + Mathf.RoundToInt(modifier.value);
                stats.SetStat(modifier.statType, newValue);
            }
        }

        private void ApplyStatusEffectStats(BaseStats stats, StatusEffect effect)
        {
            foreach (StatModifier modifier in effect.statModifiers)
            {
                ApplyStatModifier(stats, modifier);
            }
        }

        public int GetCurrentSpeed()
        {
            return CalculateFinalStats().speed;
        }

        public bool CanUseItem(ItemData item)
        {
            if (!item.CanCharacterUse(characterType))
                return false;
            
            if (allowedItemCategories.Count > 0 && !allowedItemCategories.Contains(item.category))
                return false;
            
            return true;
        }

        public bool IsDefeated()
        {
            return currentHP <= 0;
        }
    }
}