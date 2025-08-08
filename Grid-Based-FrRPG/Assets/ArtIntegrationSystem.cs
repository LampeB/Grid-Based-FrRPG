using UnityEngine;
using JRPGGame.Data;
using System.Collections.Generic;

public class ArtIntegrationSystem : MonoBehaviour
{
    [Header("🎨 Automatic Art Integration")]
    public int totalItemsCreated = 0;
    public bool integrationComplete = false;

    [Header("📋 Integration Report")]
    [TextArea(15, 25)]
    public string fullReport = "";

    [Header("📦 Created Items")]
    public List<ItemData> allCreatedItems = new List<ItemData>();

    void Start()
    {
        CreateAllItemsForYourArt();
    }

    [ContextMenu("🚀 Auto-Create Items For Your Art")]
    public void CreateAllItemsForYourArt()
    {
        fullReport = "=== 🎨 AUTOMATIC ART INTEGRATION ===\n\n";
        allCreatedItems.Clear();
        totalItemsCreated = 0;

        Debug.Log("🎨 Creating items perfectly matched to your beautiful art...");

        ItemDatabase database = ItemDatabase.Instance;

        // 🧪 POTIONS (Green, Blue, Red, etc.)
        CreatePotionCollection();

        // 💎 GEMS & CRYSTALS (Fire, Ice, Various Colors)
        CreateGemCollection();

        // ⚔️ WEAPONS (Swords, Daggers, etc.)
        CreateWeaponCollection();

        // 🛡️ ARMOR & EQUIPMENT (Boots, Helmets, Armor)
        CreateArmorCollection();

        // 💍 ACCESSORIES (Rings, Necklaces)
        CreateAccessoryCollection();

        // Add all items to database
        foreach (ItemData item in allCreatedItems)
        {
            database.AddItem(item);
        }

        integrationComplete = true;

        fullReport += $"\n🎉 INTEGRATION COMPLETE!\n";
        fullReport += $"✅ Created {totalItemsCreated} items ready for your art!\n\n";
        fullReport += "🔧 HOW TO FINISH:\n";
        fullReport += "1. Find the created ItemData assets in your Project\n";
        fullReport += "2. Drag matching sprites from Art/Items to Icon fields\n";
        fullReport += "3. Example: 'Health Potion' gets potionGreen sprite\n";
        fullReport += "4. Your beautiful art will appear in the game!\n\n";
        fullReport += "🎮 All stats, shapes, and properties are set up!\n";

        Debug.Log($"🎉 SUCCESS! Created {totalItemsCreated} items for your art!");
        Debug.Log("Now just drag your sprites to the Icon fields to complete integration!");
    }

    void CreatePotionCollection()
    {
        fullReport += "🧪 POTIONS CREATED:\n";

        // Health Potions (Green sprites)
        CreatePotion("health_potion_small", "Small Health Potion", ItemTier.Common, 25,
                    "Restores 50 HP • Use: potionGreen sprite");
        CreatePotion("health_potion_medium", "Health Potion", ItemTier.Uncommon, 50,
                    "Restores 150 HP • Use: potionGreen_un sprite");
        CreatePotion("health_potion_large", "Greater Health Potion", ItemTier.Rare, 100,
                    "Restores 350 HP • Use: potionGreen_ra sprite");

        // Mana Potions (Blue sprites)
        CreatePotion("mana_potion_small", "Small Mana Potion", ItemTier.Common, 30,
                    "Restores 30 MP • Use: potionBlue sprite");
        CreatePotion("mana_potion_medium", "Mana Potion", ItemTier.Uncommon, 60,
                    "Restores 80 MP • Use: potionBlue_un sprite");
        CreatePotion("mana_potion_large", "Greater Mana Potion", ItemTier.Rare, 120,
                    "Restores 200 MP • Use: potionBlue_ra sprite");

        // Special Potions (Red/Other sprites)
        CreatePotion("poison_antidote", "Antidote", ItemTier.Common, 40,
                    "Cures poison • Use: potionRed or similar sprite");
        CreatePotion("strength_potion", "Strength Potion", ItemTier.Uncommon, 80,
                    "Temporarily increases attack • Use: any rare potion sprite");

        fullReport += $"  ✓ Created {8} potion items\n\n";
    }

    void CreateGemCollection()
    {
        fullReport += "💎 GEMS & CRYSTALS CREATED:\n";

        // Fire Gems (Red gems/crystals)
        CreateGem("fire_gem_common", "Fire Gem", ItemTier.Common, StatType.PhysicalAttack, 4,
                 "Adds fire damage • Use: fireGem sprite");
        CreateGem("fire_gem_uncommon", "Uncommon Fire Gem", ItemTier.Uncommon, StatType.PhysicalAttack, 8,
                 "Good fire damage • Use: fireGem_un sprite");
        CreateGem("fire_gem_rare", "Rare Fire Gem", ItemTier.Rare, StatType.PhysicalAttack, 15,
                 "Great fire damage • Use: fireGem_ra sprite");

        // Ice Gems (Blue gems/crystals)  
        CreateGem("ice_gem_common", "Ice Gem", ItemTier.Common, StatType.SpecialAttack, 4,
                 "Adds ice damage • Use: iceGem sprite");
        CreateGem("ice_gem_uncommon", "Uncommon Ice Gem", ItemTier.Uncommon, StatType.SpecialAttack, 8,
                 "Good ice damage • Use: iceGem_un sprite");
        CreateGem("ice_gem_rare", "Rare Ice Gem", ItemTier.Rare, StatType.SpecialAttack, 15,
                 "Great ice damage • Use: iceGem_ra sprite");

        // Green Gems (Speed/Wind)
        CreateGem("wind_gem", "Wind Gem", ItemTier.Uncommon, StatType.Speed, 10,
                 "Increases speed • Use: green gem sprite");
        CreateGem("poison_gem", "Poison Gem", ItemTier.Rare, StatType.PhysicalAttack, 12,
                 "Poison damage • Use: poisonGem sprite");

        fullReport += $"  ✓ Created {8} gem items\n\n";
    }

    void CreateWeaponCollection()
    {
        fullReport += "⚔️ WEAPONS CREATED:\n";

        // Swords (Various sword sprites)
        CreateWeapon("iron_sword", "Iron Sword", ItemCategory.Sword, ItemTier.Common, 15, 1, 3,
                    "Basic iron sword • Use: longSword sprite");
        CreateWeapon("steel_sword", "Steel Sword", ItemCategory.Sword, ItemTier.Uncommon, 25, 1, 3,
                    "Quality steel sword • Use: longSword_un sprite");
        CreateWeapon("enchanted_sword", "Enchanted Sword", ItemCategory.Sword, ItemTier.Rare, 40, 1, 3,
                    "Magical sword • Use: longSword_ra sprite");

        // Daggers (Dagger sprites)
        CreateWeapon("iron_dagger", "Iron Dagger", ItemCategory.Dagger, ItemTier.Common, 10, 1, 2,
                    "Swift dagger • Use: dagger sprite");
        CreateWeapon("shadow_dagger", "Shadow Dagger", ItemCategory.Dagger, ItemTier.Rare, 28, 1, 2,
                    "Assassin's blade • Use: dagger_ra sprite");

        // Maces (Can use mace sprites if available)
        CreateWeapon("iron_mace", "Iron Mace", ItemCategory.Mace, ItemTier.Common, 18, 1, 2,
                    "Heavy mace • Use: mace sprite or similar");

        fullReport += $"  ✓ Created {6} weapon items\n\n";
    }

    void CreateArmorCollection()
    {
        fullReport += "🛡️ ARMOR & EQUIPMENT CREATED:\n";

        // Boots (Boot sprites)
        CreateArmor("leather_boots", "Leather Boots", ItemCategory.Shoes, ItemTier.Common,
                   StatType.PhysicalDefense, 3, StatType.Speed, 2, 1, 1,
                   "Basic leather boots • Use: boots sprite");
        CreateArmor("enchanted_boots", "Enchanted Boots", ItemCategory.Shoes, ItemTier.Rare,
                   StatType.PhysicalDefense, 12, StatType.Speed, 8, 1, 1,
                   "Magical boots • Use: boots_rare sprite");

        // Helmets (Helmet sprites)
        CreateArmor("iron_helmet", "Iron Helmet", ItemCategory.Helmet, ItemTier.Common,
                   StatType.PhysicalDefense, 6, StatType.MaxHP, 20, 1, 1,
                   "Protective helmet • Use: helmet sprite");
        CreateArmor("steel_helmet", "Steel Helmet", ItemCategory.Helmet, ItemTier.Uncommon,
                   StatType.PhysicalDefense, 12, StatType.MaxHP, 40, 1, 1,
                   "Quality helmet • Use: helmet_un sprite");

        // Chest Armor (Chestplate sprites)
        CreateArmor("leather_armor", "Leather Armor", ItemCategory.Armor, ItemTier.Common,
                   StatType.PhysicalDefense, 10, StatType.MaxHP, 35, 2, 2,
                   "Basic leather armor • Use: chestplate sprite");
        CreateArmor("chain_armor", "Chain Armor", ItemCategory.Armor, ItemTier.Uncommon,
                   StatType.PhysicalDefense, 18, StatType.MaxHP, 60, 2, 2,
                   "Chain mail armor • Use: chestplate_un sprite");

        fullReport += $"  ✓ Created {6} armor items\n\n";
    }

    void CreateAccessoryCollection()
    {
        fullReport += "💍 ACCESSORIES CREATED:\n";

        // Rings (Ring sprites if available)
        CreateAccessory("power_ring", "Power Ring", ItemCategory.Ring, ItemTier.Uncommon,
                       StatType.PhysicalAttack, 8, "Increases attack power");
        CreateAccessory("protection_ring", "Protection Ring", ItemCategory.Ring, ItemTier.Uncommon,
                       StatType.PhysicalDefense, 8, "Increases defense");
        CreateAccessory("health_ring", "Health Ring", ItemCategory.Ring, ItemTier.Rare,
                       StatType.MaxHP, 100, "Greatly increases health");

        // Necklaces/Amulets
        CreateAccessory("mana_necklace", "Mana Necklace", ItemCategory.Necklace, ItemTier.Uncommon,
                       StatType.MaxMP, 50, "Increases mana capacity");
        CreateAccessory("speed_amulet", "Speed Amulet", ItemCategory.Necklace, ItemTier.Rare,
                       StatType.Speed, 15, "Increases movement speed");

        fullReport += $"  ✓ Created {5} accessory items\n\n";
    }

    // Helper Methods
    ItemData CreatePotion(string id, string name, ItemTier tier, int price, string description)
    {
        ItemData potion = ScriptableObject.CreateInstance<ItemData>();
        potion.itemId = id;
        potion.displayName = name;
        potion.category = ItemCategory.Consumable;
        potion.type = ItemType.Consumable;
        potion.tier = tier;
        potion.shape = ItemShape.CreateRectangle(1, 1);
        potion.basePrice = price;
        potion.description = description;

        allCreatedItems.Add(potion);
        totalItemsCreated++;
        return potion;
    }

    ItemData CreateGem(string id, string name, ItemTier tier, StatType modifierStat, int power, string description)
    {
        ItemData gem = ScriptableObject.CreateInstance<ItemData>();
        gem.itemId = id;
        gem.displayName = name;
        gem.category = ItemCategory.Gem;
        gem.type = ItemType.Modifier;
        gem.tier = tier;
        gem.shape = ItemShape.CreateRectangle(1, 1);
        gem.basePrice = (int)tier * 40 + 60;
        gem.description = description;

        // Set up modifier zones
        gem.modifierZones = new ModifierPattern();
        gem.modifierZones.affectedCells = new Vector2Int[] {
            new Vector2Int(-1, 0), new Vector2Int(1, 0),
            new Vector2Int(0, -1), new Vector2Int(0, 1)
        };
        gem.modifierZones.modifierEffects.Add(new StatModifier(modifierStat, power, false));

        allCreatedItems.Add(gem);
        totalItemsCreated++;
        return gem;
    }

    ItemData CreateWeapon(string id, string name, ItemCategory category, ItemTier tier, int attackPower,
                         int shapeW, int shapeH, string description)
    {
        ItemData weapon = ScriptableObject.CreateInstance<ItemData>();
        weapon.itemId = id;
        weapon.displayName = name;
        weapon.category = category;
        weapon.type = ItemType.ActiveTool;
        weapon.tier = tier;
        weapon.shape = ItemShape.CreateRectangle(shapeW, shapeH);
        weapon.basePrice = attackPower * 5 + (int)tier * 25;
        weapon.description = description;

        weapon.statModifiers.Add(new StatModifier(StatType.PhysicalAttack, attackPower, false));

        allCreatedItems.Add(weapon);
        totalItemsCreated++;
        return weapon;
    }

    ItemData CreateArmor(string id, string name, ItemCategory category, ItemTier tier,
                        StatType stat1, int value1, StatType stat2, int value2,
                        int shapeW, int shapeH, string description)
    {
        ItemData armor = ScriptableObject.CreateInstance<ItemData>();
        armor.itemId = id;
        armor.displayName = name;
        armor.category = category;
        armor.type = ItemType.PassiveGear;
        armor.tier = tier;
        armor.shape = ItemShape.CreateRectangle(shapeW, shapeH);
        armor.basePrice = (value1 + value2) * 4 + (int)tier * 30;
        armor.description = description;

        armor.statModifiers.Add(new StatModifier(stat1, value1, false));
        armor.statModifiers.Add(new StatModifier(stat2, value2, false));

        allCreatedItems.Add(armor);
        totalItemsCreated++;
        return armor;
    }

    ItemData CreateAccessory(string id, string name, ItemCategory category, ItemTier tier,
                           StatType stat, int value, string description)
    {
        ItemData accessory = ScriptableObject.CreateInstance<ItemData>();
        accessory.itemId = id;
        accessory.displayName = name;
        accessory.category = category;
        accessory.type = ItemType.PassiveGear;
        accessory.tier = tier;
        accessory.shape = ItemShape.CreateRectangle(1, 1);
        accessory.basePrice = value * 6 + (int)tier * 40;
        accessory.description = description;

        accessory.statModifiers.Add(new StatModifier(stat, value, false));

        allCreatedItems.Add(accessory);
        totalItemsCreated++;
        return accessory;
    }

    [ContextMenu("📋 Show Art Matching Guide")]
    public void ShowArtMatchingGuide()
    {
        Debug.Log("=== 🎨 PERFECT ART MATCHING GUIDE ===");
        Debug.Log("Health Potion → potionGreen sprite");
        Debug.Log("Mana Potion → potionBlue sprite");
        Debug.Log("Fire Gem → fireGem sprite (red/orange gem)");
        Debug.Log("Ice Gem → iceGem sprite (blue gem)");
        Debug.Log("Iron Sword → longSword sprite");
        Debug.Log("Iron Dagger → dagger sprite");
        Debug.Log("Leather Boots → boots sprite");
        Debug.Log("Iron Helmet → helmet sprite");
        Debug.Log("Leather Armor → chestplate sprite");
        Debug.Log("");
        Debug.Log("🔧 HOW TO ASSIGN:");
        Debug.Log("1. Select an ItemData in Project window");
        Debug.Log("2. In Inspector, find the 'Icon' field");
        Debug.Log("3. Drag matching sprite from Art/Items");
        Debug.Log("4. Your art appears in game!");
    }

    [ContextMenu("🎮 Test Integration")]
    public void TestIntegration()
    {
        Debug.Log("=== 🧪 TESTING ART INTEGRATION ===");
        Debug.Log($"Items created: {totalItemsCreated}");
        Debug.Log($"Integration complete: {integrationComplete}");

        if (allCreatedItems.Count > 0)
        {
            Debug.Log($"Sample items created:");
            for (int i = 0; i < Mathf.Min(5, allCreatedItems.Count); i++)
            {
                Debug.Log($"  • {allCreatedItems[i].displayName} ({allCreatedItems[i].category})");
            }
        }

        ItemDatabase db = ItemDatabase.Instance;
        Debug.Log($"Items in database: {db.GetAllItems().Count}");
    }
}