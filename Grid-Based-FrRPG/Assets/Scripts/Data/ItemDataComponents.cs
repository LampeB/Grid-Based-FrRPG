using System.Collections.Generic;
using UnityEngine;

namespace JRPGGame.Data
{
    [System.Serializable]
    public class PassiveEffect
    {
        public string effectId;
        public string displayName;
        [TextArea(2, 3)]
        public string description;
        public List<StatModifier> statModifiers = new List<StatModifier>();
        public List<StatusEffectApplication> statusImmunities = new List<StatusEffectApplication>();
        
        public PassiveEffect()
        {
            statModifiers = new List<StatModifier>();
            statusImmunities = new List<StatusEffectApplication>();
        }
    }

    [System.Serializable]
    public class ItemSkill
    {
        public string skillId;
        public string displayName;
        [TextArea(2, 3)]
        public string description;
        public int mpCost = 0;
        public SkillTarget targetType = SkillTarget.Enemy;
        public SkillUsageContext usageContext = SkillUsageContext.Combat;
        public List<string> restrictedToCharacterTypes = new List<string>();
        public List<SkillEffect> effects = new List<SkillEffect>();
        public bool requiresCombat = true;
        
        public ItemSkill()
        {
            restrictedToCharacterTypes = new List<string>();
            effects = new List<SkillEffect>();
        }
    }

    [System.Serializable]
    public class SkillEffect
    {
        public string effectName;
        public DamageType damageType = DamageType.Physical;
        public float damageMultiplier = 1.0f;
        public int flatDamage = 0;
        public List<StatusEffectApplication> statusEffects = new List<StatusEffectApplication>();
        public List<StatModifier> statModifications = new List<StatModifier>();
        
        public SkillEffect()
        {
            statusEffects = new List<StatusEffectApplication>();
            statModifications = new List<StatModifier>();
        }
    }

    [System.Serializable]
    public class StatusEffectApplication
    {
        public StatusEffectType effectType;
        public int duration = 3;
        public float intensity = 1.0f;
        public float applicationChance = 1.0f;
        public bool canStack = false;
        public bool overwritesSameType = true;
    }

    [System.Serializable]
    public class ModifierPattern
    {
        [Header("Modifier Coverage")]
        public Vector2Int[] affectedCells = new Vector2Int[] { Vector2Int.zero };
        public bool affectsAdjacentItems = true;
        public int maxAffectedItems = -1; // -1 for unlimited
        
        [Header("Modifier Effects")]
        public List<StatModifier> modifierEffects = new List<StatModifier>();
        public string modifierDescription = "Affects adjacent items";
        
        public ModifierPattern()
        {
            modifierEffects = new List<StatModifier>();
        }
    }
}