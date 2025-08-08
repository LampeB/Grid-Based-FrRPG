# JRPG Game Design Document & Technical Architecture
## Proof of Concept (POC) - Gameplay Mechanics Focus

> **📋 POC Development Approach:**
> This design document focuses on creating a **Proof of Concept (POC)** to validate innovative gameplay mechanics using **Unity primitive objects** (cubes, spheres, capsules) for all visual assets. The emphasis is on **gameplay systems validation** rather than visual polish, allowing for rapid iteration and testing of complex mechanics like grid-based inventory, status effect interactions, and dynamic combat systems.
> 
> **🎯 Success Criteria:** The POC is successful when all designed gameplay systems are functional and engaging using primitive art assets. Visual improvement can be implemented later without changing the core architecture.

## Table of Contents
1. [Game Overview](#game-overview)
2. [Core Systems Overview](#core-systems-overview)
3. [Technical Architecture](#technical-architecture)
4. [Data Structures](#data-structures)
5. [System Implementation Details](#system-implementation-details)
6. [Content Creation Workflows](#content-creation-workflows)
7. [User Interface & Accessibility](#user-interface--accessibility)
9. [Cross-System Communication](#cross-system-communication)
10. [Settings & Options System](#settings--options-system)
11. [Unity Implementation Patterns](#unity-implementation-patterns)
12. [Developer Tools & Debugging](#developer-tools--debugging)
13. [Asset Pipeline & Organization](#asset-pipeline--organization)
14. [Audio & Visual Systems](#audio--visual-systems)
15. [Balance & Testing Framework](#balance--testing-framework)
16. [Quality of Life Features](#quality-of-life-features)
17. [Error Handling & Edge Cases](#error-handling--edge-cases)
18. [Development Phases](#development-phases)
19. [Performance Considerations](#performance-considerations)

---

## Game Overview

### Core Concept
A **Proof of Concept (POC) JRPG** focused on testing innovative gameplay mechanics, inspired by Final Fantasy VI and Dragon Quest, featuring:
- **Exploration:** 3D overworld and areas with top-down/third-person camera transitions
- **Combat:** Turn-based battles with speed-based turn order and dynamic camera movement  
- **Character Building:** Grid-based inventory system with item combinations and equipment-only progression
- **Story:** Linear narrative structure with meaningful side quests
- **Visual Style:** Unity primitives (cubes, spheres, capsules) for rapid prototyping and gameplay testing

### Target Platform
- **Primary Focus:** Proof of Concept for gameplay mechanics validation
- **Platform:** PC development with Steam Deck compatibility in mind
- **Visual Approach:** Functional primitives over art polish - gameplay mechanics first

### Technical Scope
- Unity 3D engine with primitive-based rendering
- C# scripting with modular, data-driven architecture
- 3D environments using Unity primitives (cubes, planes, spheres)
- 3D character models using capsules and basic shapes
- 2D UI overlays for inventory and interface systems
- Multiple save system and dynamic music integration

### POC Success Criteria
The POC is successful when players can experience and validate:
- Complex grid inventory with shaped items and modifier interactions
- Dynamic turn-based combat with status effect combinations
- Equipment-only character progression with meaningful choices
- Smooth camera transitions between exploration and combat modes
- Persistent save/load system maintaining all game states

---

## Core Systems Overview

### 1. World Navigation System
- **Overworld Map:** Top-down view showing entire world with visible but inaccessible areas
- **Transportation:** Progressive unlocks (walking → boat → airship, etc.)
- **Locations:** Fixed explorable areas accessible from overworld
- **Multiple Worlds:** Support for 2-3 different overworld maps (dimensions)

### 2. Combat System
- **Turn Order:** Speed-based initiative with dynamic recalculation when speed changes
- **Actions:** Attack, Magic (MP cost), Items, Defend, Flee
- **Encounters:** Visible enemies on overworld, fixed compositions
- **Camera:** Dynamic movement during turns and attacks
- **Status Integration:** Status effects can modify turn order mid-combat

### 3. Inventory & Equipment System
- **Character Grids:** Unique shaped inventories per character (upgradeable)
- **Item Shapes:** Tetris-like pieces with rotation support
- **Modifiers:** Gems and enhancers that affect adjacent items
- **Party Storage:** Infinite list-based storage accessible at specific locations
- **Item Tiers:** 6-tier rarity system with color coding and stat scaling
- **Upgrade System:** Combine identical items to create higher tier versions
- **Crafting System:** Blueprint-based creation of unique items and combinations
- **Preview System:** Live stat calculations, modifier highlighting, tabbed information display
- **Environmental Usage:** Items enable exploration mechanics (keys, torches, ice creation)

### 4. Character Progression System
- **Stats:** HP, MP, Speed, Luck, Physical/Special Attack/Defense, Critical Rate, Critical Damage
- **Resistances:** Type-based damage reduction (Fire, Ice, Thunder, etc.)
- **Equipment Progression:** All character development through inventory items
- **Passive Equipment:** Unmodifiable items providing permanent effects (armor, accessories)
- **Active Equipment:** Modifiable weapons and tools affected by gems and enhancers
- **Critical Hit System:** Deterministic damage with item-based crit rate and damage modifiers

### 5. Loot & Economy System
- **Sources:** NPCs, Chests, Combat rewards, Shops
- **Combat Loot:** Choose up to 2 from 0-5 items with flexible selection until confirmation
- **Shop System:** Purchase consumables, basic equipment, and common-rare blueprints
- **Reorganization:** Full inventory management during loot phases
- **Economy:** Gold-based progression for grid upgrades, shop purchases, and blueprint acquisition

---

## Technical Architecture

### Core Managers (Singleton Pattern)
```
GameManager
├── SaveSystem
├── SceneManager
├── AudioManager
├── UIManager
└── InputManager
```

### World Systems
```
OverworldManager
├── Multiple world map support
├── Transportation state tracking
├── Location accessibility logic
└── Player position persistence

LocationManager
├── Scene loading for explorable areas
├── Encounter spawn management
├── NPC and chest placement
└── Exit/entrance connections

TransportationSystem
├── Available transport methods
├── Path accessibility calculation
├── Movement mode switching
└── Story flag integration
```

### Character & Combat Systems
```
PartyManager
├── Active party composition
├── Character availability tracking
├── Party-wide state management
└── Inn/composition change handling

CharacterSystem
├── Base stats and calculations
├── Equipment stat modifications
├── Passive equipment effect application
├── Resistance calculations
└── Character-item compatibility checks

CombatManager
├── Turn order calculation
├── Action resolution
├── Damage calculations
├── Item skill execution
└── Victory/defeat handling

InventorySystem
├── Grid-based placement logic
├── Item rotation and validation
├── Modifier effect calculations
├── Drag-and-drop interface
└── Item tier visual management

CraftingSystem
├── Blueprint database management
├── Recipe validation and execution
├── Item upgrade combinations
└── Crafting UI integration

ItemSkillSystem
├── Skill availability calculation
├── Character restriction validation
├── MP cost and effect resolution
└── Combat and menu skill execution
```

### Data Management
```
ItemDatabase
├── All item definitions
├── Shape and modifier data
├── Loot pool configurations
└── Scriptable Object architecture

ShopSystem
├── Shop inventory management
├── Dynamic pricing calculation
├── Blueprint rarity distribution
└── Location-based shop availability

StoryFlagManager
├── Quest completion tracking
├── Area unlock conditions
├── NPC availability states
├── Shop unlock progression
└── Transportation prerequisites

SaveSystem
├── Multiple save slots
├── Automatic save naming
├── Complete game state serialization
├── Shop inventory persistence
└── Save file management
```

---

## Data Structures

### Core Data Classes

#### Character Data
```csharp
[System.Serializable]
public class Character
{
    public string id;
    public string displayName;
    public string characterType; // "Berserker", "Healer", "Mage", etc.
    public BaseStats baseStats;
    public GridInventory inventory;
    public List<StatusEffect> activeEffects;
    public CharacterResistances resistances;
    public List<ItemCategory> allowedItemCategories;
    public List<string> itemSkillRestrictions; // Skills this character can't use
}

[System.Serializable]
public class BaseStats
{
    public int maxHP;
    public int maxMP;
    public int speed;
    public int luck;
    public int physicalAttack;
    public int physicalDefense;
    public int specialAttack;
    public int specialDefense;
    
    [Header("Critical Hit Stats")]
    public float criticalRate;    // 0.0 to 1.0 (0% to 100%)
    public float criticalDamage;  // Multiplier (1.5 = 150% damage on crit)
}

public enum StatType
{
    MaxHP, MaxMP, Speed, Luck,
    PhysicalAttack, PhysicalDefense,
    SpecialAttack, SpecialDefense,
    CriticalRate, CriticalDamage
}

[System.Serializable]
public class StatModifier
{
    public StatType statType;
    public float value;           // For flat bonuses
    public float multiplier;      // For percentage bonuses (1.1 = +10%)
    public bool isPercentage;     // True for multiplier, false for flat value
    public string sourceItemId;   // Which item provides this modifier
    public bool appliesToSpecificItem; // True if this modifier only affects certain items
    public List<string> targetItemIds; // Items this modifier affects (if specific)
}
```

#### Item System
```csharp
public enum ItemTier
{
    Common,    // White
    Uncommon,  // Blue
    Rare,      // Yellow
    Elite,     // Orange
    Legendary, // Red
    Unique     // Gold - Only through specific loot or crafting
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
    ActiveTool,     // Weapons/tools that can be modified
    PassiveGear,    // Equipment that provides passive effects
    Modifier        // Gems and enhancers
}

[CreateAssetMenu(fileName = "New Item", menuName = "Game/Item")]
public class Item : ScriptableObject
{
    public string itemId;
    public string displayName;
    public ItemType type; // Consumable, ActiveTool, PassiveGear, Modifier
    public ItemCategory category;
    public ItemTier tier;
    public ItemShape shape;
    public List<ItemEffect> effects;
    public List<StatModifier> statModifiers;
    public List<PassiveEffect> passiveEffects; // For PassiveGear items
    public List<ItemSkill> itemSkills; // Available from Uncommon+
    public List<string> allowedCharacterTypes; // Restriction system
    public ModifierPattern modifierZones; // For modifier items only
    public bool canBeModified; // False for PassiveGear, True for ActiveTools
    public Color tierColor; // Auto-assigned based on tier
    public Sprite icon;
    public GameObject worldModel;
    public bool canBeUpgraded;
    public Item upgradeResult; // What this becomes when upgraded
}

[System.Serializable]
public class PassiveEffect
{
    public string effectId;
    public string displayName;
    public PassiveEffectType type; // StatBonus, Resistance, SpecialAbility
    public List<StatModifier> statModifiers;
    public List<ResistanceModifier> resistanceModifiers;
    public string description;
}

[System.Serializable]
public class ItemSkill
{
    public string skillId;
    public string displayName;
    public string description;
    public int mpCost;
    public SkillTarget targetType; // Self, Ally, Enemy, All
    public SkillUsageContext usageContext; // Combat, Menu, Both
    public List<string> restrictedToCharacterTypes;
    public List<SkillEffect> effects;
    public bool requiresCombat;
}

[CreateAssetMenu(fileName = "New Blueprint", menuName = "Game/Crafting Blueprint")]
public class CraftingBlueprint : ScriptableObject
{
    public string blueprintId;
    public string displayName;
    public Item resultItem;
    public List<CraftingIngredient> requiredIngredients;
    public bool isUpgradeRecipe; // True for tier upgrades
    public string description;
}

[System.Serializable]
public class CraftingIngredient
{
    public Item requiredItem;
    public ItemTier minimumTier;
    public int quantity;
    public bool exactTierRequired; // For upgrade recipes
}
```

[System.Serializable]
public class ItemShape
{
    public Vector2Int[] occupiedCells;
    public Vector2Int pivotPoint;
    public int rotationStates; // 1, 2, or 4 possible rotations
}
```

#### Interactive Status Effects System
```csharp
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

public enum StatusCategory
{
    DamageOverTime,     // Ongoing damage effects
    Vulnerability,      // Makes target susceptible to certain damage types
    StatModification,   // Changes character stats
    ActionRestriction,  // Limits available actions
    Protection,         // Defensive or beneficial effects
    Immunity           // Prevents certain effects
}

public enum DamageType
{
    Physical, Fire, Ice, Electric, Poison, 
    Spirit, Thunder, Water, Earth, Wind
}

[System.Serializable]
public class StatusEffect
{
    public StatusEffectType type;
    public StatusCategory category;
    public string displayName;
    public string description;
    public Sprite icon;
    public StatusDuration durationType;
    public int remainingTurns;
    public float intensity;
    public bool isBeneficial;
    public List<StatModifier> statModifiers;
    public List<StatusInteraction> interactions; // How this status interacts with others
    public List<DamageVulnerability> vulnerabilities; // Damage type modifications
    public string sourceId;
}

[System.Serializable]
public class StatusInteraction
{
    public StatusEffectType triggerStatus;     // Status that triggers this interaction
    public InteractionType interactionType;   // What happens when they meet
    public StatusEffectType resultStatus;     // Optional: new status to apply
    public float damageMultiplier;            // Damage modification
    public float applicationChance;           // Chance for special effects
    public bool removesTriggerStatus;         // Does interaction consume the trigger?
    public bool removesThisStatus;           // Does interaction consume this status?
}

public enum InteractionType
{
    Cancel,           // One status removes the other (freeze cancels burn)
    Amplify,          // One status makes damage stronger (oil + fire)
    Transform,        // Both statuses become something new
    Trigger,          // One status triggers an immediate effect
    Immunity         // One status prevents the other
}

[System.Serializable]
public class DamageVulnerability
{
    public DamageType damageType;
    public float damageMultiplier;     // 2.0 = double damage
    public StatusEffectType guaranteedEffect; // Effect applied on hit
    public float guaranteedChance;     // Chance for guaranteed effect (usually 1.0)
}

[System.Serializable]
public class StatusEffectApplication
{
    public StatusEffectType effectType;
    public int duration;
    public float intensity;
    public float applicationChance;
    public bool canStack;
    public bool overwritesSameType;
    public DamageType triggerDamageType; // Optional: only applies with certain damage
}
```

#### Environmental Interaction System
```csharp
public enum EnvironmentalTrigger
{
    HasSpecificItem,     // Player has key, torch, etc.
    HasItemCategory,     // Any fire gem, ice gem, etc.
    HasItemWithEffect,   // Any item that provides fire damage
    CharacterStat,       // Character has enough strength, etc.
    StoryFlag           // Story progression requirement
}

[System.Serializable]
public class EnvironmentalInteraction
{
    public string interactionId;
    public string displayPrompt; // "Light torch", "Freeze pond", "Unlock door"
    public EnvironmentalTrigger triggerType;
    public string requiredItemId;
    public ItemCategory requiredCategory;
    public string requiredEffect;
    public int requiredStatValue;
    public string requiredFlag;
    public GameObject resultPrefab; // What changes in the world
    public bool consumesItem; // Does using the key destroy it?
}
```csharp
[System.Serializable]
public class GridInventory
{
    public Vector2Int gridSize;
    public List<Vector2Int> availableCells;
    public List<PlacedItem> placedItems;
    
    public bool CanPlaceItem(Item item, Vector2Int position, int rotation);
    public bool PlaceItem(Item item, Vector2Int position, int rotation);
    public void RemoveItem(PlacedItem item);
    public List<ItemEffect> GetActiveEffects();
    public List<PassiveEffect> GetPassiveEffects(); // From PassiveGear
    public List<StatModifier> GetTotalStatModifiers(); // Combined active + passive
}

[System.Serializable]
public class PlacedItem
{
    public Item item;
    public Vector2Int position;
    public int rotation;
    public List<Vector2Int> occupiedCells;
    public List<ModifierApplication> appliedModifiers; // Only for ActiveTools
}

[System.Serializable]
public class ModifierApplication
{
    public Item modifierItem; // The gem/enhancer providing the effect
    public Vector2Int modifierPosition;
    public List<ItemEffect> appliedEffects;
}
```

### Save Data Structure
```csharp
[System.Serializable]
public class GameSaveData
{
    public string saveName;
    public DateTime saveTime;
    public string currentScene;
    public Vector3 playerPosition;
    public List<Character> partyMembers;
    public List<Character> availableCharacters;
    public Dictionary<string, bool> storyFlags;
    public List<string> unlockedTransportation;
    public PartyStorage partyStorage;
    public Dictionary<string, bool> visitedLocations;
    public Dictionary<string, ShopInventoryState> shopStates;
    public int playerGold;
    public float playtime;
}
```

### Shop System Data
```csharp
public enum ShopType
{
    GeneralGoods,    // Potions, basic consumables
    Equipment,       // Weapons, armor, accessories
    Blueprints,      // Common to Rare crafting recipes
    GridUpgrades,    // Inventory expansion
    Special          // Unique/story-specific shops
}

[CreateAssetMenu(fileName = "New Shop", menuName = "Game/Shop")]
public class Shop : ScriptableObject
{
    public string shopId;
    public string shopName;
    public ShopType shopType;
    public string shopkeeperName;
    public List<ShopItem> inventory;
    public List<string> requiredStoryFlags; // Unlock conditions
    public bool restocksItems;
    public float restockInterval; // In-game days
    public int gridUpgradeBaseCost;
    public float gridUpgradeCostMultiplier;
}

[System.Serializable]
public class ShopItem
{
    public Item item;
    public int basePrice;
    public PricingType pricingType; // Fixed, TierBased, Dynamic
    public int maxStock; // -1 for infinite
    public List<string> requiredFlags; // Item-specific unlock conditions
    public bool restockable;
}

[System.Serializable]
public class ShopInventoryState
{
    public string shopId;
    public Dictionary<string, int> currentStock; // itemId -> quantity
    public DateTime lastRestock;
    public List<string> purchasedBlueprints; // Track one-time purchases
}

public enum PricingType
{
    Fixed,        // Set price never changes
    TierBased,    // Price based on item tier
    Dynamic,      // Price varies based on story progression
    Negotiable    // Future: haggling system
}

public enum BlueprintRarity
{
    Purchasable,  // Common to Rare - available in shops
    Findable,     // Elite+ - found in world/combat/quests only
    Unique        // Story-specific acquisition
}
```

## Equipment-Focused Design Philosophy

### Core Benefits:
- **Unified Progression:** All character development flows through inventory management
- **Meaningful Choices:** Every grid slot matters for both storage and power
- **Clear Distinctions:** ActiveTools can be enhanced, PassiveGear provides reliable effects
- **Strategic Depth:** Grid shapes force players to prioritize what equipment to carry
- **Immediate Feedback:** Equipment changes instantly affect character capabilities

---

## System Implementation Details

### 2. Grid Inventory & Modifier System

#### Key Classes:
- **GridInventoryUI:** Handles drag-and-drop interface
- **InventoryValidator:** Checks placement legality
- **ModifierCalculator:** Applies item combination effects (ActiveTools only)

#### Core Logic:
```csharp
public bool ValidatePlacement(Item item, Vector2Int position, int rotation)
{
    // Get rotated shape
    Vector2Int[] rotatedShape = RotateShape(item.shape.occupiedCells, rotation);
    
    // Check bounds
    foreach (Vector2Int cell in rotatedShape)
    {
        Vector2Int finalPos = position + cell;
        if (!IsValidCell(finalPos)) return false;
        if (IsCellOccupied(finalPos)) return false;
    }
    
    return true;
}

public void CalculateModifierEffects(PlacedItem activeItem)
{
    // Only ActiveTools can be modified
    if (activeItem.item.type != ItemType.ActiveTool) return;
    
    activeItem.appliedModifiers.Clear();
    
    // Find all modifier items affecting this active tool
    foreach (PlacedItem modifier in placedItems)
    {
        if (modifier.item.type == ItemType.Modifier)
        {
            if (ModifierAffectsItem(modifier, activeItem))
            {
                ApplyModifierToItem(modifier, activeItem);
            }
        }
#### Strategic Implications of Dynamic Turn Order:

**Speed Buff Tactics:**
- Apply speed buffs to allies before big attacks to potentially act again sooner
- Very fast characters (2x+ base speed) get multiple actions per round
- Speed buffs can "steal turns" by moving characters ahead in the queue

**Speed Debuff Control:**
- Slow down enemy spellcasters to delay their dangerous abilities
- Speed debuffs can push enemies later in the turn order
- Strategic timing: apply speed debuffs right before enemy turns for maximum delay

**Turn Order Manipulation:**
- Players can see upcoming turn order and plan speed modifications accordingly
- Equipment swaps during combat can affect speed (if allowed by game rules)
- Status effect timing becomes crucial - when to apply buffs/debuffs for optimal positioning

**Example Combat Scenarios:**

**Scenario 1 - Speed Buff Combo:**
1. Turn 1: Support character casts "Haste" on damage dealer (+50 speed)
2. Turn order recalculates: Damage dealer moves from position 4 to position 2
3. Damage dealer gets their turn much sooner than expected
4. Enemy strategy disrupted by unexpected turn order change

**Scenario 2 - Speed Control:**
1. Enemy mage is about to cast powerful spell (next in turn queue)
2. Player casts "Slow" on mage (-30 speed)
3. Turn order recalculates: Mage drops to last position
4. Player team gets multiple actions before mage can complete their spell

**Scenario 3 - Multi-Action Speed Demon:**
1. Character has base speed 120 (above 2x threshold of 100)
2. Gets 2 actions per round automatically
3. Speed buff increases speed to 180
4. Now gets 3 actions per round - massive advantage

#### Visual Feedback Systems:
- **Turn Queue UI:** Shows next 5-6 upcoming turns with character portraits
- **Speed Change Indicators:** Flash/glow effects when turn order changes
- **Recalculation Notification:** Brief message "Turn order updated!" when speed changes
- **Multiple Action Indicators:** Special markers for characters with multiple actions per round
```

#### Implementation:
- **Base Stats:** Defined per character type, never change
- **Equipment Stats:** All stat modifications come from inventory items
- **Active Tools:** Weapons and tools that can be enhanced by modifier items
- **Passive Gear:** Armor and accessories providing effects regardless of grid position

```csharp
public class CharacterStatsCalculator
{
    public CharacterStats CalculateFinalStats(Character character)
    {
        CharacterStats finalStats = character.baseStats.Clone();
        
        // Apply passive gear effects (unmodifiable)
        foreach (PlacedItem item in character.inventory.placedItems)
        {
            if (item.item.type == ItemType.PassiveGear)
            {
                ApplyPassiveEffects(finalStats, item.item.passiveEffects);
            }
            else if (item.item.type == ItemType.ActiveTool)
            {
                // Apply base tool effects + any modifier enhancements
                ApplyActiveToolEffects(finalStats, item);
            }
        }
        
        return finalStats;
    }
    
    private void ApplyPassiveEffects(CharacterStats stats, List<PassiveEffect> effects)
    {
        // PassiveGear effects are always active regardless of grid position
        foreach (PassiveEffect effect in effects)
        {
            stats.ApplyStatModifiers(effect.statModifiers);
            stats.ApplyResistanceModifiers(effect.resistanceModifiers);
#### New Tactical Mechanics Examples:

**Armor Points System:**
- **Shield Usage:** Equipping shield grants 15 armor points that absorb damage until next turn
- **Layered Defense:** Multiple armor sources can stack (shield + armor spell)
- **Smart Timing:** Apply armor right before expected big attacks
- **Turn Management:** Armor refreshes each turn, encouraging active shield usage

**Spikes Reflection:**
- **Defensive Strategy:** Character with 10 spikes reflects 10 damage to each attacker
- **Multi-Hit Punishment:** Each individual attack triggers spikes separately
- **Strategic Positioning:** Place spikes character where enemies will focus fire
- **Decay Balance:** Spikes decrease by 1 each turn, preventing permanent advantage

**Enhanced Freeze Effect:**
- **Speed Reduction:** -20 speed affects turn order positioning
- **Damage Vulnerability:** +50% physical damage taken
- **Turn Decay:** Effect intensity decreases by 1 each turn
- **Strategic Application:** Use freeze to slow down fast enemies and make them more vulnerable

**Stun Mechanics:**
- **Turn Skip:** Stunned characters lose their entire turn
- **One-Turn Duration:** Effect automatically ends after skipping one turn
- **Interrupt Potential:** Apply stun right before enemy's powerful ability
- **No Stacking:** Can't chain-stun the same target continuously

#### Combat Flow with New Mechanics:

**Example Combat Round:**
1. **Turn 1 - Ally Tank:** Uses shield, gains 15 armor points, casts spikes (10 damage reflection)
2. **Turn 2 - Enemy 1:** Attacks tank → hits for 8 damage, absorbed by armor, takes 10 spike reflection damage
3. **Turn 3 - Ally Mage:** Casts freeze on fast enemy → reduces enemy speed by 20, increases damage taken
4. **Turn 4 - Fast Enemy:** Speed reduced, moves later in turn order due to freeze
5. **Turn 5 - Ally DPS:** Attacks frozen enemy → deals +50% damage due to freeze vulnerability
6. **Next Round:** Tank's armor refreshes, all turn-decaying effects decrease by 1

**Strategic Considerations:**
- **Timing Armor:** Shield use right before big enemy attacks for maximum absorption
- **Spikes Placement:** Put spikes on characters who will be focused by multiple enemies
- **Freeze Control:** Use freeze to manipulate turn order and set up vulnerability combos
- **Stun Interrupts:** Save stun effects to interrupt critical enemy abilities
- **Effect Decay Management:** Plan around effects wearing off over time
}

#### Key Classes:
- **GridInventoryUI:** Handles drag-and-drop interface
- **InventoryValidator:** Checks placement legality
- **ModifierCalculator:** Applies item combination effects

#### Core Logic:
```csharp
public bool ValidatePlacement(Item item, Vector2Int position, int rotation)
{
    // Get rotated shape
    Vector2Int[] rotatedShape = RotateShape(item.shape.occupiedCells, rotation);
    
    // Check bounds
    foreach (Vector2Int cell in rotatedShape)
    {
        Vector2Int finalPos = position + cell;
        if (!IsValidCell(finalPos)) return false;
        if (IsCellOccupied(finalPos)) return false;
    }
    
    return true;
}
```

### 3. Dynamic Combat Turn Order System

#### Implementation Approach:
- **Initial Calculation:** Speed-based initiative at combat start
- **Dynamic Recalculation:** Update turn queue when speed stats change
- **Queue Preservation:** Maintain current turn position during recalculation
- **Visual Updates:** Turn queue UI updates immediately when order changes

```csharp
public class DynamicTurnOrderManager
{
    private List<CombatEntity> allCombatants;
    private Queue<CombatEntity> currentTurnQueue;
    private CombatEntity currentActor;
    private int currentRound;
    
    public void InitializeCombat(List<CombatEntity> combatants)
    {
        allCombatants = combatants.ToList();
        currentRound = 1;
        RecalculateTurnOrder();
    }
    
    public void RecalculateTurnOrder()
    {
        // Get all active (non-defeated) combatants
        List<CombatEntity> activeCombatants = allCombatants
            .Where(c => !c.IsDefeated())
            .ToList();
        
        // Sort by current speed (including all modifiers)
        activeCombatants.Sort((a, b) => b.GetCurrentSpeed().CompareTo(a.GetCurrentSpeed()));
        
        // Handle multiple entries for very fast characters
        List<CombatEntity> newQueue = new List<CombatEntity>();
        foreach (CombatEntity combatant in activeCombatants)
        {
            int actions = CalculateActionsPerRound(combatant);
            for (int i = 0; i < actions; i++)
            {
                newQueue.Add(combatant);
            }
        }
        
        // Preserve current actor if mid-turn
        if (currentActor != null && !currentActor.IsDefeated())
        {
            // Current actor keeps their turn, but future turns are recalculated
            List<CombatEntity> futureQueue = newQueue.Where(c => c != currentActor).ToList();
            currentTurnQueue = new Queue<CombatEntity>(futureQueue);
        }
        else
        {
            currentTurnQueue = new Queue<CombatEntity>(newQueue);
        }
        
        UpdateTurnOrderUI();
    }
    
    public void OnSpeedChanged(CombatEntity entity)
    {
        // Speed changed due to status effect, equipment change, etc.
        RecalculateTurnOrder();
        
        // Log for feedback
        Debug.Log($"{entity.name}'s speed changed. Turn order recalculated.");
    }
    
    public CombatEntity GetNextActor()
    {
        if (currentTurnQueue.Count == 0)
        {
            // Round ended, start new round
            currentRound++;
            RecalculateTurnOrder();
        }
        
        if (currentTurnQueue.Count > 0)
        {
            currentActor = currentTurnQueue.Dequeue();
            
            // Skip defeated characters
            while (currentActor.IsDefeated() && currentTurnQueue.Count > 0)
            {
                currentActor = currentTurnQueue.Dequeue();
            }
            
            return currentActor.IsDefeated() ? null : currentActor;
        }
        
        return null;
    }
    
    private int CalculateActionsPerRound(CombatEntity entity)
    {
        int speed = entity.GetCurrentSpeed();
        int baseSpeed = 100; // Average speed reference point
        
        // Very fast characters get multiple actions per round
        if (speed >= baseSpeed * 2) return 2;
        if (speed >= baseSpeed * 3) return 3;
        return 1;
    }
    
    public List<CombatEntity> GetUpcomingTurnOrder(int lookahead = 5)
    {
        // For UI display - show next few actors in queue
        return currentTurnQueue.Take(lookahead).ToList();
/// Example Critical Hit Item Definitions:
public static class CriticalHitItemExamples
{
    public static ItemData CreateCriticalRateGem()
    {
        return new ItemData
        {
            itemId = "gem_precision",
            displayName = "Precision Gem",
            description = "Increases critical hit rate for the character.",
            type = ItemType.Modifier,
            tier = ItemTier.Uncommon,
            statModifiers = new List<StatModifier>
            {
                new StatModifier
                {
                    statType = StatType.CriticalRate,
                    value = 0.05f, // +5% crit rate
                    isPercentage = false,
                    appliesToSpecificItem = false // Affects all attacks
                }
            }
        };
    }
    
    public static ItemData CreateWeaponSpecificCritGem()
    {
        return new ItemData
        {
            itemId = "gem_sword_mastery",
            displayName = "Sword Mastery Gem",
            description = "Increases critical hit rate and damage when using swords.",
            type = ItemType.Modifier,
            tier = ItemTier.Rare,
            statModifiers = new List<StatModifier>
            {
                new StatModifier
                {
                    statType = StatType.CriticalRate,
                    value = 0.10f, // +10% crit rate for swords
                    isPercentage = false,
                    appliesToSpecificItem = true,
                    targetItemIds = new List<string> { "sword_iron", "sword_steel", "sword_mithril" }
                },
                new StatModifier
                {
                    statType = StatType.CriticalDamage,
                    value = 0.25f, // +25% crit damage for swords
                    isPercentage = false,
                    appliesToSpecificItem = true,
                    targetItemIds = new List<string> { "sword_iron", "sword_steel", "sword_mithril" }
                }
            }
        };
    }
    
    public static ItemData CreateHighCritWeapon()
    {
        return new ItemData
        {
            itemId = "dagger_assassin",
            displayName = "Assassin's Dagger",
            description = "A lightweight blade designed for precision strikes.",
            type = ItemType.ActiveTool,
            category = ItemCategory.Dagger,
            tier = ItemTier.Elite,
            statModifiers = new List<StatModifier>
            {
                new StatModifier
                {
                    statType = StatType.PhysicalAttack,
                    value = 25f,
                    isPercentage = false
                },
                new StatModifier
                {
                    statType = StatType.CriticalRate,
                    value = 0.15f, // +15% inherent crit rate
                    isPercentage = false
                },
                new StatModifier
                {
                    statType = StatType.CriticalDamage,
                    value = 0.50f, // +50% crit damage multiplier
                    isPercentage = false
                }
            }
        };
    }
    
    public static ItemData CreateCritAccessory()
    {
        return new ItemData
        {
            itemId = "ring_fortune",
            displayName = "Ring of Fortune",
            description = "Increases luck and critical hit potential.",
            type = ItemType.PassiveGear,
            category = ItemCategory.Ring,
            tier = ItemTier.Legendary,
            statModifiers = new List<StatModifier>
            {
                new StatModifier
                {
                    statType = StatType.Luck,
                    value = 15f,
                    isPercentage = false
                },
                new StatModifier
                {
                    statType = StatType.CriticalRate,
                    multiplier = 1.20f, // 20% multiplicative increase
                    isPercentage = true
                },
                new StatModifier
                {
                    statType = StatType.CriticalDamage,
                    multiplier = 1.30f, // 30% multiplicative increase  
                    isPercentage = true
                }
            }
        };
    }
}
```

#### Critical Hit Balance Guidelines:
- **Base Critical Rate:** Most characters start with 5% base crit rate
- **Base Critical Damage:** Default 150% damage (1.5x multiplier)
- **Crit Rate Sources:**
  - Character base: 5%
  - Luck stat: Each 10 luck = +1% crit rate
  - Equipment bonuses: +5% to +15% per item
  - Weapon-specific gems: +10% to +20% for specific weapon types
  - Maximum crit rate: 95% (always 5% chance to not crit)

- **Crit Damage Sources:**
  - Character base: 150% (1.5x)
  - Equipment bonuses: +25% to +50% per item (1.75x to 2.0x)
  - Multiplicative bonuses: 10% to 30% increases (rare)
  - High-tier weapons: +100% crit damage possible (2.5x total)

#### Strategic Implications:
- **Weapon Specialization:** Gems that boost crit for specific weapon types encourage focused builds
- **Risk/Reward:** High crit builds sacrifice consistent damage for burst potential
- **Item Synergy:** Combining crit rate and crit damage items for maximum effectiveness
- **Character Roles:** Fast characters with high crit become glass cannon assassins
- **Deterministic Gameplay:** Same equipment setup always has same crit chance, no RNG frustration

public class CombatEntity : MonoBehaviour
{
    [SerializeField] private int baseSpeed;
    private List<StatusEffect> activeStatusEffects;
    private List<StatModifier> equipmentModifiers;
    
    public int GetCurrentSpeed()
    {
        int currentSpeed = baseSpeed;
        
        // Apply equipment modifiers
        foreach (StatModifier modifier in equipmentModifiers)
        {
            if (modifier.statType == StatType.Speed)
            {
                currentSpeed += modifier.value;
            }
        }
        
        // Apply status effect modifiers
        foreach (StatusEffect effect in activeStatusEffects)
        {
            foreach (StatModifier modifier in effect.statModifiers)
            {
                if (modifier.statType == StatType.Speed)
                {
                    currentSpeed += modifier.value;
                }
            }
        }
        
        return Math.Max(1, currentSpeed); // Minimum speed of 1
    }
    
    public void OnStatusEffectApplied(StatusEffect effect)
    {
        activeStatusEffects.Add(effect);
        
        // Check if this effect modifies speed
        bool affectsSpeed = effect.statModifiers.Any(m => m.statType == StatType.Speed);
        if (affectsSpeed)
        {
            CombatManager.Instance.turnOrderManager.OnSpeedChanged(this);
    }
}

### 4. Critical Hit System Implementation

#### Critical Hit Calculation:
```csharp
public class CriticalHitSystem : MonoBehaviour
{
    [System.Serializable]
    public struct CriticalHitResult
    {
        public bool isCritical;
        public float finalDamage;
        public float criticalRate;
        public float criticalDamage;
        public List<string> criticalSources; // For UI feedback
    }
    
    public CriticalHitResult CalculateCriticalHit(Character attacker, Item weapon, float baseDamage)
    {
        CriticalHitResult result = new CriticalHitResult
        {
            criticalSources = new List<string>()
        };
        
        // Calculate total critical rate
        float totalCritRate = CalculateTotalCriticalRate(attacker, weapon, result.criticalSources);
        
        // Determine if this attack is critical (deterministic based on luck + crit rate)
        result.isCritical = DetermineCriticalHit(attacker, totalCritRate);
        
        if (result.isCritical)
        {
            // Calculate critical damage multiplier
            float critDamageMultiplier = CalculateTotalCriticalDamage(attacker, weapon, result.criticalSources);
            result.finalDamage = baseDamage * critDamageMultiplier;
            result.criticalRate = totalCritRate;
            result.criticalDamage = critDamageMultiplier;
        }
        else
        {
            result.finalDamage = baseDamage;
            result.criticalRate = totalCritRate;
            result.criticalDamage = 1.0f;
        }
        
        return result;
    }
    
    private float CalculateTotalCriticalRate(Character attacker, Item weapon, List<string> sources)
    {
        float totalCritRate = attacker.baseStats.criticalRate;
        sources.Add($"Base: {totalCritRate:P1}");
        
        // Apply character-wide crit rate modifiers
        foreach (PlacedItem item in attacker.inventory.placedItems)
        {
            foreach (StatModifier modifier in item.item.statModifiers)
            {
                if (modifier.statType == StatType.CriticalRate && !modifier.appliesToSpecificItem)
                {
                    if (modifier.isPercentage)
                    {
                        totalCritRate *= modifier.multiplier;
                        sources.Add($"{item.item.displayName}: x{modifier.multiplier:F2}");
                    }
                    else
                    {
                        totalCritRate += modifier.value;
                        sources.Add($"{item.item.displayName}: +{modifier.value:P1}");
                    }
                }
            }
        }
        
        // Apply weapon-specific crit rate modifiers
        if (weapon != null)
        {
            foreach (PlacedItem item in attacker.inventory.placedItems)
            {
                foreach (StatModifier modifier in item.item.statModifiers)
                {
                    if (modifier.statType == StatType.CriticalRate && 
                        modifier.appliesToSpecificItem &&
                        modifier.targetItemIds.Contains(weapon.itemId))
                    {
                        if (modifier.isPercentage)
                        {
                            totalCritRate *= modifier.multiplier;
                            sources.Add($"{item.item.displayName} (for {weapon.displayName}): x{modifier.multiplier:F2}");
                        }
                        else
                        {
                            totalCritRate += modifier.value;
                            sources.Add($"{item.item.displayName} (for {weapon.displayName}): +{modifier.value:P1}");
                        }
                    }
                }
            }
            
            // Weapon's inherent crit rate
            foreach (StatModifier modifier in weapon.statModifiers)
            {
                if (modifier.statType == StatType.CriticalRate)
                {
                    if (modifier.isPercentage)
                    {
                        totalCritRate *= modifier.multiplier;
                        sources.Add($"{weapon.displayName} (inherent): x{modifier.multiplier:F2}");
                    }
                    else
                    {
                        totalCritRate += modifier.value;
                        sources.Add($"{weapon.displayName} (inherent): +{modifier.value:P1}");
                    }
                }
            }
        }
        
        // Cap critical rate at 95% (always 5% chance to not crit)
        return Mathf.Clamp(totalCritRate, 0f, 0.95f);
    }
    
    private float CalculateTotalCriticalDamage(Character attacker, Item weapon, List<string> sources)
    {
        float totalCritDamage = attacker.baseStats.criticalDamage;
        if (totalCritDamage <= 0) totalCritDamage = 1.5f; // Default 150% crit damage
        
        sources.Add($"Base Crit Damage: {totalCritDamage:F2}x");
        
        // Apply character-wide crit damage modifiers
        foreach (PlacedItem item in attacker.inventory.placedItems)
        {
            foreach (StatModifier modifier in item.item.statModifiers)
            {
                if (modifier.statType == StatType.CriticalDamage && !modifier.appliesToSpecificItem)
                {
                    if (modifier.isPercentage)
                    {
                        totalCritDamage *= modifier.multiplier;
                        sources.Add($"{item.item.displayName}: x{modifier.multiplier:F2}");
                    }
                    else
                    {
                        totalCritDamage += modifier.value;
                        sources.Add($"{item.item.displayName}: +{modifier.value:F2}x");
                    }
                }
            }
        }
        
        // Apply weapon-specific crit damage modifiers
        if (weapon != null)
        {
            foreach (PlacedItem item in attacker.inventory.placedItems)
            {
                foreach (StatModifier modifier in item.item.statModifiers)
                {
                    if (modifier.statType == StatType.CriticalDamage && 
                        modifier.appliesToSpecificItem &&
                        modifier.targetItemIds.Contains(weapon.itemId))
                    {
                        if (modifier.isPercentage)
                        {
                            totalCritDamage *= modifier.multiplier;
                            sources.Add($"{item.item.displayName} (for {weapon.displayName}): x{modifier.multiplier:F2}");
                        }
                        else
                        {
                            totalCritDamage += modifier.value;
                            sources.Add($"{item.item.displayName} (for {weapon.displayName}): +{modifier.value:F2}x");
                        }
                    }
                }
            }
            
            // Weapon's inherent crit damage
            foreach (StatModifier modifier in weapon.statModifiers)
            {
                if (modifier.statType == StatType.CriticalDamage)
                {
                    if (modifier.isPercentage)
                    {
                        totalCritDamage *= modifier.multiplier;
                        sources.Add($"{weapon.displayName} (inherent): x{modifier.multiplier:F2}");
                    }
                    else
                    {
                        totalCritDamage += modifier.value;
                        sources.Add($"{weapon.displayName} (inherent): +{modifier.value:F2}x");
                    }
                }
            }
        }
        
        return Mathf.Max(totalCritDamage, 1.0f); // Minimum 100% damage
    }
    
    private bool DetermineCriticalHit(Character attacker, float criticalRate)
    {
        // Deterministic critical hit based on character's luck + attack count
        // This ensures same weapon always has same crit chance, but varies per attack
        
        int seed = attacker.GetHashCode() + attacker.totalAttacksMade + Time.frameCount;
        System.Random random = new System.Random(seed);
        
        float roll = (float)random.NextDouble();
        return roll < criticalRate;
    }
}
```

#### Integration with Combat Damage:
```csharp
public class CombatDamageCalculator : MonoBehaviour
{
    private CriticalHitSystem criticalHitSystem;
    
    private void Awake()
    {
        criticalHitSystem = GetComponent<CriticalHitSystem>();
    }
    
    public float CalculateAttackDamage(Character attacker, Character target, Item weapon, DamageType damageType)
    {
        // Base damage calculation (deterministic)
        float baseDamage = CalculateBaseDamage(attacker, weapon, damageType);
        
        // Apply critical hit calculation
        CriticalHitResult critResult = criticalHitSystem.CalculateCriticalHit(attacker, weapon, baseDamage);
        
        // Apply target's damage vulnerabilities and defenses
        float finalDamage = ProcessIncomingDamage(target, critResult.finalDamage, damageType, attacker);
        
        // Visual and audio feedback for critical hits
        if (critResult.isCritical)
        {
            GameEvents.OnCriticalHit.Raise(new CriticalHitData
            {
                attacker = attacker,
                target = target,
                weapon = weapon,
                damage = finalDamage,
                criticalRate = critResult.criticalRate,
                criticalDamage = critResult.criticalDamage,
                sources = critResult.criticalSources
            });
        }
        
        return finalDamage;
    }
    
    private float CalculateBaseDamage(Character attacker, Item weapon, DamageType damageType)
    {
        // Deterministic base damage - always the same for same equipment
        float attackStat = damageType switch
        {
            DamageType.Physical => attacker.GetFinalStat(StatType.PhysicalAttack),
            DamageType.Fire or DamageType.Ice or DamageType.Electric or 
            DamageType.Thunder or DamageType.Spirit => attacker.GetFinalStat(StatType.SpecialAttack),
            _ => attacker.GetFinalStat(StatType.PhysicalAttack)
        };
        
        float weaponDamage = 0f;
        if (weapon != null)
        {
            // Weapon provides fixed damage bonus
            foreach (StatModifier modifier in weapon.statModifiers)
            {
                if (modifier.statType == StatType.PhysicalAttack || modifier.statType == StatType.SpecialAttack)
                {
                    weaponDamage += modifier.value;
                }
            }
        }
        
        return attackStat + weaponDamage;
    }
}

[System.Serializable]
public struct CriticalHitData
{
    public Character attacker;
    public Character target;
    public Item weapon;
    public float damage;
    public float criticalRate;
    public float criticalDamage;
    public List<string> sources;
}
```

#### Critical Hit Visual & Audio Feedback:
```csharp
public class CriticalHitFeedbackManager : MonoBehaviour
{
    [Header("Visual Effects")]
    [SerializeField] private GameObject criticalHitEffect;
    [SerializeField] private Color criticalDamageColor = Color.yellow;
    [SerializeField] private float criticalScreenShakeIntensity = 0.3f;
    
    [Header("Audio")]
    [SerializeField] private AudioClip criticalHitSound;
    [SerializeField] private float criticalHitVolume = 0.8f;
    
    private void OnEnable()
    {
        GameEvents.OnCriticalHit.RegisterListener(OnCriticalHitEvent);
    }
    
    private void OnDisable()
    {
        GameEvents.OnCriticalHit.UnregisterListener(OnCriticalHitEvent);
    }
    
    private void OnCriticalHitEvent(CriticalHitData critData)
    {
        // Visual effects
        ShowCriticalHitEffect(critData.target.transform.position);
        ShowCriticalDamageNumber(critData.damage, critData.target.transform.position);
        ApplyScreenShake();
        
        // Audio feedback
        PlayCriticalHitAudio();
        
        // UI feedback (show crit details in tooltip)
        if (DebugMode.showCriticalDetails)
        {
            ShowCriticalHitDetails(critData);
        }
    }
    
    private void ShowCriticalHitEffect(Vector3 position)
    {
        if (criticalHitEffect != null)
        {
            GameObject effect = Instantiate(criticalHitEffect, position, Quaternion.identity);
            Destroy(effect, 2.0f);
        }
    }
    
    private void ShowCriticalDamageNumber(float damage, Vector3 position)
    {
        // Create floating damage number with special critical styling
        DamageNumberManager.ShowDamage(damage, position, criticalDamageColor, isTriBold: true, scale: 1.5f);
    }
    
    private void ShowCriticalHitDetails(CriticalHitData critData)
    {
        StringBuilder details = new StringBuilder();
        details.AppendLine($"CRITICAL HIT! {critData.damage:F0} damage");
        details.AppendLine($"Crit Rate: {critData.criticalRate:P1}");
        details.AppendLine($"Crit Damage: {critData.criticalDamage:F2}x");
        details.AppendLine("Sources:");
        
        foreach (string source in critData.sources)
        {
            details.AppendLine($"  • {source}");
        }
        
        UIManager.ShowDebugTooltip(details.ToString(), 3.0f);
    }
}
```
    
    public void OnStatusEffectRemoved(StatusEffect effect)
    {
        activeStatusEffects.Remove(effect);
        
        // Check if this effect modified speed
        bool affectedSpeed = effect.statModifiers.Any(m => m.statType == StatType.Speed);
        if (affectedSpeed)
        {
            CombatManager.Instance.turnOrderManager.OnSpeedChanged(this);
        }
    }
    
    public void OnEquipmentChanged()
    {
        // Called when inventory changes affect stats
        RecalculateEquipmentModifiers();
        CombatManager.Instance.turnOrderManager.OnSpeedChanged(this);
    }
}

/// Turn Order UI Management
public class TurnOrderUI : MonoBehaviour
{
    [SerializeField] private Transform turnQueueParent;
    [SerializeField] private GameObject turnIndicatorPrefab;
    
    public void UpdateTurnOrder(List<CombatEntity> upcomingTurns)
    {
        // Clear existing indicators
        foreach (Transform child in turnQueueParent)
        {
            Destroy(child.gameObject);
        }
        
        // Create new indicators
        for (int i = 0; i < upcomingTurns.Count; i++)
        {
            GameObject indicator = Instantiate(turnIndicatorPrefab, turnQueueParent);
            TurnIndicator turnIndicator = indicator.GetComponent<TurnIndicator>();
            turnIndicator.SetCombatant(upcomingTurns[i], i == 0); // Highlight current turn
        }
    }
    
    public void HighlightSpeedChange(CombatEntity entity)
    {
        // Visual feedback when speed changes affect turn order
        // Could be a brief glow, color change, or animation
        StartCoroutine(FlashSpeedChangeIndicator(entity));
    }
}
```

### 3. Story Flag System

#### Implementation:
```csharp
public class StoryFlagManager : MonoBehaviour
{
    private Dictionary<string, bool> flags = new Dictionary<string, bool>();
    
    public void SetFlag(string flagName, bool value)
    {
        flags[flagName] = value;
        OnFlagChanged?.Invoke(flagName, value);
    }
    
    public bool GetFlag(string flagName)
    {
        return flags.ContainsKey(flagName) ? flags[flagName] : false;
    }
    
    // Transportation unlock example
    public bool CanUseBoat() => GetFlag("BOAT_UNLOCKED");
}
```

### 5. Item Upgrade & Crafting System

#### Implementation Approach:
```csharp
public class CraftingManager : MonoBehaviour
{
    private Dictionary<string, CraftingBlueprint> availableBlueprints;
    
    public bool CanUpgradeItem(Item item)
    {
        return item.canBeUpgraded && item.tier < ItemTier.Unique;
    }
    
    public Item UpgradeItem(List<Item> identicalItems)
    {
        // Verify all items are identical and meet requirements
        if (identicalItems.Count >= 2 && AllItemsIdentical(identicalItems))
        {
            return CreateUpgradedItem(identicalItems[0]);
        }
        return null;
    }
    
    public bool CanCraftItem(CraftingBlueprint blueprint, List<Item> availableItems)
    {
        foreach (CraftingIngredient ingredient in blueprint.requiredIngredients)
        {
            if (!HasSufficientIngredients(ingredient, availableItems))
                return false;
        }
        return true;
    }
}
```

### 8. Item Skill System

#### Skill Usage Contexts:
- **Combat Menu:** Skills accessible during battle through Magic menu
- **Character Menu:** Healing/utility skills usable outside combat
- **Automatic:** Passive effects that trigger without player input

#### Character Restrictions:
```csharp
public class ItemSkillValidator
{
    public bool CanCharacterUseSkill(Character character, ItemSkill skill)
    {
        // Check character type restrictions
        if (skill.restrictedToCharacterTypes.Contains(character.characterType))
            return false;
            
        // Check if character has restrictions for this specific skill
        if (character.itemSkillRestrictions.Contains(skill.skillId))
            return false;
            
        return true;
    }
    
    public List<ItemSkill> GetAvailableSkillsForCharacter(Character character)
    {
        List<ItemSkill> availableSkills = new List<ItemSkill>();
        
        // Get all skills from equipped items of Uncommon+ tier
        foreach (PlacedItem placedItem in character.inventory.placedItems)
        {
            if (placedItem.item.tier >= ItemTier.Uncommon)
            {
                foreach (ItemSkill skill in placedItem.item.itemSkills)
                {
                    if (CanCharacterUseSkill(character, skill))
                        availableSkills.Add(skill);
                }
            }
        }
        
        return availableSkills;
    }
}
```

### 9. Dynamic Camera System

#### Combat Camera Implementation:
```csharp
public class CombatCameraController : MonoBehaviour
{
    public void FocusOnCharacter(Character character, CameraFocusType focusType)
    {
        switch (focusType)
        {
            case CameraFocusType.TurnStart:
                // Rotate around character
                StartCoroutine(RotateAroundCharacter(character.transform));
                break;
            case CameraFocusType.MagicMenu:
                // Zoom to face
                StartCoroutine(ZoomToFace(character.transform));
                break;
            case CameraFocusType.AttackMenu:
                // Move behind shoulder
                StartCoroutine(MoveBehindShoulder(character.transform));
                break;
        }
    }
}
```

---

## Content Creation Workflows

### Adding New Items
1. Create Item ScriptableObject
2. Set tier, category, and character restrictions
3. Define shape in ItemShape component
4. Set up stat modifiers and effects
5. Configure item skills (Uncommon+ tier)
6. Create world model and icon with tier-appropriate coloring
7. Add to ItemDatabase
8. Configure loot pools if needed
9. Set up upgrade path if applicable

### Adding New Crafting Recipes
1. Create CraftingBlueprint ScriptableObject
2. Define required ingredients with tier requirements
3. Set result item and description
4. Add to CraftingManager database
5. Create blueprint loot item if needed
6. Test ingredient validation logic

### Adding New Shops
1. Create Shop ScriptableObject
2. Define shop type and inventory items
3. Set pricing strategy for each item type
4. Configure story flag requirements for access
5. Set up restock rules and intervals
6. Design shopkeeper character and dialogue
7. Place shop location in appropriate town/area
8. Test economic balance and progression flow

### Adding New Status Effects
1. Define effect type and category in StatusEffectType enum
2. Create visual icon and description
3. Set duration type and stacking rules
4. **Define vulnerability interactions** - what damage types trigger special effects
5. **Set up cancellation rules** - what status effects neutralize this one
6. **Configure amplification effects** - how this status enhances certain damage
7. Add to items that should apply the effect
8. Test interaction chains and balance
9. Ensure proper visual feedback in UI

### Adding Status Interactions
1. Identify the two status effects that should interact
2. Choose interaction type (Cancel, Amplify, Transform, Trigger)
3. Define damage multipliers and result effects
4. Set consumption rules (which effects are removed)
5. Add interaction rule to StatusEffectDatabase
6. Test interaction in various combat scenarios
7. Balance risk/reward of applying status effects in sequence

#### Example Interaction Chains:
- **Setup → Payoff:** Apply Oiled → Fire attack (double damage + guaranteed burn)
- **Cancellation:** Burning enemy → Ice attack (removes burn, applies freeze)
- **Defensive:** Wet ally → Electric immunity spell (prevents electric vulnerability)
- **Transformation:** Poison + Fire → Toxic Cloud (area effect)

### Adding Environmental Interactions
1. Create EnvironmentalInteraction ScriptableObject
2. Define trigger requirements and consumption rules
3. Create result prefab for world changes
4. Set up interaction prompt UI
5. Place interaction trigger in appropriate locations
6. Test with various item combinations
7. Verify story flag integration

### Adding Tutorial Steps
1. Define new TutorialStep enum value
2. Create progression logic in TutorialManager
3. Design appropriate item rewards for teaching
4. Create UI guidance and highlighting
5. Test tutorial flow and comprehension
6. Ensure tutorial can be skipped for experienced players

### Adding New Characters
1. Create Character ScriptableObject
2. Design unique grid inventory shape
3. Set up base stats and resistances
4. Create passive skill tree structure
5. Design character model and animations
6. Configure recruitment story flags

### Adding New Areas
1. Create Unity scene for exploration
2. Place encounters and NPCs
3. Configure exits to overworld
4. Set up accessibility story flags
5. Add to LocationManager database
6. Create overworld map icon

### Adding New Combat Encounters
1. Define enemy composition
2. Set up loot pools
3. Place encounter trigger on overworld/exploration
4. Configure story flag prerequisites
5. Test combat balance

---

## Development Phases

### Phase 1: Core Systems Foundation
1. **Data Architecture:** Item system with tiers and equipment types, Character system, Save system
2. **Basic UI:** Inventory grid, drag-and-drop, preview system with highlighting
3. **Equipment System:** Passive gear effects, active tool modifiers
4. **Status Effects:** Basic application and processing system
5. **Combat Framework:** Turn-based logic, basic animations
6. **Scene Management:** Overworld and location transitions

### Phase 2: Gameplay Systems
1. **Complete Inventory System:** Modifiers, rotations, storage, tier visualization, live previews
2. **Full Combat System:** All actions, camera movement, item skills, status effects
3. **Shop System:** Purchase interface, inventory management, pricing
4. **Upgrade & Crafting System:** Item combinations, blueprint management
5. **Character Progression:** Equipment-based stat calculation
6. **Environmental Interactions:** Key usage, torch lighting, ice creation
7. **Story Flag System:** Progression tracking
8. **Transportation System:** Progressive unlocks

### Phase 3: Content Integration
1. **Tutorial System:** Progressive learning area with controlled item introduction
2. **Audio System:** Music management, sound effects
3. **Save System:** Multiple saves, naming, persistence, shop states
4. **Character Restriction System:** Item compatibility validation
5. **Status Effect Polish:** Visual feedback, duration tracking, effect stacking
6. **Economy Balancing:** Shop prices, grid upgrade costs, gold distribution
7. **Polish Features:** Cinematics, advanced UI
8. **Content Creation Tools:** Editor utilities for easy addition

### Phase 4: Content & Polish
1. **Game Content:** Story, areas, encounters
2. **Art Integration:** Voxel models, animations
3. **Balancing:** Combat, progression, economy
4. **Bug Fixing & Optimization**

---

## Performance Considerations

### Optimization Strategies
- **Object Pooling:** For combat effects and UI elements
- **Lazy Loading:** Calculate stats only when needed
- **Caching:** Store complex calculations (modifier effects)
- **Scene Management:** Unload unused areas efficiently

### Memory Management
- **Asset Loading:** Load/unload area-specific assets
- **Save Data:** Compress large save files
- **UI Optimization:** Efficient inventory grid rendering

### Scalability Considerations
- **Modular Architecture:** Easy to add new content types
- **Data-Driven Design:** Most content in ScriptableObjects
- **Event System:** Loose coupling between systems
- **Editor Tools:** Streamline content creation workflow

---

## Development Guidelines

### Code Organization
```
Scripts/
├── Core/
│   ├── Managers/
│   ├── Systems/
│   └── Utilities/
├── Character/
├── Combat/
├── Inventory/
├── World/
├── UI/
└── Data/
    ├── ScriptableObjects/
    └── SaveData/
```

### Naming Conventions
- **Classes:** PascalCase (GridInventory)
- **Methods:** PascalCase (PlaceItem)
- **Variables:** camelCase (currentCharacter)
- **Constants:** UPPER_CASE (MAX_GRID_SIZE)
- **ScriptableObjects:** Descriptive names (FireSword_Item)

### Documentation Standards
- XML comments for all public methods
- README files for each major system
- Inline comments for complex algorithms
- Update this document with any architectural changes

---

---

## User Interface & Accessibility

### Input System Design

#### Dual Input Support:
- **Mouse & Keyboard:** Precise drag-and-drop inventory management
- **Controller Support:** Full Steam Deck compatibility with adapted controls
- **Hybrid Mode:** Allow seamless switching between input methods mid-game

#### Controller Adaptations:
```csharp
public class InputManager : MonoBehaviour
{
    public enum InputMode { MouseKeyboard, Controller, Hybrid }
    
    public void HandleInventoryNavigation()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            // Controller grid navigation
            HandleGridCursor();
            HandleItemSelection();
            HandleRotation(); // Shoulder buttons for rotation
        }
        else
        {
            // Mouse drag-and-drop
            HandleMouseDragDrop();
        }
    }
    
    private void HandleGridCursor()
    {
        // D-pad/left stick moves cursor through grid
        // A/X button picks up/places items
        // Y/Triangle button rotates selected item
        // B/Circle cancels current action
    }
}
```

### Inventory UI System

#### Tabbed Organization:
```csharp
public enum InventoryTab
{
    All,           // Show everything
    Weapons,       // ActiveTools only
    Armor,         // PassiveGear only  
    Consumables,   // Consumable items only
    Materials,     // Crafting materials and gems
    Storage        // Party storage access
}

public class InventoryTabSystem : MonoBehaviour
{
    private Dictionary<InventoryTab, List<ItemCategory>> tabFilters;
    
    private void InitializeTabFilters()
    {
        tabFilters = new Dictionary<InventoryTab, List<ItemCategory>>
        {
            { InventoryTab.Weapons, new List<ItemCategory> 
                { ItemCategory.Sword, ItemCategory.Mace, ItemCategory.Bow, ItemCategory.Staff, ItemCategory.Dagger } },
            { InventoryTab.Armor, new List<ItemCategory> 
                { ItemCategory.Armor, ItemCategory.Helmet, ItemCategory.Pants, ItemCategory.Shirt, ItemCategory.Shoes, 
                  ItemCategory.Ring, ItemCategory.Necklace } },
            // ... additional mappings
        };
    }
}
```

#### Filter System Implementation:
```csharp
public class InventoryFilterSystem : MonoBehaviour
{
    public enum FilterType
    {
        ItemTier,      // T1, T2, T3, T4, T5, T6
        ItemCategory,  // Sword, Armor, etc.
        StatusEffect,  // Items that apply specific effects
        StatModifier,  // Items that boost specific stats
        Usability     // Items current character can use
    }
    
    public List<Item> ApplyFilters(List<Item> items, List<FilterCriteria> filters)
    {
        List<Item> filteredItems = items;
        
        foreach (FilterCriteria filter in filters)
        {
            filteredItems = ApplyFilter(filteredItems, filter);
        }
        
        return filteredItems;
    }
    
    private List<Item> ApplyFilter(List<Item> items, FilterCriteria criteria)
    {
        switch (criteria.filterType)
        {
            case FilterType.ItemTier:
                return items.Where(i => i.tier == criteria.targetTier).ToList();
            case FilterType.ItemCategory:
                return items.Where(i => i.category == criteria.targetCategory).ToList();
            case FilterType.Usability:
                return items.Where(i => CanCurrentCharacterUse(i)).ToList();
            default:
                return items;
        }
    }
}
```

### Item Comparison System

#### Comparison UI:
```csharp
public class ItemComparisonUI : MonoBehaviour
{
    [SerializeField] private GameObject comparisonPanel;
    private Item selectedItem1;
    private Item selectedItem2;
    
    public void ShowComparison(Item item1, Item item2)
    {
        selectedItem1 = item1;
        selectedItem2 = item2;
        
        comparisonPanel.SetActive(true);
        UpdateComparisonDisplay();
    }
    
    private void UpdateComparisonDisplay()
    {
        // Side-by-side stat comparison
        DisplayItemStats(selectedItem1, leftPanel);
        DisplayItemStats(selectedItem2, rightPanel);
        
        // Highlight differences
        HighlightStatDifferences();
        
        // Show tier indicators
        ShowTierComparison();
    }
    
    public void ShowRewardComparison(List<Item> rewardItems)
    {
        // Special comparison mode for combat rewards
        // Click item to select for comparison, click another to compare
        EnableRewardComparisonMode(rewardItems);
    }
}
```

### Undo System

#### Action History:
```csharp
public class UndoSystem : MonoBehaviour
{
    private Stack<IUndoableAction> actionHistory;
    private const int MAX_UNDO_STEPS = 10;
    
    public interface IUndoableAction
    {
        void Execute();
        void Undo();
        string GetDescription();
    }
    
    public class ItemPlacementAction : IUndoableAction
    {
        private Character character;
        private Item item;
        private Vector2Int oldPosition;
        private Vector2Int newPosition;
        private int oldRotation;
        private int newRotation;
        
        public void Execute()
        {
            character.inventory.PlaceItem(item, newPosition, newRotation);
        }
        
        public void Undo()
        {
            character.inventory.RemoveItem(item);
            if (oldPosition != Vector2Int.zero)
            {
                character.inventory.PlaceItem(item, oldPosition, oldRotation);
            }
        }
        
        public string GetDescription()
        {
            return $"Moved {item.displayName}";
        }
    }
    
    public void ExecuteAction(IUndoableAction action)
    {
        action.Execute();
        actionHistory.Push(action);
        
        // Limit undo history
        if (actionHistory.Count > MAX_UNDO_STEPS)
        {
            var actionsToKeep = actionHistory.Take(MAX_UNDO_STEPS).ToList();
            actionHistory.Clear();
            foreach (var a in actionsToKeep.Reverse<IUndoableAction>())
            {
                actionHistory.Push(a);
            }
        }
    }
    
    public bool CanUndo() => actionHistory.Count > 0;
    
    public void UndoLastAction()
    {
        if (CanUndo())
        {
            IUndoableAction lastAction = actionHistory.Pop();
            lastAction.Undo();
            UIManager.ShowUndoFeedback(lastAction.GetDescription());
        }
    }
}
```

### Accessibility Features

#### Colorblind Support:
```csharp
public class AccessibilityManager : MonoBehaviour
{
    public void ApplyTierIndicators(Item item, Text itemText)
    {
        string tierIndicator = GetTierIndicator(item.tier);
        itemText.text = $"{item.displayName} {tierIndicator}";
    }
    
    private string GetTierIndicator(ItemTier tier)
    {
        return tier switch
        {
            ItemTier.Common => "T1",
            ItemTier.Uncommon => "T2", 
            ItemTier.Rare => "T3",
            ItemTier.Elite => "T4",
            ItemTier.Legendary => "T5",
            ItemTier.Unique => "T6",
            _ => ""
        };
    }
}
```

#### Controller UI Navigation:
- **Grid Cursor:** Visual indicator for controller navigation
- **Quick Actions:** Shoulder buttons for common operations (rotate, undo)
- **Context Menus:** Hold button for additional options
- **Audio Cues:** Sound feedback for navigation and actions

---

## Cross-System Communication

### Event-Driven Architecture

#### Unity ScriptableObject Events:
```csharp
[CreateAssetMenu(fileName = "Game Event", menuName = "Game/Events/Game Event")]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();
    
    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised();
        }
    }
    
    public void RegisterListener(GameEventListener listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }
    
    public void UnregisterListener(GameEventListener listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }
}

// Generic version for passing data
[CreateAssetMenu(fileName = "Game Event T", menuName = "Game/Events/Game Event (Data)")]
public class GameEvent<T> : ScriptableObject
{
    private List<GameEventListener<T>> listeners = new List<GameEventListener<T>>();
    
    public void Raise(T data)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventRaised(data);
        }
    }
}

public class GameEventListener : MonoBehaviour
{
    [SerializeField] private GameEvent gameEvent;
    [SerializeField] private UnityEvent response;
    
    private void OnEnable()
    {
        gameEvent.RegisterListener(this);
    }
    
    private void OnDisable()
    {
        gameEvent.UnregisterListener(this);
    }
    
    public void OnEventRaised()
    {
        response.Invoke();
    }
}
```

#### Core Game Events:
```csharp
// Create these as ScriptableObject assets
public static class GameEvents
{
    // Inventory Events
    public static GameEvent<Item> OnItemPlaced;
    public static GameEvent<Item> OnItemRemoved;
    public static GameEvent<Character> OnInventoryChanged;
    public static GameEvent<Character> OnStatsChanged;
    
    // Combat Events  
    public static GameEvent OnCombatStarted;
    public static GameEvent OnCombatEnded;
    public static GameEvent<Character> OnTurnStarted;
    public static GameEvent<Character> OnCharacterDefeated;
    public static GameEvent OnTurnOrderChanged;
    public static GameEvent<CriticalHitData> OnCriticalHit;
    
    // Status Effect Events
    public static GameEvent<StatusEffectData> OnStatusEffectApplied;
    public static GameEvent<StatusEffectData> OnStatusEffectRemoved;
    public static GameEvent<Character> OnSpeedChanged;
    
    // UI Events
    public static GameEvent<Item> OnItemHovered;
    public static GameEvent OnItemUnhovered;
    public static GameEvent<string> OnShowMessage;
    
    // Economy Events
    public static GameEvent<int> OnGoldChanged;
    public static GameEvent<ShopTransaction> OnItemPurchased;
    
    // Story Events
    public static GameEvent<string> OnStoryFlagChanged;
    public static GameEvent<string> OnAreaEntered;
}

[System.Serializable]
public struct StatusEffectData
{
    public Character target;
    public StatusEffect effect;
    public Character source;
}

[System.Serializable]
public struct ShopTransaction
{
    public Item item;
    public int price;
    public Character buyer;
}
```

#### System Communication Examples:
```csharp
// When inventory changes, multiple systems need to know
public class InventorySystem : MonoBehaviour
{
    public void PlaceItem(Item item, Vector2Int position, int rotation)
    {
        // Place item logic...
        placedItems.Add(new PlacedItem(item, position, rotation));
        
        // Notify all interested systems
        GameEvents.OnItemPlaced.Raise(item);
        GameEvents.OnInventoryChanged.Raise(character);
        GameEvents.OnStatsChanged.Raise(character); // For stat recalculation
    }
}

// Combat system listens for stat changes
public class CombatManager : MonoBehaviour
{
    private void OnEnable()
    {
        GameEvents.OnStatsChanged.RegisterListener(OnCharacterStatsChanged);
        GameEvents.OnStatusEffectApplied.RegisterListener(OnStatusEffectApplied);
    }
    
    private void OnCharacterStatsChanged(Character character)
    {
        if (character.GetCurrentSpeed() != lastKnownSpeed[character])
        {
            turnOrderManager.RecalculateTurnOrder();
            GameEvents.OnTurnOrderChanged.Raise();
        }
    }
    
    private void OnStatusEffectApplied(StatusEffectData data)
    {
        if (data.effect.affectsSpeed)
        {
            GameEvents.OnSpeedChanged.Raise(data.target);
        }
    }
}

// UI listens for various events to update displays
public class UIManager : MonoBehaviour
{
    private void OnEnable()
    {
        GameEvents.OnStatsChanged.RegisterListener(UpdateCharacterStatsDisplay);
        GameEvents.OnGoldChanged.RegisterListener(UpdateGoldDisplay);
        GameEvents.OnTurnOrderChanged.RegisterListener(UpdateTurnOrderUI);
        GameEvents.OnShowMessage.RegisterListener(ShowMessagePopup);
    }
}
```

#### Event Flow Diagrams:

**Inventory Item Placement Flow:**
```
Player Places Item → InventorySystem.PlaceItem()
                  ↓
               OnItemPlaced Event
                  ↓
        ┌─────────┼─────────┐
        ↓         ↓         ↓
    ModifierSystem  UIManager  AudioManager
  (calculate effects) (update UI) (play sound)
        ↓
  OnStatsChanged Event
        ↓
    CombatManager
  (update turn order)
```

**Status Effect Application Flow:**
```
Status Effect Applied → StatusEffectManager.ApplyEffect()
                     ↓
               OnStatusEffectApplied Event
                     ↓
           ┌─────────┼─────────┐
           ↓         ↓         ↓
      CombatManager UIManager AudioManager
    (check speed change) (show icon) (play sound)
           ↓
    OnSpeedChanged Event
           ↓
    TurnOrderManager.RecalculateTurnOrder()
           ↓
    OnTurnOrderChanged Event
           ↓
       UIManager
    (update turn queue)
```

### Dependency Management:
```csharp
// Service Locator pattern for core systems
public class ServiceLocator : MonoBehaviour
{
    private static ServiceLocator instance;
    public static ServiceLocator Instance => instance;
    
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private CombatManager combatManager;
    [SerializeField] private StatusEffectManager statusEffectManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private AudioManager audioManager;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public static T GetService<T>() where T : MonoBehaviour
    {
        return Instance.GetComponent<T>();
    }
}
```

---

## Settings & Options System

### Basic Settings Implementation:
```csharp
[System.Serializable]
public class GameSettings
{
    [Header("Video Settings")]
    public Resolution resolution = new Resolution { width = 1920, height = 1080, refreshRate = 60 };
    public bool fullscreen = true;
    public bool vsync = true;
    
    [Header("Audio Settings")]
    [Range(0f, 1f)] public float masterVolume = 1.0f;
    [Range(0f, 1f)] public float musicVolume = 0.8f;
    [Range(0f, 1f)] public float sfxVolume = 1.0f;
    
    [Header("Accessibility")]
    public bool colorblindMode = false;
    public bool reducedMotion = false;
}

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private GameSettings currentSettings;
    private const string SETTINGS_KEY = "GameSettings";
    
    private void Awake()
    {
        LoadSettings();
        ApplySettings();
    }
    
    public void SaveSettings()
    {
        string json = JsonUtility.ToJson(currentSettings);
        PlayerPrefs.SetString(SETTINGS_KEY, json);
        PlayerPrefs.Save();
    }
    
    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey(SETTINGS_KEY))
        {
            string json = PlayerPrefs.GetString(SETTINGS_KEY);
            currentSettings = JsonUtility.FromJson<GameSettings>(json);
        }
        else
        {
            currentSettings = new GameSettings(); // Use defaults
        }
    }
    
    public void ApplySettings()
    {
        // Apply video settings
        Screen.SetResolution(currentSettings.resolution.width, 
                           currentSettings.resolution.height, 
                           currentSettings.fullscreen);
        QualitySettings.vSyncCount = currentSettings.vsync ? 1 : 0;
        
        // Apply audio settings
        AudioListener.volume = currentSettings.masterVolume;
        
        // Find and configure audio mixers
        AudioMixer mixer = Resources.Load<AudioMixer>("MainAudioMixer");
        if (mixer != null)
        {
            mixer.SetFloat("MusicVolume", Mathf.Log10(currentSettings.musicVolume) * 20);
            mixer.SetFloat("SFXVolume", Mathf.Log10(currentSettings.sfxVolume) * 20);
        }
        
        // Apply accessibility settings
        AccessibilityManager.SetColorblindMode(currentSettings.colorblindMode);
        AccessibilityManager.SetReducedMotion(currentSettings.reducedMotion);
    }
    
    // UI Binding Methods
    public void SetResolution(int resolutionIndex)
    {
        Resolution[] resolutions = Screen.resolutions;
        if (resolutionIndex < resolutions.Length)
        {
            currentSettings.resolution = resolutions[resolutionIndex];
            ApplyVideoSettings();
        }
    }
    
    public void SetFullscreen(bool fullscreen)
    {
        currentSettings.fullscreen = fullscreen;
        Screen.fullScreen = fullscreen;
    }
    
    public void SetMasterVolume(float volume)
    {
        currentSettings.masterVolume = volume;
        AudioListener.volume = volume;
    }
    
    public void SetMusicVolume(float volume)
    {
        currentSettings.musicVolume = volume;
        AudioMixer mixer = Resources.Load<AudioMixer>("MainAudioMixer");
        mixer?.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }
    
    public void SetSFXVolume(float volume)
    {
        currentSettings.sfxVolume = volume;
        AudioMixer mixer = Resources.Load<AudioMixer>("MainAudioMixer");
        mixer?.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
    }
}
```

### Settings UI:
```csharp
public class SettingsUI : MonoBehaviour
{
    [Header("Video Settings")]
    [SerializeField] private Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Toggle vsyncToggle;
    
    [Header("Audio Settings")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    
    [Header("Accessibility")]
    [SerializeField] private Toggle colorblindToggle;
    [SerializeField] private Toggle reducedMotionToggle;
    
    private SettingsManager settingsManager;
    
    private void Start()
    {
        settingsManager = FindObjectOfType<SettingsManager>();
        PopulateResolutionDropdown();
        LoadCurrentSettings();
    }
    
    private void PopulateResolutionDropdown()
    {
        resolutionDropdown.ClearOptions();
        
        List<string> options = new List<string>();
        Resolution[] resolutions = Screen.resolutions;
        
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
        }
        
        resolutionDropdown.AddOptions(options);
    }
    
    private void LoadCurrentSettings()
    {
        GameSettings settings = settingsManager.currentSettings;
        
        // Set UI elements to current values
        fullscreenToggle.isOn = settings.fullscreen;
        vsyncToggle.isOn = settings.vsync;
        masterVolumeSlider.value = settings.masterVolume;
        musicVolumeSlider.value = settings.musicVolume;
        sfxVolumeSlider.value = settings.sfxVolume;
        colorblindToggle.isOn = settings.colorblindMode;
        reducedMotionToggle.isOn = settings.reducedMotion;
        
        // Set resolution dropdown
        Resolution currentRes = settings.resolution;
        Resolution[] resolutions = Screen.resolutions;
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == currentRes.width && 
                resolutions[i].height == currentRes.height)
            {
                resolutionDropdown.value = i;
                break;
            }
        }
    }
    
    public void OnSettingsChanged()
    {
        settingsManager.SaveSettings();
    }
}
```

---

## Unity Implementation Patterns

### ScriptableObject Architecture:
```csharp
// Base class for all game data
public abstract class GameData : ScriptableObject
{
    [SerializeField] protected string id;
    public string ID => id;
    
    protected virtual void OnValidate()
    {
        if (string.IsNullOrEmpty(id))
        {
            id = name.Replace(" ", "").ToLower();
        }
    }
}

// Item data structure
[CreateAssetMenu(fileName = "New Item", menuName = "Game/Item")]
public class ItemData : GameData
{
    [Header("Basic Info")]
    public string displayName;
    [TextArea(3, 5)] public string description;
    public Sprite icon;
    public ItemTier tier;
    public ItemCategory category;
    public ItemType type;
    
    [Header("Grid Properties")]
    public ItemShape shape;
    public bool canRotate = true;
    
    [Header("Effects")]
    public List<StatModifier> statModifiers;
    public List<PassiveEffect> passiveEffects;
    public List<ItemSkill> itemSkills;
    
    [Header("Modifiers")]
    [ShowIf("type", ItemType.Modifier)]
    public ModifierPattern modifierZones;
    
    [Header("Economic")]
    public int basePrice;
    public bool canBeSold = true;
    public bool canBeUpgraded = true;
}

// Character data structure
[CreateAssetMenu(fileName = "New Character", menuName = "Game/Character")]
public class CharacterData : GameData
{
    [Header("Basic Info")]
    public string displayName;
    public string characterType;
    [TextArea(3, 5)] public string description;
    public Sprite portrait;
    public GameObject characterPrefab;
    
    [Header("Stats")]
    public BaseStats baseStats;
    public CharacterResistances resistances;
    
    [Header("Equipment")]
    public List<ItemCategory> allowedItemCategories;
    public List<string> itemSkillRestrictions;
    public GridShape startingGridShape;
    public GridShape maxGridShape;
    
    [Header("Progression")]
    public List<string> requiredStoryFlags; // To unlock this character
}
```

### Prefab Organization:
```
Prefabs/
├── Characters/
│   ├── Player/
│   │   ├── PlayerCharacter_Warrior.prefab
│   │   ├── PlayerCharacter_Mage.prefab
│   │   └── PlayerCharacter_Rogue.prefab
│   └── Enemies/
│       ├── Enemy_Goblin.prefab
│       ├── Enemy_Orc.prefab
│       └── Boss_Dragon.prefab
├── Items/
│   ├── Weapons/
│   │   ├── Sword_Basic.prefab
│   │   ├── Staff_Fire.prefab
│   │   └── Bow_Elven.prefab
│   ├── Armor/
│   │   ├── Helmet_Iron.prefab
│   │   └── Chestplate_Steel.prefab
│   └── Effects/
│       ├── ItemPickup_Common.prefab
│       ├── ItemPickup_Rare.prefab
│       └── ItemPickup_Legendary.prefab
├── UI/
│   ├── Panels/
│   │   ├── InventoryPanel.prefab
│   │   ├── CombatUI.prefab
│   │   └── ShopPanel.prefab
│   ├── Elements/
│   │   ├── ItemSlot.prefab
│   │   ├── StatusEffectIcon.prefab
│   │   └── TurnOrderIndicator.prefab
│   └── Popups/
│       ├── MessagePopup.prefab
│       ├── ConfirmationDialog.prefab
│       └── ItemComparisonPanel.prefab
└── Systems/
    ├── GameManager.prefab
    ├── UIManager.prefab
    ├── AudioManager.prefab
    └── ServiceLocator.prefab
```

### Scene Management:
```csharp
public class SceneManagerCustom : MonoBehaviour
{
    [System.Serializable]
    public class SceneReference
    {
        public string sceneName;
        public string displayName;
        public bool isPersistent; // Should this scene stay loaded?
        public List<string> requiredScenes; // Dependencies
    }
    
    [SerializeField] private List<SceneReference> gameScenes;
    private List<string> loadedScenes = new List<string>();
    
    public void LoadGameScene(string sceneName, bool additive = false)
    {
        SceneReference sceneRef = gameScenes.Find(s => s.sceneName == sceneName);
        if (sceneRef == null)
        {
            Debug.LogError($"Scene {sceneName} not found in scene references");
            return;
        }
        
        StartCoroutine(LoadSceneCoroutine(sceneRef, additive));
    }
    
    private IEnumerator LoadSceneCoroutine(SceneReference sceneRef, bool additive)
    {
        // Load required dependencies first
        foreach (string requiredScene in sceneRef.requiredScenes)
        {
            if (!loadedScenes.Contains(requiredScene))
            {
                yield return SceneManager.LoadSceneAsync(requiredScene, LoadSceneMode.Additive);
                loadedScenes.Add(requiredScene);
            }
        }
        
        // Load the main scene
        LoadSceneMode mode = additive ? LoadSceneMode.Additive : LoadSceneMode.Single;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneRef.sceneName, mode);
        
        while (!operation.isDone)
        {
            float progress = operation.progress;
            GameEvents.OnLoadingProgress.Raise(progress);
            yield return null;
        }
        
        if (!loadedScenes.Contains(sceneRef.sceneName))
        {
            loadedScenes.Add(sceneRef.sceneName);
        }
        
        GameEvents.OnSceneLoaded.Raise(sceneRef.sceneName);
    }
    
    public void UnloadNonPersistentScenes()
    {
        foreach (string sceneName in loadedScenes.ToList())
        {
            SceneReference sceneRef = gameScenes.Find(s => s.sceneName == sceneName);
            if (sceneRef != null && !sceneRef.isPersistent)
            {
                SceneManager.UnloadSceneAsync(sceneName);
                loadedScenes.Remove(sceneName);
            }
        }
    }
}
```

### Component Architecture:
```csharp
// Base class for all interactive objects
public abstract class InteractableObject : MonoBehaviour
{
    [SerializeField] protected string interactionPrompt = "Interact";
    [SerializeField] protected bool canInteract = true;
    
    public abstract void Interact(Character interactor);
    
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canInteract)
        {
            UIManager.ShowInteractionPrompt(interactionPrompt);
        }
    }
    
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.HideInteractionPrompt();
        }
    }
}

// Specific implementations
public class ChestInteractable : InteractableObject
{
    [SerializeField] private List<ItemData> lootItems;
    [SerializeField] private bool hasBeenOpened = false;
    
    public override void Interact(Character interactor)
    {
        if (!hasBeenOpened)
        {
            hasBeenOpened = true;
            LootManager.ShowLootInterface(lootItems);
            
            // Visual feedback
            GetComponent<Animator>().SetTrigger("Open");
            AudioManager.PlaySFX("ChestOpen");
        }
    }
}

public class NPCInteractable : InteractableObject
{
    [SerializeField] private DialogueData dialogue;
    [SerializeField] private QuestData associatedQuest;
    
    public override void Interact(Character interactor)
    {
        if (associatedQuest != null && !associatedQuest.IsCompleted())
        {
            QuestManager.StartQuest(associatedQuest);
        }
        
        DialogueManager.StartDialogue(dialogue);
    }
}
```

---

## Developer Tools & Debugging

### Debug Console System:
```csharp
public class DebugConsole : MonoBehaviour
{
    [SerializeField] private bool enableInBuild = false;
    [SerializeField] private KeyCode toggleKey = KeyCode.BackQuote;
    [SerializeField] private GameObject consoleUI;
    [SerializeField] private InputField commandInput;
    [SerializeField] private Text outputText;
    
    private Dictionary<string, System.Action<string[]>> commands;
    private bool isConsoleOpen = false;
    
    private void Awake()
    {
        if (!Debug.isDebugBuild && !enableInBuild)
        {
            gameObject.SetActive(false);
            return;
        }
        
        InitializeCommands();
        consoleUI.SetActive(false);
    }
    
    private void InitializeCommands()
    {
        commands = new Dictionary<string, System.Action<string[]>>
        {
            { "give", GiveItemCommand },
            { "setstat", SetStatCommand },
            { "setstory", SetStoryFlagCommand },
            { "addgold", AddGoldCommand },
            { "heal", HealCommand },
            { "status", AddStatusCommand },
            { "clearinv", ClearInventoryCommand },
            { "teleport", TeleportCommand },
            { "spawnenemy", SpawnEnemyCommand },
            { "godmode", GodModeCommand },
            { "setcrit", SetCritCommand },
            { "testcrit", TestCritCommand },
            { "help", HelpCommand }
        };
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleConsole();
        }
    }
    
    private void ToggleConsole()
    {
        isConsoleOpen = !isConsoleOpen;
        consoleUI.SetActive(isConsoleOpen);
        
        if (isConsoleOpen)
        {
            commandInput.Select();
            commandInput.ActivateInputField();
        }
    }
    
    public void ExecuteCommand(string input)
    {
        string[] parts = input.Split(' ');
        string command = parts[0].ToLower();
        
        if (commands.ContainsKey(command))
        {
            try
            {
                commands[command](parts);
                LogOutput($"> {input}");
            }
            catch (System.Exception e)
            {
                LogOutput($"Error: {e.Message}");
            }
        }
        else
        {
            LogOutput($"Unknown command: {command}. Type 'help' for available commands.");
        }
        
        commandInput.text = "";
        commandInput.Select();
        commandInput.ActivateInputField();
    }
    
    private void LogOutput(string message)
    {
        outputText.text += message + "\n";
        
        // Limit output length
        string[] lines = outputText.text.Split('\n');
        if (lines.Length > 20)
        {
            outputText.text = string.Join("\n", lines, lines.Length - 20, 20);
        }
    }
    
    // Command implementations
    private void GiveItemCommand(string[] args)
    {
        if (args.Length < 2)
        {
            LogOutput("Usage: give <itemId> [quantity]");
            return;
        }
        
        string itemId = args[1];
        int quantity = args.Length > 2 ? int.Parse(args[2]) : 1;
        
        ItemData item = ItemDatabase.GetItem(itemId);
        if (item != null)
        {
            for (int i = 0; i < quantity; i++)
            {
                LootManager.AddToPartyStorage(item);
            }
            LogOutput($"Added {quantity}x {item.displayName} to party storage");
        }
        else
        {
            LogOutput($"Item not found: {itemId}");
        }
    }
    
    private void SetStatCommand(string[] args)
    {
        if (args.Length < 3)
        {
            LogOutput("Usage: setstat <statname> <value>");
            return;
        }
        
        string statName = args[1].ToLower();
        int value = int.Parse(args[2]);
        
        Character currentChar = PartyManager.GetCurrentCharacter();
        if (currentChar != null)
        {
            // Temporarily modify stats for testing
            DebugStatModifier.ModifyCharacterStat(currentChar, statName, value);
            LogOutput($"Set {statName} to {value} for {currentChar.displayName}");
        }
    }
    
    private void HelpCommand(string[] args)
    {
        LogOutput("Available commands:");
        LogOutput("give <itemId> [qty] - Add item to party storage");
        LogOutput("setstat <stat> <value> - Modify character stat");
        LogOutput("setstory <flag> <true/false> - Set story flag");
        LogOutput("addgold <amount> - Add gold to player");
        LogOutput("heal - Fully heal current character");
        LogOutput("status <effectId> - Apply status effect");
        LogOutput("clearinv - Clear current character inventory");
        LogOutput("teleport <scene> - Load specific scene");
        LogOutput("spawnenemy <enemyId> - Start combat with enemy");
        LogOutput("godmode - Toggle invincibility");
        LogOutput("setcrit <rate|damage> <value> - Set crit rate (0.0-1.0) or damage (1.0+)");
        LogOutput("testcrit [iterations] - Test critical hit calculations");
    }
    
    private void SetCritCommand(string[] args)
    {
        if (args.Length < 3)
        {
            LogOutput("Usage: setcrit <rate|damage> <value>");
            LogOutput("  rate: 0.0 to 1.0 (0% to 100%)");
            LogOutput("  damage: 1.0+ (100%+ damage multiplier)");
            return;
        }
        
        string critType = args[1].ToLower();
        float value = float.Parse(args[2]);
        
        Character currentChar = PartyManager.GetCurrentCharacter();
        if (currentChar != null)
        {
            if (critType == "rate")
            {
                currentChar.baseStats.criticalRate = Mathf.Clamp(value, 0f, 1f);
                LogOutput($"Set {currentChar.displayName} crit rate to {value:P1}");
            }
            else if (critType == "damage")
            {
                currentChar.baseStats.criticalDamage = Mathf.Max(value, 1f);
                LogOutput($"Set {currentChar.displayName} crit damage to {value:F2}x");
            }
            else
            {
                LogOutput("Invalid crit type. Use 'rate' or 'damage'");
            }
        }
    }
    
    private void TestCritCommand(string[] args)
    {
        int iterations = args.Length > 1 ? int.Parse(args[1]) : 100;
        
        Character currentChar = PartyManager.GetCurrentCharacter();
        if (currentChar == null)
        {
            LogOutput("No character selected for testing");
            return;
        }
        
        Item weapon = currentChar.inventory.GetEquippedWeapon(); // Implement this method
        CriticalHitSystem critSystem = FindObjectOfType<CriticalHitSystem>();
        
        int criticalHits = 0;
        float totalDamage = 0f;
        float baseDamage = 100f; // Test damage
        
        LogOutput($"Testing {iterations} attacks with {currentChar.displayName}...");
        
        for (int i = 0; i < iterations; i++)
        {
            CriticalHitSystem.CriticalHitResult result = critSystem.CalculateCriticalHit(currentChar, weapon, baseDamage);
            
            if (result.isCritical)
            {
                criticalHits++;
            }
            
            totalDamage += result.finalDamage;
        }
        
        float critRate = (float)criticalHits / iterations;
        float avgDamage = totalDamage / iterations;
        
        LogOutput($"Results: {criticalHits}/{iterations} crits ({critRate:P1})");
        LogOutput($"Average damage: {avgDamage:F1} (expected: {baseDamage:F1})");
        
        // Show current crit stats
        float currentCritRate = critSystem.CalculateTotalCriticalRate(currentChar, weapon, new List<string>());
        float currentCritDamage = critSystem.CalculateTotalCriticalDamage(currentChar, weapon, new List<string>());
        LogOutput($"Current stats - Rate: {currentCritRate:P1}, Damage: {currentCritDamage:F2}x");
    }
}
```

### In-Game Balance Testing Tools:
```csharp
public class BalanceTestingTools : MonoBehaviour
{
    [Header("Combat Testing")]
    [SerializeField] private List<EnemyData> testEnemies;
    [SerializeField] private Transform spawnPoint;
    
    [Header("Item Testing")]
    [SerializeField] private ItemData[] testItems;
    
    [Header("UI")]
    [SerializeField] private GameObject testingPanel;
    [SerializeField] private Dropdown enemyDropdown;
    [SerializeField] private Dropdown itemDropdown;
    [SerializeField] private InputField quantityInput;
    
    private void Start()
    {
        if (!Debug.isDebugBuild)
        {
            gameObject.SetActive(false);
            return;
        }
        
        PopulateDropdowns();
        testingPanel.SetActive(false);
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            testingPanel.SetActive(!testingPanel.activeSelf);
        }
    }
    
    private void PopulateDropdowns()
    {
        // Populate enemy dropdown
        enemyDropdown.ClearOptions();
        List<string> enemyNames = new List<string>();
        foreach (EnemyData enemy in testEnemies)
        {
            enemyNames.Add(enemy.displayName);
        }
        enemyDropdown.AddOptions(enemyNames);
        
        // Populate item dropdown
        itemDropdown.ClearOptions();
        List<string> itemNames = new List<string>();
        foreach (ItemData item in testItems)
        {
            itemNames.Add(item.displayName);
        }
        itemDropdown.AddOptions(itemNames);
    }
    
    public void SpawnTestEnemy()
    {
        if (enemyDropdown.value < testEnemies.Count)
        {
            EnemyData enemyData = testEnemies[enemyDropdown.value];
            GameObject enemy = Instantiate(enemyData.enemyPrefab, spawnPoint.position, spawnPoint.rotation);
            
            // Start combat immediately
            List<Enemy> enemies = new List<Enemy> { enemy.GetComponent<Enemy>() };
            CombatManager.StartCombat(enemies);
        }
    }
    
    public void GiveTestItem()
    {
        if (itemDropdown.value < testItems.Length)
        {
            ItemData item = testItems[itemDropdown.value];
            int quantity = int.Parse(quantityInput.text);
            
            for (int i = 0; i < quantity; i++)
            {
                LootManager.AddToPartyStorage(item);
            }
            
            Debug.Log($"Added {quantity}x {item.displayName} to party storage");
        }
    }
    
    public void RunBalanceTest()
    {
        // Quick balance validation
        Character testChar = PartyManager.GetCurrentCharacter();
        EnemyData testEnemy = testEnemies[enemyDropdown.value];
        
        int playerPower = CalculateCharacterPower(testChar);
        int enemyPower = CalculateEnemyPower(testEnemy);
        
        float ratio = (float)playerPower / enemyPower;
        string assessment = ratio switch
        {
            < 0.8f => "Enemy too strong",
            > 1.2f => "Player too strong", 
            _ => "Balanced"
        };
        
        Debug.Log($"Balance Test: Player({playerPower}) vs Enemy({enemyPower}) = {ratio:F2} ({assessment})");
    }
    
    private int CalculateCharacterPower(Character character)
    {
        CharacterStats stats = character.CalculateFinalStats();
        return stats.maxHP + stats.physicalAttack + stats.specialAttack + 
               stats.physicalDefense + stats.specialDefense + stats.speed;
    }
    
    private int CalculateEnemyPower(EnemyData enemy)
    {
        return enemy.maxHP + enemy.attack + enemy.defense + enemy.speed;
    }
}
```

### Save File Editor:
```csharp
public class SaveFileEditor : EditorWindow
{
    private GameSaveData currentSave;
    private Vector2 scrollPosition;
    private string saveFileName = "";
    
    [MenuItem("Tools/Save File Editor")]
    public static void ShowWindow()
    {
        GetWindow<SaveFileEditor>("Save File Editor");
    }
    
    private void OnGUI()
    {
        GUILayout.Label("Save File Editor", EditorStyles.boldLabel);
        
        GUILayout.BeginHorizontal();
        saveFileName = EditorGUILayout.TextField("Save File Name:", saveFileName);
        if (GUILayout.Button("Load Save"))
        {
            LoadSaveFile();
        }
        GUILayout.EndHorizontal();
        
        if (currentSave != null)
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            
            DrawSaveDataEditor();
            
            EditorGUILayout.EndScrollView();
            
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Save Changes"))
            {
                SaveChanges();
            }
            if (GUILayout.Button("Revert"))
            {
                LoadSaveFile();
            }
            GUILayout.EndHorizontal();
        }
    }
    
    private void LoadSaveFile()
    {
        string path = Path.Combine(Application.persistentDataPath, "Saves", saveFileName + ".json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            currentSave = JsonUtility.FromJson<GameSaveData>(json);
        }
        else
        {
            Debug.LogError($"Save file not found: {path}");
        }
    }
    
    private void DrawSaveDataEditor()
    {
        GUILayout.Label("Basic Info", EditorStyles.boldLabel);
        currentSave.saveName = EditorGUILayout.TextField("Save Name:", currentSave.saveName);
        currentSave.currentScene = EditorGUILayout.TextField("Current Scene:", currentSave.currentScene);
        currentSave.playerGold = EditorGUILayout.IntField("Gold:", currentSave.playerGold);
        currentSave.playtime = EditorGUILayout.FloatField("Playtime (hours):", currentSave.playtime);
        
        EditorGUILayout.Space();
        GUILayout.Label("Story Flags", EditorStyles.boldLabel);
        
        // Story flags editor
        if (currentSave.storyFlags != null)
        {
            List<string> flagKeys = new List<string>(currentSave.storyFlags.Keys);
            foreach (string flag in flagKeys)
            {
                bool value = currentSave.storyFlags[flag];
                bool newValue = EditorGUILayout.Toggle(flag, value);
                if (newValue != value)
                {
                    currentSave.storyFlags[flag] = newValue;
                }
            }
        }
        
        EditorGUILayout.Space();
        if (GUILayout.Button("Add Story Flag"))
        {
            // Simple dialog to add new flag
            currentSave.storyFlags = currentSave.storyFlags ?? new Dictionary<string, bool>();
            currentSave.storyFlags["NEW_FLAG"] = false;
        }
    }
    
    private void SaveChanges()
    {
        string path = Path.Combine(Application.persistentDataPath, "Saves", saveFileName + ".json");
        string json = JsonUtility.ToJson(currentSave, true);
        File.WriteAllText(path, json);
        Debug.Log($"Save file updated: {path}");
    }
}
```

---

## Asset Pipeline & Organization

### POC Asset Philosophy
**Gameplay Mechanics > Visual Polish**
- All art assets designed for rapid iteration and gameplay testing
- Unity primitives provide immediate, functional visuals
- Focus on system validation rather than visual appeal
- Art can be upgraded incrementally without changing core systems

### Primitive Asset Standards

#### 3D Character Models (Unity Primitives):
```
Player Characters:
- Base: Capsule (1.8 units tall, 0.5 units wide)
- Head: Sphere (0.3 units diameter) positioned on top
- Color coding by character class:
  - Warrior: Blue (#1E90FF)
  - Mage: Purple (#9370DB)  
  - Rogue: Dark Green (#006400)
  - Healer: Yellow (#FFD700)

Enemies:
- Goblin: Green Sphere (0.8 units diameter)
- Orc: Red Cube (1.2 units per side)
- Skeleton: White Capsule (1.6 units tall)
- Dragon: Large Purple Capsule (3.0 units long, horizontal)
- Boss enemies: Larger versions with distinct colors

NPCs:
- Generic: Green Capsule (1.8 units tall)
- Important: Green Capsule with Yellow Sphere head
- Merchants: Brown Capsule with larger middle section
```

#### 3D Environment Assets (Unity Primitives):
```
Overworld Terrain:
- Ground: Large Plane with grass texture (Unity default)
- Mountains: Scaled Cubes with gray material
- Water: Blue Plane with transparency
- Trees: Brown Cylinder (trunk) + Green Sphere (leaves)
- Paths: Stretched Cubes with brown material

Buildings & Structures:
- Houses: Brown Cube base + Red Cube roof (rotated 45°)
- Shops: Larger brown cubes with distinct roof colors
- Dungeons: Gray cube entrances with dark interiors
- Bridges: Long, thin cubes connecting areas

Interactive Objects:
- Treasure Chests: Brown Cube (1.0 x 0.7 x 0.7 units)
- Doors: Tall, thin cubes (2.0 x 0.1 x 1.0 units)
- Levers: Small cylinders with cube handles
- Crystals: Stretched octahedrons (Unity primitive)
```

#### Item Visualization (2D Icons + 3D World Objects):
```
UI Display: 2D placeholder icons (your current sprites)
World Pickup Objects: Small colored cubes floating above ground
- Weapons: Elongated cubes matching item shape
- Armor: Appropriately sized cubes
- Potions: Small cylinders
- Gems: Small octahedrons with bright colors

Equipment on Characters:
- Sword: Thin cylinder attached to character side
- Shield: Flat cube attached to character arm  
- Staff: Long cylinder with sphere on top
- Helmet: Small cube on character head
- Armor: Slightly larger capsule overlaying character
```

### Folder Structure (POC Optimized):
```
Assets/
├── Materials/
│   ├── Characters/
│   │   ├── Player_Blue.mat
│   │   ├── Enemy_Red.mat
│   │   └── NPC_Green.mat
│   ├── Environment/
│   │   ├── Grass.mat
│   │   ├── Stone.mat
│   │   └── Water.mat
│   └── Items/
│       ├── Weapon_Gray.mat
│       ├── Armor_Brown.mat
│       └── Gem_Colored.mat
├── Prefabs/
│   ├── Characters/
│   │   ├── Player_Primitive.prefab
│   │   ├── Enemy_Goblin.prefab
│   │   └── NPC_Generic.prefab
│   ├── Environment/
│   │   ├── Tree_Basic.prefab
│   │   ├── House_Basic.prefab
│   │   └── Chest_Basic.prefab
│   └── UI/
│       ├── InventoryPanel.prefab
│       └── CombatUI.prefab
├── Scenes/
│   ├── Testing/
│   │   ├── InventoryTest.unity
│   │   ├── CombatTest.unity
│   │   └── CameraTest.unity
│   ├── Core/
│   │   ├── MainGame.unity
│   │   └── Overworld.unity
│   └── Areas/
│       ├── TestDungeon.unity
│       └── TestTown.unity
└── [All previous script/data folders remain the same]
```

### Material Creation Guidelines:
```csharp
// Standard materials for consistent primitive appearance
public class PrimitiveMaterialLibrary
{
    // Character materials with emission for status effects
    public Material playerBaseMaterial;      // Blue diffuse
    public Material enemyBaseMaterial;       // Red diffuse  
    public Material npcBaseMaterial;         // Green diffuse
    
    // Status effect overlays (applied via code)
    public Material poisonOverlay;           // Green emission
    public Material burnOverlay;             // Red emission
    public Material freezeOverlay;           // Blue emission
    
    // Environment materials
    public Material grassMaterial;           // Green with grass texture
    public Material stoneMaterial;           // Gray with stone texture
    public Material waterMaterial;           // Blue with transparency
    public Material woodMaterial;            // Brown diffuse
    
    // Item materials (tier-based coloring)
    public Material[] itemTierMaterials = new Material[6];
    // T1: White, T2: Blue, T3: Gold, T4: Orange, T5: Red, T6: Purple
}
```

### Rapid Prototyping Advantages:
```
✅ Instant visual feedback - no waiting for art
✅ Easy differentiation through color coding
✅ Consistent style across all game elements  
✅ Fast iteration - change gameplay instantly
✅ Clear focus on mechanics validation
✅ Perfect for demonstrating systems to others
✅ Zero art bottlenecks during development
✅ Easily replaceable with detailed art later
```

### Visual Hierarchy (Color-Coded Systems):
```
Character Status Indication:
- Healthy: Normal material color
- Poisoned: Green glow/emission added
- Burning: Red glow/emission added
- Frozen: Blue tint/emission added
- Buffed: Brighter/saturated colors
- Debuffed: Darker/desaturated colors

Item Quality Indication:
- Common (T1): White/Gray materials
- Uncommon (T2): Blue materials with slight glow
- Rare (T3): Gold materials with medium glow
- Elite (T4): Orange materials with bright glow
- Legendary (T5): Red materials with intense glow
- Unique (T6): Purple materials with special effects

Interactive Object States:
- Available: Normal color + subtle pulse
- Activated: Brighter emission
- Disabled: Darker/grayed out
- Important: Yellow outline or emission
```

### Performance Optimization (Primitive Rendering):
```csharp
// Efficient primitive rendering for POC
public class PrimitiveOptimizer : MonoBehaviour
{
    [Header("LOD Settings")]
    public float maxRenderDistance = 100f;
    public int maxVisiblePrimitives = 500;
    
    [Header("Batching")]
    public bool enableStaticBatching = true;
    public bool enableDynamicBatching = true;
    
    private void Start()
    {
        // Enable GPU instancing for identical primitives
        OptimizePrimitiveRendering();
    }
    
    private void OptimizePrimitiveRendering()
    {
        // Group identical primitive objects for batching
        // Use LOD system to reduce complexity at distance
        // Implement object pooling for frequently spawned items
    }
}
```

### Content Creation Speed:
```
Character Creation: 2 minutes
- Create capsule + sphere
- Apply colored material
- Add basic collision

Environment Asset: 1-3 minutes  
- Arrange primitives in scene
- Apply appropriate materials
- Set up interactions

Item Visualization: 30 seconds
- Scale primitive to match item shape
- Apply tier-appropriate material
- Position relative to character

Total POC Asset Creation Time: 2-4 hours maximum
Result: Fully playable 3D game world with all systems functional
```
```

---

## Audio & Visual Systems

### Dynamic Music System

#### Music State Management:
```csharp
public class DynamicMusicManager : MonoBehaviour
{
    public enum MusicState
    {
        Exploration,    // Area-specific exploration music
        Combat,         // Battle music with intensity layers
        Victory,        // Post-combat victory stinger
        Shop,           // Town/shop ambient music
        Dramatic,       // Story moments and cinematics
        Silence         // For dramatic pauses
    }
    
    private Dictionary<string, AudioClip> explorationTracks;
    private Dictionary<CombatIntensity, AudioClip> combatLayers;
    
    public void TransitionToState(MusicState newState, string areaId = null)
    {
        switch (newState)
        {
            case MusicState.Exploration:
                PlayExplorationMusic(areaId);
                break;
            case MusicState.Combat:
                TransitionToCombat();
                break;
            case MusicState.Victory:
                PlayVictoryStinger();
                break;
        }
    }
    
    private void TransitionToCombat()
    {
        // Fade out exploration music
        StartCoroutine(FadeOutCurrentTrack(1.0f));
        
        // Start combat music with appropriate intensity
        CombatIntensity intensity = CalculateCombatIntensity();
        PlayCombatMusic(intensity);
    }
    
    public void UpdateCombatIntensity()
    {
        // Adjust music based on combat state
        // More intense as characters get low on HP
        // Dramatic shifts during status effect combinations
        CombatIntensity newIntensity = CalculateCombatIntensity();
        SmoothTransitionToCombatLayer(newIntensity);
    }
}

public enum CombatIntensity
{
    Low,        // Start of combat, everyone healthy
    Medium,     // Some damage taken, status effects active
    High,       // Low HP, dangerous situation
    Critical    // Near death, desperate situation
}
```

#### Area-Specific Audio:
```csharp
[CreateAssetMenu(fileName = "Area Audio Profile", menuName = "Game/Audio Profile")]
public class AreaAudioProfile : ScriptableObject
{
    [Header("Music")]
    public AudioClip explorationMusic;
    public AudioClip ambientLoop;
    public float musicVolume = 0.7f;
    
    [Header("Environmental Audio")]
    public List<AudioClip> ambientSounds; // Wind, water, etc.
    public AudioClip footstepOverride;     // Different surfaces
    public AudioClip environmentalLoop;    // Cave echoes, forest sounds
    
    [Header("Combat Overrides")]
    public AudioClip areaCombatMusic;      // Special combat music for this area
    public bool useGlobalCombat = true;    // Or use global combat music
}
```

### Audio Feedback Systems

#### Status Effect Audio:
```csharp
public class StatusEffectAudioManager : MonoBehaviour
{
    [System.Serializable]
    public class StatusAudioProfile
    {
        public StatusEffectType statusType;
        public AudioClip applicationSound;   // When effect is applied
        public AudioClip processSound;       // Each turn the effect triggers
        public AudioClip removalSound;      // When effect ends
        public bool loopWhileActive;        // For ongoing effects
    }
    
    public List<StatusAudioProfile> statusAudioProfiles;
    
    public void PlayStatusEffectAudio(StatusEffectType type, StatusAudioEvent eventType)
    {
        StatusAudioProfile profile = statusAudioProfiles.Find(p => p.statusType == type);
        if (profile != null)
        {
            AudioClip clipToPlay = eventType switch
            {
                StatusAudioEvent.Applied => profile.applicationSound,
                StatusAudioEvent.Processed => profile.processSound,
                StatusAudioEvent.Removed => profile.removalSound,
                _ => null
            };
            
            if (clipToPlay != null)
            {
                AudioSource.PlayClipAtPoint(clipToPlay, Camera.main.transform.position);
            }
        }
    }
}

public enum StatusAudioEvent { Applied, Processed, Removed }
```

#### Combat Audio Cues:
- **Turn Order Changes:** Distinct sound when speed effects alter turn queue
- **Damage Types:** Different audio for physical, fire, ice, electric damage
- **Armor Absorption:** Satisfying "clank" when armor blocks damage
- **Spikes Reflection:** Sharp "ping" when damage is reflected
- **Critical Hits:** Dramatic impact sound
- **Item Skills:** Unique audio signature for each skill type

### Visual Feedback Guidelines

#### Status Effect Visualization:
```csharp
public class StatusEffectVisualManager : MonoBehaviour
{
    [System.Serializable]
    public class StatusVisualProfile
    {
        public StatusEffectType statusType;
        public GameObject particleEffect;     // Ongoing visual effect
        public Color characterTint;           // Tint character while active
        public AnimationClip statusAnimation; // Character animation override
        public Sprite statusIcon;             // UI icon representation
        public bool pulseIcon;               // Should icon pulse/animate?
    }
    
    public void ApplyStatusVisuals(Character character, StatusEffect effect)
    {
        StatusVisualProfile profile = GetVisualProfile(effect.type);
        
        // Apply particle effect
        if (profile.particleEffect != null)
        {
            GameObject particles = Instantiate(profile.particleEffect, character.transform);
            particles.name = $"{effect.type}_particles";
        }
        
        // Apply character tint
        character.GetComponent<SpriteRenderer>().color = profile.characterTint;
        
        // Update UI icon
        character.statusIconUI.SetIcon(profile.statusIcon, profile.pulseIcon);
    }
}
```

#### Combat Feedback:
- **Damage Numbers:** Color-coded floating text (red physical, blue ice, etc.)
- **Screen Effects:** Subtle screen shake for heavy hits, freeze frame for critical hits
- **Turn Order UI:** Smooth animations when turn queue changes
- **Item Highlighting:** Glow effects for modifier interactions and comparisons
- **Grid Feedback:** Valid/invalid placement indicators, snap-to-grid animations

---

## Balance & Testing Framework

### Power Progression Curves

#### Character Power Growth:
```csharp
public class BalanceCalculator
{
    // Target power progression: exponential growth with plateau periods
    public static float CalculateTargetPower(int characterLevel, int storyProgress)
    {
        float basePower = 100f; // Starting power level
        float levelMultiplier = Mathf.Pow(1.15f, characterLevel); // 15% per "level"
        float storyMultiplier = 1f + (storyProgress * 0.1f); // 10% per major story beat
        
        return basePower * levelMultiplier * storyMultiplier;
    }
    
    // Item tier power scaling
    public static int GetTierStatBonus(ItemTier tier)
    {
        return tier switch
        {
            ItemTier.Common => 5,       // Base power
            ItemTier.Uncommon => 8,     // 60% increase
            ItemTier.Rare => 13,        // 62% increase  
            ItemTier.Elite => 21,       // 62% increase
            ItemTier.Legendary => 35,   // 67% increase
            ItemTier.Unique => 50,      // 43% increase (unique effects matter more)
            _ => 0
        };
    }
}
```

#### Enemy Scaling Guidelines:
- **Early Game (Story 0-25%):** Enemies slightly below player power, focus on teaching mechanics
- **Mid Game (Story 25-75%):** Enemies match player power, require tactical thinking
- **Late Game (Story 75-100%):** Enemies above player power, require optimization

### Economic Balance

#### Pricing Guidelines:
```csharp
public class EconomicBalance
{
    // Grid upgrade cost progression (exponential)
    public static int CalculateGridUpgradeCost(int currentSlots)
    {
        int baseSlots = 20; // Starting inventory size
        int additionalSlots = currentSlots - baseSlots;
        
        if (additionalSlots <= 0) return 0;
        
        int baseCost = 100;
        float multiplier = 1.5f;
        
        return Mathf.RoundToInt(baseCost * Mathf.Pow(multiplier, additionalSlots));
    }
    
    // Item pricing based on tier and type
    public static int CalculateItemPrice(Item item)
    {
        int basePriceByCategory = item.category switch
        {
            ItemCategory.Consumable => 10,
            ItemCategory.Gem => 25,
            ItemCategory.Sword => 50,
            ItemCategory.Armor => 75,
            _ => 30
        };
        
        float tierMultiplier = item.tier switch
        {
            ItemTier.Common => 1.0f,
            ItemTier.Uncommon => 2.5f,
            ItemTier.Rare => 6.0f,
            ItemTier.Elite => 15.0f,
            ItemTier.Legendary => 40.0f,
            ItemTier.Unique => 100.0f, // Rarely sold
            _ => 1.0f
        };
        
        return Mathf.RoundToInt(basePriceByCategory * tierMultiplier);
    }
}
```

#### Gold Income Balance:
- **Combat Rewards:** 10-50 gold per encounter (scaling with enemy difficulty)
- **Side Quest Rewards:** 100-500 gold (meaningful but not game-breaking)
- **Exploration Rewards:** 25-100 gold (treasure chests, hidden areas)
- **Target Economy:** Player should afford 1-2 grid upgrades per major story area

### Testing Framework

#### Automated Balance Testing:
```csharp
public class BalanceTestSuite : MonoBehaviour
{
    [System.Serializable]
    public class BalanceTest
    {
        public string testName;
        public Character testCharacter;
        public Enemy testEnemy;
        public float expectedWinRate; // 0.0 to 1.0
        public int testIterations = 100;
    }
    
    public List<BalanceTest> balanceTests;
    
    public void RunAllBalanceTests()
    {
        foreach (BalanceTest test in balanceTests)
        {
            RunBalanceTest(test);
        }
    }
    
    private void RunBalanceTest(BalanceTest test)
    {
        int wins = 0;
        
        for (int i = 0; i < test.testIterations; i++)
        {
            // Simulate combat
            bool playerWon = SimulateCombat(test.testCharacter, test.testEnemy);
            if (playerWon) wins++;
        }
        
        float actualWinRate = (float)wins / test.testIterations;
        float deviation = Mathf.Abs(actualWinRate - test.expectedWinRate);
        
        if (deviation > 0.1f) // 10% tolerance
        {
            Debug.LogWarning($"Balance Test '{test.testName}' failed: Expected {test.expectedWinRate:P}, Got {actualWinRate:P}");
        }
    }
}
```

#### Playtesting Metrics:
```csharp
public class PlaytestingMetrics : MonoBehaviour
{
    [System.Serializable]
    public class SessionMetrics
    {
        public float sessionDuration;
        public int combatsWon;
        public int combatsLost;
        public int itemsLooted;
        public int gridUpgradesPurchased;
        public int timesUndoUsed;
        public Dictionary<string, int> statusEffectUsage;
        public List<string> playerDeaths; // What killed the player
    }
    
    public void RecordMetric(string metricName, float value)
    {
        // Track player behavior and game balance
        // Export data for analysis
    }
    
    public void AnalyzeSessionData()
    {
        // Identify balance issues:
        // - Too many deaths to specific enemies
        // - Excessive undo usage (UI issues)
        // - Low status effect usage (mechanics not engaging)
        // - Rapid or slow progression (pacing issues)
    }
}
```

### Difficulty Validation

#### Encounter Testing:
- **Solo Testing:** Each encounter should be beatable with "average" gear for that story point
- **Tactical Testing:** Multiple valid strategies should exist for each encounter
- **Edge Case Testing:** Encounters should not become trivial with optimized builds
- **Accessibility Testing:** Encounters should be manageable with different playstyles

#### Item Balance Validation:
- **Power Level Consistency:** Items within same tier should offer roughly equivalent power
- **Meaningful Choices:** No item should be strictly superior to all others in its tier
- **Interaction Testing:** Item combinations should not break encounter balance
- **Economy Testing:** Purchasing power should match intended progression pace

### Smart Inventory Management

#### Auto-Organization Suggestions:
```csharp
public class InventoryOptimizer : MonoBehaviour
{
    public struct OptimizationSuggestion
    {
        public List<ItemPlacement> suggestedPlacements;
        public float efficiencyScore; // 0.0 to 1.0
        public string description;
    }
    
    public OptimizationSuggestion SuggestOptimalLayout(Character character)
    {
        List<Item> allItems = character.inventory.GetAllItems();
        List<ItemPlacement> bestPlacements = new List<ItemPlacement>();
        
        // Priority order: PassiveGear first, then ActiveTools, then Modifiers
        var prioritizedItems = allItems
            .OrderBy(item => GetPlacementPriority(item))
            .ThenByDescending(item => item.tier);
        
        foreach (Item item in prioritizedItems)
        {
            Vector2Int bestPosition = FindOptimalPosition(item, bestPlacements);
            if (bestPosition != Vector2Int.zero)
            {
                bestPlacements.Add(new ItemPlacement(item, bestPosition, 0));
            }
        }
        
        return new OptimizationSuggestion
        {
            suggestedPlacements = bestPlacements,
            efficiencyScore = CalculateEfficiencyScore(bestPlacements),
            description = "Optimized for maximum modifier coverage"
        };
    }
    
    private int GetPlacementPriority(Item item)
    {
        return item.type switch
        {
            ItemType.PassiveGear => 1,    // Highest priority
            ItemType.ActiveTool => 2,     // Second priority
            ItemType.Modifier => 3,       // Place last for optimal coverage
            ItemType.Consumable => 4,     // Lowest priority
            _ => 5
        };
    }
}
```

#### Quick Actions System:
```csharp
public class QuickActionManager : MonoBehaviour
{
    [System.Serializable]
    public class QuickAction
    {
        public KeyCode hotkey;
        public string actionName;
        public System.Action actionCallback;
        public bool requiresSelectedItem;
    }
    
    public List<QuickAction> quickActions = new List<QuickAction>
    {
        new QuickAction { hotkey = KeyCode.R, actionName = "Rotate Item", requiresSelectedItem = true },
        new QuickAction { hotkey = KeyCode.Z, actionName = "Undo", requiresSelectedItem = false },
        new QuickAction { hotkey = KeyCode.F5, actionName = "Quick Save", requiresSelectedItem = false },
        new QuickAction { hotkey = KeyCode.Space, actionName = "Auto-Sort Suggestions", requiresSelectedItem = false },
        new QuickAction { hotkey = KeyCode.C, actionName = "Compare Items", requiresSelectedItem = true }
    };
    
    private void Update()
    {
        foreach (QuickAction action in quickActions)
        {
            if (Input.GetKeyDown(action.hotkey))
            {
                if (!action.requiresSelectedItem || HasSelectedItem())
                {
                    action.actionCallback?.Invoke();
                }
            }
        }
    }
}
```

### Enhanced Tooltips & Information

#### Context-Aware Tooltips:
```csharp
public class AdvancedTooltipSystem : MonoBehaviour
{
    public void ShowItemTooltip(Item item, Vector3 position, TooltipContext context)
    {
        StringBuilder tooltipText = new StringBuilder();
        
        // Basic item info
        tooltipText.AppendLine($"{item.displayName} {GetTierIndicator(item.tier)}");
        tooltipText.AppendLine($"{item.description}");
        tooltipText.AppendLine();
        
        // Context-specific information
        switch (context)
        {
            case TooltipContext.Inventory:
                AddInventorySpecificInfo(tooltipText, item);
                break;
            case TooltipContext.Shop:
                AddShopSpecificInfo(tooltipText, item);
                break;
            case TooltipContext.Reward:
                AddRewardSpecificInfo(tooltipText, item);
                break;
            case TooltipContext.Comparison:
                AddComparisonInfo(tooltipText, item);
                break;
        }
        
        // Show modifier effects if applicable
        if (item.type == ItemType.Modifier)
        {
            AddModifierEffectInfo(tooltipText, item);
        }
        
        // Show compatibility warnings
        if (!CanCurrentCharacterUse(item))
        {
            tooltipText.AppendLine($"<color=red>⚠ {GetCurrentCharacter().characterType} cannot use this item</color>");
        }
        
        DisplayTooltip(tooltipText.ToString(), position);
    }
    
    private void AddInventorySpecificInfo(StringBuilder text, Item item)
    {
        // Show current stat contributions
        text.AppendLine("Current Effects:");
        foreach (StatModifier modifier in item.statModifiers)
        {
            text.AppendLine($"  {modifier.statType}: +{modifier.value}");
        }
        
        // Show active modifiers affecting this item
        List<Item> affectingModifiers = GetModifiersAffecting(item);
        if (affectingModifiers.Count > 0)
        {
            text.AppendLine("Enhanced by:");
            foreach (Item modifier in affectingModifiers)
            {
                text.AppendLine($"  • {modifier.displayName}");
            }
        }
    }
}

public enum TooltipContext
{
    Inventory,    // Hovering in character inventory
    Shop,         // Hovering in shop interface
    Reward,       // Hovering in combat rewards
    Comparison,   // Hovering during comparison mode
    Storage       // Hovering in party storage
}
```

### Progressive Tutorial System

#### Contextual Help:
```csharp
public class ContextualHelpManager : MonoBehaviour
{
    [System.Serializable]
    public class HelpTrigger
    {
        public string triggerName;
        public GameEvent triggerEvent;
        public string helpText;
        public Sprite helpImage;
        public bool showOnce;
        public bool hasBeenShown;
    }
    
    public List<HelpTrigger> helpTriggers;
    
    public void OnGameEvent(GameEvent eventType)
    {
        HelpTrigger trigger = helpTriggers.Find(h => h.triggerEvent == eventType);
        
        if (trigger != null && (!trigger.showOnce || !trigger.hasBeenShown))
        {
            ShowContextualHelp(trigger);
            trigger.hasBeenShown = true;
        }
    }
    
    private void ShowContextualHelp(HelpTrigger trigger)
    {
        // Non-intrusive help popup
        UIManager.ShowHelpPopup(trigger.helpText, trigger.helpImage, 3.0f); // Auto-dismiss after 3 seconds
    }
}

public enum GameEvent
{
    FirstItemPickup,
    FirstModifierPlacement,
    FirstItemRotation,
    FirstCombatReward,
    FirstShopVisit,
    FirstStatusEffect,
    FirstSpeedChange,
    InventoryFull,
    FirstComparison
}
```

### Advanced Shop System

#### Merchant-Specific Drag & Drop:
```csharp
public class MerchantDragDropSystem : MonoBehaviour
{
    private bool isDraggingFromShop = false;
    private Item originalShopItem;
    private int originalShopSlot;
    
    public void OnStartDragFromShop(Item item, int shopSlot)
    {
        isDraggingFromShop = true;
        originalShopItem = item;
        originalShopSlot = shopSlot;
        
        // Create visual copy for dragging
        CreateDragVisual(item);
        
        // Item remains in shop during drag
    }
    
    public void OnDropToInventory(Vector2Int gridPosition)
    {
        if (isDraggingFromShop)
        {
            bool canAfford = PlayerGold >= CalculateItemPrice(originalShopItem);
            bool canPlace = CurrentCharacter.inventory.CanPlaceItem(originalShopItem, gridPosition, 0);
            
            if (canAfford && canPlace)
            {
                // Complete purchase
                PlayerGold -= CalculateItemPrice(originalShopItem);
                CurrentCharacter.inventory.PlaceItem(originalShopItem, gridPosition, 0);
                
                // Update shop stock
                shopInventory.ReduceStock(originalShopSlot, 1);
            }
            else
            {
                // Show error message
                string error = !canAfford ? "Not enough gold!" : "Cannot place item here!";
                UIManager.ShowErrorMessage(error);
            }
            
            CleanupDragOperation();
        }
    }
    
    public void OnDropToStorage()
    {
        if (isDraggingFromShop)
        {
            // Direct purchase to party storage
            bool canAfford = PlayerGold >= CalculateItemPrice(originalShopItem);
            
            if (canAfford)
            {
                PlayerGold -= CalculateItemPrice(originalShopItem);
                PartyStorage.AddItem(originalShopItem);
                shopInventory.ReduceStock(originalShopSlot, 1);
            }
            
            CleanupDragOperation();
        }
    }
}
```

---

## Error Handling & Edge Cases

### Save System Resilience

#### Corrupted Save Recovery:
```csharp
public class RobustSaveSystem : MonoBehaviour
{
    private List<string> saveFilePaths;
    private const string SAVE_DIRECTORY = "Saves";
    private const string BACKUP_SUFFIX = "_backup";
    
    public bool LoadGame(string saveName)
    {
        string primaryPath = GetSavePath(saveName);
        string backupPath = GetSavePath(saveName + BACKUP_SUFFIX);
        
        // Try primary save first
        GameSaveData saveData = TryLoadSaveFile(primaryPath);
        if (saveData != null && ValidateSaveData(saveData))
        {
            ApplySaveData(saveData);
            return true;
        }
        
        // Primary failed, try backup
        Debug.LogWarning($"Primary save '{saveName}' corrupted or invalid. Trying backup...");
        saveData = TryLoadSaveFile(backupPath);
        if (saveData != null && ValidateSaveData(saveData))
        {
            ApplySaveData(saveData);
            UIManager.ShowMessage("Loaded from backup save due to corruption.");
            return true;
        }
        
        // Both failed, try previous save
        return TryLoadPreviousSave(saveName);
    }
    
    private bool TryLoadPreviousSave(string failedSaveName)
    {
        // Get all saves for this profile, sorted by date
        List<string> availableSaves = GetAllSaves()
            .Where(s => s != failedSaveName)
            .OrderByDescending(s => GetSaveTimestamp(s))
            .ToList();
        
        foreach (string saveName in availableSaves)
        {
            GameSaveData saveData = TryLoadSaveFile(GetSavePath(saveName));
            if (saveData != null && ValidateSaveData(saveData))
            {
                ApplySaveData(saveData);
                UIManager.ShowMessage($"Loaded previous save '{saveName}' due to corruption.");
                return true;
            }
        }
        
        // No valid saves found
        UIManager.ShowErrorMessage("No valid save files found. Starting new game.");
        return false;
    }
    
    private bool ValidateSaveData(GameSaveData saveData)
    {
        // Validate critical save data integrity
        if (saveData.partyMembers == null || saveData.partyMembers.Count == 0)
            return false;
        
        if (string.IsNullOrEmpty(saveData.currentScene))
            return false;
        
        if (saveData.playerGold < 0)
            return false;
        
        // Validate character data
        foreach (Character character in saveData.partyMembers)
        {
            if (!ValidateCharacterData(character))
                return false;
        }
        
        return true;
    }
    
    public void SaveGame(string saveName)
    {
        // Create backup of existing save first
        string primaryPath = GetSavePath(saveName);
        string backupPath = GetSavePath(saveName + BACKUP_SUFFIX);
        
        if (File.Exists(primaryPath))
        {
            File.Copy(primaryPath, backupPath, true);
        }
        
        // Save new data
        GameSaveData saveData = GatherSaveData();
        WriteSaveFile(primaryPath, saveData);
    }
}
```

### Inventory Edge Cases

#### Full Inventory Handling:
```csharp
public class InventoryOverflowManager : MonoBehaviour
{
    public void HandleLootWithFullInventory(List<Item> lootItems)
    {
        List<Item> placedItems = new List<Item>();
        List<Item> overflowItems = new List<Item>();
        
        // Try to place items in available inventory slots
        foreach (Item item in lootItems)
        {
            bool placed = false;
            
            // Try each character's inventory
            foreach (Character character in PartyManager.GetActiveParty())
            {
                if (character.inventory.CanPlaceAnywhereWithRotation(item))
                {
                    Vector2Int position = character.inventory.FindBestPosition(item);
                    character.inventory.PlaceItem(item, position, 0);
                    placedItems.Add(item);
                    placed = true;
                    break;
                }
            }
            
            if (!placed)
            {
                overflowItems.Add(item);
            }
        }
        
        // Handle overflow
        if (overflowItems.Count > 0)
        {
            ShowOverflowDialog(placedItems, overflowItems);
        }
        else
        {
            // All items placed successfully
            LootUIManager.ShowLootSummary(placedItems);
        }
    }
    
    private void ShowOverflowDialog(List<Item> placedItems, List<Item> overflowItems)
    {
        OverflowDialog dialog = UIManager.CreateOverflowDialog();
        dialog.SetItems(placedItems, overflowItems);
        
        dialog.OnSendToStorage += () =>
        {
            foreach (Item item in overflowItems)
            {
                PartyStorage.AddItem(item);
            }
            dialog.Close();
        };
        
        dialog.OnReorganize += () =>
        {
            // Open inventory management with overflow items highlighted
            InventoryManager.OpenWithOverflowItems(overflowItems);
            dialog.Close();
        };
        
        dialog.OnDiscard += (List<Item> discardedItems) =>
        {
            // Items are lost forever
            UIManager.ShowMessage($"Discarded {discardedItems.Count} items.");
            dialog.Close();
        };
    }
}
```

#### Combat Reward Edge Cases:
```csharp
public class CombatRewardEdgeCaseHandler : MonoBehaviour
{
    public void HandleFullInventoryRewards(List<Item> selectedRewards)
    {
        List<Item> canPlace = new List<Item>();
        List<Item> mustStore = new List<Item>();
        
        // Categorize selected rewards
        foreach (Item item in selectedRewards)
        {
            if (CanPlaceInAnyInventory(item))
            {
                canPlace.Add(item);
            }
            else
            {
                mustStore.Add(item);
            }
        }
        
        if (mustStore.Count > 0)
        {
            // Show forced storage dialog
            ForceStorageDialog dialog = UIManager.CreateForceStorageDialog();
            dialog.SetMessage($"{mustStore.Count} items will be sent to storage due to full inventories.");
            dialog.SetItems(canPlace, mustStore);
            
            dialog.OnConfirm += () =>
            {
                // Place what we can, store the rest
                foreach (Item item in canPlace)
                {
                    PlaceInBestInventory(item);
                }
                
                foreach (Item item in mustStore)
                {
                    PartyStorage.AddItem(item);
                }
                
                dialog.Close();
                OpenLootReorganization();
            };
        }
        else
        {
            // Normal loot flow
            OpenLootReorganization();
        }
    }
}
```

### Performance Safeguards

#### Inventory Size Limits:
```csharp
public class PerformanceManager : MonoBehaviour
{
    private const int MAX_GRID_DIMENSION = 12;
    private const int MAX_TOTAL_ITEMS_PER_CHARACTER = 50;
    private const int MAX_PARTY_STORAGE_ITEMS = 1000;
    
    public bool ValidateGridExpansion(Character character, Vector2Int newSize)
    {
        if (newSize.x > MAX_GRID_DIMENSION || newSize.y > MAX_GRID_DIMENSION)
        {
            UIManager.ShowErrorMessage($"Grid cannot exceed {MAX_GRID_DIMENSION}x{MAX_GRID_DIMENSION}");
            return false;
        }
        
        int totalSlots = newSize.x * newSize.y;
        if (totalSlots > MAX_GRID_DIMENSION * MAX_GRID_DIMENSION)
        {
            UIManager.ShowErrorMessage("Grid too large for optimal performance");
            return false;
        }
        
        return true;
    }
    
    public void MonitorPerformance()
    {
        // Track performance metrics
        float frameTime = Time.deltaTime;
        int totalItems = GetTotalItemCount();
        
        if (frameTime > 0.033f && totalItems > 200) // 30 FPS threshold
        {
            Debug.LogWarning("Performance degradation detected with high item count");
            SuggestOptimization();
        }
    }
    
    private void SuggestOptimization()
    {
        UIManager.ShowOptimizationSuggestion(
            "Large number of items detected. Consider storing unused items to improve performance."
        );
    }
}
```

### Data Integrity Validation

#### Item Database Validation:
```csharp
public class DataIntegrityValidator : MonoBehaviour
{
    [System.Serializable]
    public class ValidationResult
    {
        public bool isValid;
        public List<string> errors;
        public List<string> warnings;
    }
    
    public ValidationResult ValidateItemDatabase()
    {
        ValidationResult result = new ValidationResult
        {
            errors = new List<string>(),
            warnings = new List<string>()
        };
        
        List<Item> allItems = ItemDatabase.GetAllItems();
        
        foreach (Item item in allItems)
        {
            ValidateItem(item, result);
        }
        
        // Check for duplicate IDs
        var duplicateIds = allItems
            .GroupBy(i => i.itemId)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key);
        
        foreach (string duplicateId in duplicateIds)
        {
            result.errors.Add($"Duplicate item ID found: {duplicateId}");
        }
        
        result.isValid = result.errors.Count == 0;
        return result;
    }
    
    private void ValidateItem(Item item, ValidationResult result)
    {
        // Required fields
        if (string.IsNullOrEmpty(item.itemId))
            result.errors.Add($"Item missing ID: {item.name}");
        
        if (string.IsNullOrEmpty(item.displayName))
            result.errors.Add($"Item missing display name: {item.itemId}");
        
        if (item.icon == null)
            result.warnings.Add($"Item missing icon: {item.itemId}");
        
        // Shape validation
        if (item.shape.occupiedCells == null || item.shape.occupiedCells.Length == 0)
            result.errors.Add($"Item has no shape defined: {item.itemId}");
        
        // Modifier validation
        if (item.type == ItemType.Modifier && item.modifierZones == null)
            result.warnings.Add($"Modifier item has no modifier zones: {item.itemId}");
        
        // Price validation
        int calculatedPrice = EconomicBalance.CalculateItemPrice(item);
        if (calculatedPrice <= 0)
            result.warnings.Add($"Item has invalid calculated price: {item.itemId}");
    }
}
```

---

## Development Phases

### Updated Phase Structure

#### Phase 1: Foundation & Core Systems (Weeks 1-8)
**Week 1-2: Project Setup**
- Unity project initialization with proper folder structure
- Input system setup (mouse/keyboard + controller)
- Basic scene management and transitions
- Core data structures (Item, Character, SaveData)

**Week 3-4: Basic Inventory System**
- Grid-based inventory implementation
- Drag-and-drop mechanics (mouse only initially)
- Item placement validation and rotation
- Basic undo system for item placement

**Week 5-6: Combat Framework**
- Turn-based combat structure
- Dynamic turn order calculation
- Basic status effect system
- Simple combat UI

**Week 7-8: Save System & Data Management**
- Save/load functionality with corruption handling
- Multiple save slots with automatic naming
- Basic item database structure
- Data validation systems

#### Phase 2: Advanced Gameplay Systems (Weeks 9-20)
**Week 9-10: Enhanced Inventory**
- Modifier system with item interactions
- Live stat calculation and preview
- Tab system and basic filtering
- Controller support for inventory navigation

**Week 11-12: Complete Combat System**
- All status effects with interactions
- Armor points and spikes reflection
- Turn-decaying effects
- Combat reward selection system

**Week 13-14: Shop & Economy**
- Basic shop system with pricing
- Grid upgrade purchases
- Economic balance implementation
- Merchant-specific drag-and-drop

**Week 15-16: Crafting & Progression**
- Item upgrade system (combine identical items)
- Blueprint-based crafting
- Item skill system
- Character restriction validation

**Week 17-18: Tutorial & Onboarding**
- Progressive tutorial area design
- Contextual help system
- Guided mechanic introduction
- Tutorial skip options

**Week 19-20: Quality of Life Features**
- Item comparison system
- Filter improvements and search alternatives
- Auto-organization suggestions
- Hotkey system and quick actions

#### Phase 3: Polish & Content Integration (Weeks 21-32)
**Week 21-22: Audio System**
- Dynamic music system with area transitions
- Status effect audio cues
- Combat feedback sounds
- Environmental audio profiles

**Week 23-24: Visual Polish**
- Status effect visual feedback
- UI animations and transitions
- Particle effects for combat
- Screen effects and camera shake

**Week 25-26: Balance Framework Implementation**
- Automated balance testing suite
- Economic balance validation
- Power progression curve implementation
- Playtesting metrics collection

**Week 27-28: Accessibility & Control Polish**
- Controller optimization for all systems
- Colorblind accessibility features
- Performance optimization
- Error handling robustness

**Week 29-30: Content Creation Tools**
- Editor utilities for easy item creation
- Template systems for rapid content development
- Batch creation tools
- Data integrity validation tools

**Week 31-32: Final Polish & Testing**
- Comprehensive playtesting
- Bug fixing and edge case handling
- Performance optimization
- Steam Deck compatibility validation

#### Phase 4: Content Creation & Launch Preparation (Weeks 33-40)
**Week 33-34: Core Game Content**
- Main story areas and encounters
- Character roster implementation
- Essential item database population
- Side quest design and implementation

**Week 35-36: Content Balance & Iteration**
- Encounter difficulty tuning
- Economic balance validation
- Progression pacing adjustments
- Content accessibility testing

**Week 37-38: Localization & Platform Preparation**
- Text localization framework
- Steam Deck optimization
- Platform-specific testing
- Achievement/progression system

**Week 39-40: Launch Preparation**
- Final bug fixes
- Performance optimization
- Marketing asset creation
- Steam store page preparation

### Milestone Deliverables

#### Phase 1 Milestones:
- **M1.1:** Basic inventory with drag-and-drop working
- **M1.2:** Simple combat with turn order
- **M1.3:** Save/load system functional
- **M1.4:** Core data structures complete

#### Phase 2 Milestones:
- **M2.1:** Complete inventory system with modifiers
- **M2.2:** Full combat with all status effects
- **M2.3:** Shop system operational
- **M2.4:** Tutorial system implemented

#### Phase 3 Milestones:
- **M3.1:** Audio system complete
- **M3.2:** Balance framework operational
- **M3.3:** All quality of life features implemented
- **M3.4:** Accessibility compliance achieved

#### Phase 4 Milestones:
- **M4.1:** Core content complete
- **M4.2:** Balance validated through testing
- **M4.3:** Platform compatibility confirmed
- **M4.4:** Launch-ready build achieved

---

## Performance Considerations

### POC Performance Framework (Primitive-Optimized)

#### Unity Primitive Rendering Advantages:
```csharp
public class PrimitivePerformanceManager : MonoBehaviour
{
    [Header("Primitive Optimization")]
    public int maxVisiblePrimitives = 1000;
    public float cullingDistance = 100f;
    public bool enableGPUInstancing = true;
    public bool enableStaticBatching = true;
    
    [Header("LOD Settings")]
    public float highDetailDistance = 20f;
    public float mediumDetailDistance = 50f;
    public float lowDetailDistance = 100f;
    
    private Dictionary<PrimitiveType, MaterialPropertyBlock> materialBlocks;
    private Dictionary<string, ObjectPool> primitiveObjectPools;
    
    private void Start()
    {
        OptimizePrimitiveRendering();
        SetupObjectPools();
    }
    
    private void OptimizePrimitiveRendering()
    {
        // Enable GPU instancing for identical primitive meshes
        Graphics.DrawMeshInstanced(cubeMesh, 0, cubeMaterial, cubeTransforms);
        
        // Use static batching for environment objects
        StaticBatchingUtility.Combine(staticEnvironmentObjects, gameObject);
        
        // Enable frustum culling for distant objects
        Camera.main.layerCullDistances = CalculateCullDistances();
    }
}
```

#### Memory Management Strategy (Primitive-Based):
```csharp
public class PrimitiveMemoryManager : MonoBehaviour
{
    [Header("Memory Limits")]
    private const int MAX_PRIMITIVE_INSTANCES = 2000;
    private const int MAX_MATERIAL_VARIANTS = 50;
    private const int MAX_ACTIVE_EFFECTS = 100;
    
    [Header("Object Pooling")]
    private ObjectPool<GameObject> cubePool;
    private ObjectPool<GameObject> spherePool;
    private ObjectPool<GameObject> capsulePool;
    private ObjectPool<GameObject> cylinderPool;
    
    public void ValidateMemoryUsage()
    {
        int activePrimitives = GetActivePrimitiveCount();
        int activeMaterials = GetActiveMaterialCount();
        
        if (activePrimitives > MAX_PRIMITIVE_INSTANCES)
        {
            Debug.LogWarning($"High primitive count: {activePrimitives}/{MAX_PRIMITIVE_INSTANCES}");
            OptimizePrimitiveUsage();
        }
        
        if (activeMaterials > MAX_MATERIAL_VARIANTS)
        {
            Debug.Log("Consolidating materials for better batching");
            ConsolidateMaterials();
        }
    }
    
    private void OptimizePrimitiveUsage()
    {
        // Disable distant primitives
        // Pool inactive objects
        // Reduce LOD for far objects
        CullDistantPrimitives();
        ReturnInactivePrimitivesToPool();
    }
}
```

#### Rendering Performance (Primitive Advantages):
- **High Polygon Efficiency:** Unity primitives are optimized meshes
- **Excellent Batching:** Identical primitives batch automatically
- **Minimal Draw Calls:** Same material primitives render in single pass
- **GPU Instancing:** Thousands of identical objects render efficiently
- **No Texture Memory:** Solid color materials use minimal VRAM
- **Fast Lighting:** Simple geometry calculates lighting quickly

#### LOD System for Primitives:
```csharp
public class PrimitiveLODSystem : MonoBehaviour
{
    [System.Serializable]
    public class PrimitiveLOD
    {
        public float distance;
        public int vertexReduction; // For procedural primitive generation
        public bool enableShadows;
        public bool enableCollision;
        public MaterialQuality materialQuality;
    }
    
    public enum MaterialQuality
    {
        High,    // Full material with emission/effects
        Medium,  // Diffuse only
        Low      // Solid color only
    }
    
    public List<PrimitiveLOD> lodLevels = new List<PrimitiveLOD>
    {
        new PrimitiveLOD { distance = 20f, vertexReduction = 0, enableShadows = true, enableCollision = true, materialQuality = MaterialQuality.High },
        new PrimitiveLOD { distance = 50f, vertexReduction = 25, enableShadows = true, enableCollision = false, materialQuality = MaterialQuality.Medium },
        new PrimitiveLOD { distance = 100f, vertexReduction = 50, enableShadows = false, enableCollision = false, materialQuality = MaterialQuality.Low }
    };
    
    private void Update()
    {
        float distanceToCamera = Vector3.Distance(transform.position, Camera.main.transform.position);
        ApplyLODForDistance(distanceToCamera);
    }
}
```

### Scalability Considerations (POC to Production):

#### Primitive Asset Scalability:
- **Current Capacity:** 1000+ simultaneous primitive objects at 60fps
- **Grid Size Limits:** 12×12 per character (144 cells) remains optimal
- **Combat Participants:** Up to 8 characters in combat without performance impact
- **Status Effect Limits:** 50+ simultaneous effects with visual feedback
- **Save File Performance:** Fast serialization with primitive data structures

#### Platform-Specific Optimizations:

**Steam Deck Optimization:**
```csharp
public class SteamDeckOptimizer : MonoBehaviour
{
    private void Start()
    {
        if (SystemInfo.deviceName.Contains("Steam Deck"))
        {
            // Optimize for Steam Deck hardware
            QualitySettings.SetQualityLevel(2); // Medium quality
            Application.targetFrameRate = 60;
            
            // Reduce primitive density for handheld performance
            PrimitivePerformanceManager.maxVisiblePrimitives = 500;
            
            // Optimize battery life
            Screen.brightness = 0.8f;
            
            // Enable dynamic batching for better mobile GPU performance
            QualitySettings.enableLODCrossFade = false;
        }
    }
}
```

**Desktop Optimization:**
```csharp
public class DesktopOptimizer : MonoBehaviour
{
    private void Start()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer || 
            Application.platform == RuntimePlatform.LinuxPlayer)
        {
            // Take advantage of desktop hardware
            QualitySettings.SetQualityLevel(4); // High quality
            Application.targetFrameRate = -1; // Unlimited FPS
            
            // Enable advanced effects for desktop
            EnableAdvancedLighting();
            EnableParticleEffects();
            EnablePostProcessing();
        }
    }
}
```

### POC Performance Benchmarks:

#### Target Performance Metrics:
```
Desktop (1080p):
- 60+ FPS with 1000 visible primitives
- <100ms inventory operations
- <16ms combat calculations
- <500ms save/load operations

Steam Deck (800p):
- 60 FPS with 500 visible primitives  
- <150ms inventory operations
- <20ms combat calculations
- <750ms save/load operations

Memory Usage:
- <2GB RAM total
- <500MB VRAM usage
- <50MB save file sizes
- <1ms garbage collection spikes
```

#### Optimization Priorities:
```
1. Primitive rendering efficiency (batching, instancing)
2. Inventory grid calculations (spatial optimization)
3. Status effect processing (efficient state management)
4. Combat turn order updates (minimal recalculation)
5. Save/load serialization (compressed data structures)
```

### Testing & Profiling Framework:

#### Automated Performance Testing:
```csharp
public class PerformanceTestSuite : MonoBehaviour
{
    [Header("Test Scenarios")]
    public int maxPrimitivesTest = 2000;
    public int maxInventoryItemsTest = 200;
    public int maxStatusEffectsTest = 100;
    public int maxCombatParticipantsTest = 10;
    
    public void RunPerformanceTests()
    {
        StartCoroutine(TestPrimitiveRendering());
        StartCoroutine(TestInventoryPerformance());
        StartCoroutine(TestCombatCalculations());
        StartCoroutine(TestSaveLoadPerformance());
    }
    
    private IEnumerator TestPrimitiveRendering()
    {
        // Spawn increasing numbers of primitives
        // Measure FPS at each threshold
        // Record maximum sustainable primitive count
        
        for (int count = 100; count <= maxPrimitivesTest; count += 100)
        {
            SpawnPrimitives(count);
            yield return new WaitForSeconds(2f);
            
            float fps = 1f / Time.deltaTime;
            Debug.Log($"Primitives: {count}, FPS: {fps:F1}");
            
            if (fps < 30f)
            {
                Debug.LogWarning($"Performance threshold reached at {count} primitives");
                break;
            }
        }
    }
}
```

#### Real-Time Performance Monitoring:
```csharp
public class PerformanceMonitor : MonoBehaviour
{
    [Header("Performance Display")]
    public bool showPerformanceUI = true;
    public KeyCode toggleKey = KeyCode.F3;
    
    private float frameTime;
    private int primitiveCount;
    private int drawCalls;
    private int activeMaterials;
    
    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            showPerformanceUI = !showPerformanceUI;
        }
        
        frameTime = Time.deltaTime;
        primitiveCount = GetActivePrimitiveCount();
        drawCalls = UnityStats.drawCalls;
        activeMaterials = GetActiveMaterialCount();
    }
    
    private void OnGUI()
    {
        if (showPerformanceUI)
        {
            GUI.Box(new Rect(10, 10, 250, 120), "Performance Monitor");
            GUI.Label(new Rect(20, 35, 200, 20), $"FPS: {1f/frameTime:F1}");
            GUI.Label(new Rect(20, 55, 200, 20), $"Frame Time: {frameTime*1000:F1}ms");
            GUI.Label(new Rect(20, 75, 200, 20), $"Primitives: {primitiveCount}");
            GUI.Label(new Rect(20, 95, 200, 20), $"Draw Calls: {drawCalls}");
            GUI.Label(new Rect(20, 115, 200, 20), $"Materials: {activeMaterials}");
        }
    }
}
```

### Production Scalability Path:

#### Asset Upgrade Path (Primitives → Detailed Models):
```
Phase 1 (POC): Unity primitives with colored materials
Phase 2 (Alpha): Low-poly 3D models replacing key primitives  
Phase 3 (Beta): Medium-poly models with textures
Phase 4 (Release): High-quality models with full material systems

Architecture Support:
- Same object pooling systems work with any mesh
- Same LOD framework applies to detailed models
- Same batching optimizations scale to complex geometry
- Same performance monitoring tracks any asset complexity
```

The primitive-based approach provides an excellent performance foundation that scales naturally to production-quality assets while maintaining all the complex gameplay systems at optimal performance levels.

---

This document serves as the complete reference for the JRPG project architecture and should be updated as the project evolves. All systems are designed to work together cohesively while maintaining modularity for easy content creation and future enhancements.