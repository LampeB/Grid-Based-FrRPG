# POC Step-by-Step Development Guide
## Copy-Paste Prompts for Complete Code Implementation

---

## **📋 How to Use This Guide**

1. **Follow steps in order** - each builds on the previous
2. **Copy the prompt exactly** as written for each step
3. **Create the files/objects** as instructed before moving to next step
4. **Test each milestone** before continuing
5. **Save your project** after each major step

---

## **🎯 Phase 1: Project Foundation (Days 1-3)**

### **Step 1: Unity Project Setup**
**What you do manually:**
1. Create new Unity project (3D template)
2. Set up folder structure as shown in the game design document
3. Import your placeholder item icons into Assets/Art/Items/

**Prompt to give me:**
```
"Create the core data structures for my JRPG POC. I need ItemData ScriptableObject, Character class, BaseStats, and all the enums (ItemTier, ItemCategory, ItemType, StatType). Include item shape support with Vector2Int arrays and rotation."
```

**Expected result:** Core data architecture scripts

---

### **Step 2: Basic Item Database**
**What you do after getting the code:**
1. Create first ItemData assets using the ScriptableObjects
2. Set up 5-10 basic items (health potion, sword, etc.)

**Prompt to give me:**
```
"Create an ItemDatabase manager that can store and retrieve ItemData assets. Include methods to get items by ID, get items by category, and validate item data integrity. Also create a simple inspector tool to verify all items are properly configured."
```

**Expected result:** Item management system

---

### **Step 3: Event System Foundation**
**Prompt to give me:**
```
"Create a Unity ScriptableObject-based event system for my JRPG. I need GameEvent base class, generic GameEvent<T> for passing data, GameEventListener components, and specific events for inventory changes, combat events, and UI interactions."
```

**Expected result:** Clean communication between systems

---

## **🎮 Phase 2: 2D Inventory System (Days 4-7)**

### **Step 4: Grid Inventory Core**
**Prompt to give me:**
```
"Create a GridInventory class that handles item placement in a 2D grid. Support for shaped items (Vector2Int arrays), item rotation, placement validation, and collision detection. Include PlacedItem class to track items with their positions and rotations."
```

**Expected result:** Core inventory logic

---

### **Step 5: Inventory UI System**
**What you do first:**
1. Create basic UI Canvas in scene
2. Create InventorySlot prefab (just an Image component)

**Prompt to give me:**
```
"Create InventoryUI component that displays a GridInventory using Unity UI. Needs to generate grid slots dynamically, show item icons in correct positions, handle multi-cell items, and refresh display when inventory changes. Include InventorySlot component for individual slots."
```

**Expected result:** Visual inventory grid

---

### **Step 6: Basic Drag and Drop**
**Prompt to give me:**
```
"Create a basic drag and drop system for the inventory UI. Items should be draggable from slots, show visual feedback during drag, validate placement on drop, and return to original position if invalid. Include item rotation with R key during drag."
```

**Expected result:** Interactive inventory

---

### **Step 7: Item Modification System**
**Prompt to give me:**
```
"Create the item modification system where gems can affect adjacent items. Include ModifierPattern class for defining modifier zones, stat calculation that applies modifiers to affected items, and visual highlighting of modifier relationships in the UI."
```

**Expected result:** Gem + weapon interactions

---

## **🌍 Phase 3: 3D World Foundation (Days 8-10)**

### **Step 8: Character Controller (Primitive)**
**What you do first:**
1. Create empty scene
2. Add basic plane for ground

**Prompt to give me:**
```
"Create a 3D character controller using Unity primitives. Character should be a capsule with a sphere head, move with WASD, have smooth camera follow, and include a Character component that links to the inventory system. Use materials to color-code different character types."
```

**Expected result:** Moveable 3D character

---

### **Step 9: 3D Item Pickups**
**Prompt to give me:**
```
"Create a 3D item pickup system. Items in the world should be small colored cubes that float slightly. When player approaches, show UI prompt. On interaction, add item to inventory and show loot interface. Include ItemPickup component and integration with the existing inventory system."
```

**Expected result:** Items you can pick up in 3D world

---

### **Step 10: Camera System Foundation**
**Prompt to give me:**
```
"Create a dynamic camera system that can switch between different modes: overworld (top-down), exploration (third-person), and combat (dynamic positioning). Include smooth transitions between modes and basic follow camera for exploration mode."
```

**Expected result:** Camera that changes perspectives

---

## **⚔️ Phase 4: Combat System (Days 11-15)**

### **Step 11: Turn-Based Combat Core**
**What you do first:**
1. Create combat test scene with a few primitive enemies (colored cubes)

**Prompt to give me:**
```
"Create the core turn-based combat system. Include CombatManager that handles turn order based on speed stats, CombatEntity base class for characters and enemies, and turn queue management. Support for multiple combatants and dynamic turn order recalculation."
```

**Expected result:** Basic turn-based combat

---

### **Step 12: Combat Actions**
**Prompt to give me:**
```
"Create combat action system with Attack, Magic, Items, and Defend actions. Include damage calculation, MP costs for magic, item usage from inventory, and basic combat UI to select actions. Actions should affect character stats and trigger appropriate events."
```

**Expected result:** Full combat actions

---

### **Step 13: Status Effects System**
**Prompt to give me:**
```
"Create the status effects system with StatusEffect class, status application and removal, turn-based processing, and visual feedback using material color changes. Include basic effects like Poison, Burn, and Freeze with turn-decaying behavior."
```

**Expected result:** Working status effects

---

### **Step 14: Status Effect Interactions**
**Prompt to give me:**
```
"Implement the status effect interaction system where effects can cancel (freeze removes burn), amplify (oil + fire = double damage), or transform into new effects. Include the interaction rules database and processing logic for complex combinations."
```

**Expected result:** Status effect combinations working

---

### **Step 15: Critical Hit System**
**Prompt to give me:**
```
"Create the critical hit system with deterministic critical chance calculation, equipment-based crit rate and damage modifiers, weapon-specific bonuses, and visual feedback for critical hits. Include the CriticalHitSystem class and integration with combat damage."
```

**Expected result:** Critical hits with equipment scaling

---

## **🛡️ Phase 5: Advanced Combat Features (Days 16-18)**

### **Step 16: Armor and Spikes**
**Prompt to give me:**
```
"Implement armor points and spikes reflection system. Armor should absorb damage until depleted, spikes should reflect damage to attackers, both should decay by 1 each turn. Include visual feedback and integration with the existing damage calculation system."
```

**Expected result:** Defensive mechanics working

---

### **Step 17: Combat Camera Dynamics**
**Prompt to give me:**
```
"Create the dynamic combat camera system that moves around characters based on selected menu (attack, magic, items, defend). Include smooth camera transitions, character positioning changes, and camera profiles for different combat actions."
```

**Expected result:** Cinematic combat camera

---

### **Step 18: Combat Rewards**
**Prompt to give me:**
```
"Implement the combat reward system where players can select up to 2 items from 0-5 generated rewards. Include flexible selection (can change mind until confirmed), enemy loot pools, and integration with the inventory system."
```

**Expected result:** Post-combat loot selection

---

## **💰 Phase 6: Economy and Progression (Days 19-21)**

### **Step 19: Shop System**
**What you do first:**
1. Create a primitive NPC (green capsule) in your test scene

**Prompt to give me:**
```
"Create the shop system with Shop ScriptableObjects, ShopUI for purchasing items, merchant-specific drag and drop (items stay in shop during purchase), and integration with the player's gold system. Include different shop types and item pricing."
```

**Expected result:** Working shop interface

---

### **Step 20: Item Upgrading**
**Prompt to give me:**
```
"Implement the item upgrade system where two identical items can be combined into a higher tier version. Include upgrade validation, tier progression, stat scaling, and UI for the upgrade process."
```

**Expected result:** Item combining system

---

### **Step 21: Blueprint Crafting**
**Prompt to give me:**
```
"Create the blueprint-based crafting system. Include CraftingBlueprint ScriptableObjects, ingredient validation, crafting UI, and integration with the inventory system. Support for complex recipes with multiple ingredients and tier requirements."
```

**Expected result:** Recipe-based crafting

---

## **💾 Phase 7: Persistence and Polish (Days 22-25)**

### **Step 22: Save System**
**Prompt to give me:**
```
"Create a comprehensive save/load system that preserves all game state: character inventories, placed items, story flags, player position, and shop states. Include multiple save slots, save file naming, and corruption recovery. Use JSON serialization for easy debugging."
```

**Expected result:** Complete save/load functionality

---

### **Step 23: Settings System**
**Prompt to give me:**
```
"Create a settings system for the POC with video settings (resolution, fullscreen), audio settings (master, music, SFX volume), and accessibility options (colorblind mode). Include SettingsUI and persistent settings storage."
```

**Expected result:** Game settings menu

---

### **Step 24: Debug Console**
**Prompt to give me:**
```
"Create a debug console system with commands for: giving items, modifying stats, setting story flags, teleporting, spawning enemies, testing critical hits, and clearing inventory. Include toggle with backtick key and command history."
```

**Expected result:** Powerful debugging tools

---

### **Step 25: Performance Monitor**
**Prompt to give me:**
```
"Create a performance monitoring system that tracks FPS, primitive count, draw calls, memory usage, and provides optimization suggestions. Include real-time display (F3 key) and automated performance testing for different scenarios."
```

**Expected result:** Performance analysis tools

---

## **🎮 Phase 8: Final Integration (Days 26-28)**

### **Step 26: Game Manager Integration**
**Prompt to give me:**
```
"Create a comprehensive GameManager that ties all systems together: scene management, game state persistence, system initialization order, and coordination between combat, exploration, and inventory modes. Include proper cleanup and state transitions."
```

**Expected result:** Unified game management

---

### **Step 27: Input System Polish**
**Prompt to give me:**
```
"Implement comprehensive input handling for both keyboard/mouse and controller. Include inventory navigation with controller, hotkeys for common actions, input remapping support, and seamless switching between input methods."
```

**Expected result:** Polished controls

---

### **Step 28: Final POC Integration**
**Prompt to give me:**
```
"Create the final POC integration that demonstrates all systems working together: a complete gameplay loop from exploration to combat to inventory management to progression. Include a demo scene that showcases all major features and validates the POC success criteria."
```

**Expected result:** Complete playable POC

---

## **📋 Testing Milestones**

### **After Phase 2 - Test Inventory:**
- Can place L-shaped items in grid
- Items rotate correctly
- Gems modify adjacent weapons
- Drag and drop works smoothly

### **After Phase 4 - Test Combat:**
- Turn order based on speed
- Status effects apply and interact
- Critical hits with equipment bonuses
- Camera moves dynamically

### **After Phase 6 - Test Economy:**
- Shop purchases work
- Item upgrades combine correctly
- Crafting follows blueprints
- Progression feels meaningful

### **After Phase 8 - Final Validation:**
- Complete gameplay loop functional
- All systems integrate smoothly
- Performance meets targets
- POC demonstrates innovation

---

## **🎯 Success Criteria Checklist**

**Your POC is successful when you can demonstrate:**

**✅ Complex Inventory System:**
- [ ] Place L-shaped crowbar in 3×4 grid space
- [ ] Rotate items to fit better
- [ ] Place fire gem next to sword, see damage increase
- [ ] Different character grid shapes feel unique

**✅ Status Effect Mastery:**
- [ ] Apply oil to enemy, hit with fire → double damage + burn
- [ ] Freeze burning enemy → burn removed
- [ ] Status effects decay by 1 each turn
- [ ] Armor absorbs damage, spikes reflect it

**✅ Dynamic Combat:**
- [ ] Speed buffs change turn order mid-combat
- [ ] Critical hits with equipment scaling
- [ ] Camera smoothly moves between characters
- [ ] Character poses change with menu selection

**✅ Equipment Progression:**
- [ ] Two identical swords upgrade to better sword
- [ ] Equipment visible on primitive characters
- [ ] Character builds feel meaningfully different
- [ ] All progression through equipment only

**✅ Technical Excellence:**
- [ ] Save/load preserves all game state perfectly
- [ ] 60fps with 1000+ primitive objects
- [ ] Controller and keyboard both work well
- [ ] Debug tools help rapid iteration

---

## **🚀 Ready to Start?**

**Begin with Step 1!** Copy the prompt exactly and I'll provide you with the complete, working code for each step. Each response will include:

- Complete script files with all necessary code
- Clear instructions on where to place/create things
- What to test to verify it's working
- Dependencies and setup requirements

**Your POC will be genuinely impressive - complex tactical gameplay with innovative inventory mechanics, all running smoothly with simple primitive graphics!**

Let's build something amazing! 🎮✨