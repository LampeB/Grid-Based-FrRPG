using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPGGame.Data
{
    [System.Serializable]
    public class BaseStats
    {
        [Header("Health and Mana")]
        public int maxHP = 100;
        public int maxMP = 50;
        
        [Header("Core Stats")]
        public int speed = 100;
        public int luck = 10;
        
        [Header("Attack and Defense")]
        public int physicalAttack = 20;
        public int physicalDefense = 15;
        public int specialAttack = 15;
        public int specialDefense = 15;
        
        [Header("Critical Hit Stats")]
        [Range(0f, 1f)] public float criticalRate = 0.05f;    // 5% base crit rate
        public float criticalDamage = 1.5f;  // 150% damage on crit

        public BaseStats Clone()
        {
            return new BaseStats
            {
                maxHP = this.maxHP,
                maxMP = this.maxMP,
                speed = this.speed,
                luck = this.luck,
                physicalAttack = this.physicalAttack,
                physicalDefense = this.physicalDefense,
                specialAttack = this.specialAttack,
                specialDefense = this.specialDefense,
                criticalRate = this.criticalRate,
                criticalDamage = this.criticalDamage
            };
        }

        public int GetStat(StatType statType)
        {
            return statType switch
            {
                StatType.MaxHP => maxHP,
                StatType.MaxMP => maxMP,
                StatType.Speed => speed,
                StatType.Luck => luck,
                StatType.PhysicalAttack => physicalAttack,
                StatType.PhysicalDefense => physicalDefense,
                StatType.SpecialAttack => specialAttack,
                StatType.SpecialDefense => specialDefense,
                StatType.CriticalRate => Mathf.RoundToInt(criticalRate * 100),
                StatType.CriticalDamage => Mathf.RoundToInt(criticalDamage * 100),
                _ => 0
            };
        }

        public void SetStat(StatType statType, int value)
        {
            switch (statType)
            {
                case StatType.MaxHP: maxHP = value; break;
                case StatType.MaxMP: maxMP = value; break;
                case StatType.Speed: speed = value; break;
                case StatType.Luck: luck = value; break;
                case StatType.PhysicalAttack: physicalAttack = value; break;
                case StatType.PhysicalDefense: physicalDefense = value; break;
                case StatType.SpecialAttack: specialAttack = value; break;
                case StatType.SpecialDefense: specialDefense = value; break;
                case StatType.CriticalRate: criticalRate = value / 100f; break;
                case StatType.CriticalDamage: criticalDamage = value / 100f; break;
            }
        }
    }

    [System.Serializable]
    public class StatModifier
    {
        public StatType statType;
        public float value;           // For flat bonuses
        public float multiplier = 1.0f;      // For percentage bonuses (1.1 = +10%)
        public bool isPercentage = false;     // True for multiplier, false for flat value
        public string sourceItemId;   // Which item provides this modifier
        public bool appliesToSpecificItem = false; // True if this modifier only affects certain items
        public List<string> targetItemIds = new List<string>(); // Items this modifier affects (if specific)

        public StatModifier()
        {
            targetItemIds = new List<string>();
        }

        public StatModifier(StatType stat, float val, bool percentage = false)
        {
            statType = stat;
            value = val;
            isPercentage = percentage;
            multiplier = percentage ? val : 1.0f;
            targetItemIds = new List<string>();
        }
    }
}