using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPGGame.Data
{
    public enum ItemTier
    {
        Common,    // White - T1
        Uncommon,  // Blue - T2
        Rare,      // Yellow - T3
        Elite,     // Orange - T4
        Legendary, // Red - T5
        Unique     // Gold - T6 - Only through specific loot or crafting
    }

    public enum ItemCategory
    {
        // Active Equipment (Modifiable)
        Sword, Mace, Bow, Staff, Dagger, Shield,
        
        // Passive Equipment (Unmodifiable)
        Armor, Helmet, Pants, Shirt, Shoes, 
        Ring, Necklace,
        
        // Consumables and Materials
        Consumable, Gem, Material
    }

    public enum ItemType
    {
        Consumable,     // Single-use items
        ActiveTool,     // Weapons/tools that can be modified by gems
        PassiveGear,    // Equipment that provides passive effects (unmodifiable)
        Modifier        // Gems and enhancers that affect adjacent items
    }

    public enum StatType
    {
        MaxHP, MaxMP, Speed, Luck,
        PhysicalAttack, PhysicalDefense,
        SpecialAttack, SpecialDefense,
        CriticalRate, CriticalDamage
    }

    public enum StatusEffectType
    {
        // Damage Over Time (decreases by 1 each turn)
        Poison, Burn, Bleed,
        
        // Defensive/Offensive States (decreases by 1 each turn)  
        ArmorPoints, Spikes,
        
        // Modified Freeze (decreases by 1 each turn)
        Freeze, // Now reduces speed AND increases damage taken
        
        // Vulnerability States
        Wet, Oiled, Charged, Brittle, Exposed,
        
        // Stat Modifications
        StrengthUp, StrengthDown, SpeedUp, SpeedDown,
        DefenseUp, DefenseDown, MagicUp, MagicDown,
        
        // Action Restrictions
        Sleep, Silence, Charm, Paralysis,
        Stun, // NEW: Forces character to skip next turn
        
        // Special States
        Regeneration, Shield, Invisibility,
        FireImmunity, IceImmunity, PoisonImmunity
    }

    public enum DamageType
    {
        Physical, Fire, Ice, Electric, Poison, 
        Spirit, Thunder, Water, Earth, Wind
    }

    public enum SkillTarget
    {
        Self, Ally, Enemy, AllAllies, AllEnemies, All
    }

    public enum SkillUsageContext
    {
        Combat, Menu, Both
    }
}