using System.Collections.Generic;
using UnityEngine;

namespace JRPGGame.Data
{
    [System.Serializable]
    public class CharacterResistances
    {
        [Header("Damage Type Resistances")]
        [Range(0f, 1f)] public float physicalResistance = 0f;
        [Range(0f, 1f)] public float fireResistance = 0f;
        [Range(0f, 1f)] public float iceResistance = 0f;
        [Range(0f, 1f)] public float electricResistance = 0f;
        [Range(0f, 1f)] public float poisonResistance = 0f;
        [Range(0f, 1f)] public float spiritResistance = 0f;

        public float GetResistance(DamageType damageType)
        {
            return damageType switch
            {
                DamageType.Physical => physicalResistance,
                DamageType.Fire => fireResistance,
                DamageType.Ice => iceResistance,
                DamageType.Electric => electricResistance,
                DamageType.Poison => poisonResistance,
                DamageType.Spirit => spiritResistance,
                _ => 0f
            };
        }
    }

    [System.Serializable]
    public class StatusEffect
    {
        public StatusEffectType type;
        public string displayName;
        public string description;
        public int remainingTurns;
        public float intensity;
        public bool isBeneficial;
        public List<StatModifier> statModifiers = new List<StatModifier>();
        public string sourceId;
        
        public StatusEffect()
        {
            statModifiers = new List<StatModifier>();
        }
        
        public StatusEffect(StatusEffectType effectType, int duration, float intensityValue = 1.0f)
        {
            type = effectType;
            remainingTurns = duration;
            intensity = intensityValue;
            statModifiers = new List<StatModifier>();
            displayName = effectType.ToString();
            description = effectType + " effect";
        }
    }
}