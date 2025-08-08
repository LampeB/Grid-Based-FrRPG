using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace JRPGGame.Data
{
    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "Game/Item Database")]
    public class ItemDatabase : ScriptableObject
    {
        [Header("Item Storage")]
        [SerializeField] private List<ItemData> allItems = new List<ItemData>();
        
        [Header("Database Info")]
        [SerializeField] private int totalItems = 0;
        [SerializeField] private bool isDatabaseValid = true;
        
        private Dictionary<string, ItemData> itemLookup;
        private Dictionary<ItemCategory, List<ItemData>> categoryLookup;
        
        private static ItemDatabase instance;
        public static ItemDatabase Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<ItemDatabase>("ItemDatabase");
                    if (instance == null)
                    {
                        instance = CreateInstance<ItemDatabase>();
                    }
                }
                return instance;
            }
        }

        private void OnEnable()
        {
            BuildLookupTables();
            ValidateDatabase();
        }

        private void OnValidate()
        {
            BuildLookupTables();
            ValidateDatabase();
            totalItems = allItems.Count;
        }

        public void AddItem(ItemData item)
        {
            if (item == null || allItems.Contains(item)) return;
            
            allItems.Add(item);
            BuildLookupTables();
            ValidateDatabase();
        }

        public ItemData GetItemById(string itemId)
        {
            if (itemLookup == null) BuildLookupTables();
            itemLookup.TryGetValue(itemId, out ItemData item);
            return item;
        }

        public List<ItemData> GetItemsByCategory(ItemCategory category)
        {
            if (categoryLookup == null) BuildLookupTables();
            categoryLookup.TryGetValue(category, out List<ItemData> items);
            return items ?? new List<ItemData>();
        }

        public List<ItemData> GetAllItems()
        {
            return new List<ItemData>(allItems);
        }

        private void BuildLookupTables()
        {
            itemLookup = new Dictionary<string, ItemData>();
            categoryLookup = new Dictionary<ItemCategory, List<ItemData>>();
            
            foreach (ItemCategory category in System.Enum.GetValues(typeof(ItemCategory)))
            {
                categoryLookup[category] = new List<ItemData>();
            }
            
            foreach (ItemData item in allItems)
            {
                if (item != null && !string.IsNullOrEmpty(item.itemId))
                {
                    itemLookup[item.itemId] = item;
                    categoryLookup[item.category].Add(item);
                }
            }
        }

        public bool ValidateDatabase()
        {
            bool isValid = true;
            allItems.RemoveAll(item => item == null);
            
            var duplicateIds = allItems
                .Where(item => !string.IsNullOrEmpty(item.itemId))
                .GroupBy(item => item.itemId)
                .Where(group => group.Count() > 1);
                
            if (duplicateIds.Any())
            {
                isValid = false;
                Debug.LogError("Database has duplicate item IDs!");
            }
            
            isDatabaseValid = isValid;
            return isValid;
        }

        [ContextMenu("Create Test Items")]
        public void CreateTestItems()
        {
            ItemData sword = CreateInstance<ItemData>();
            sword.itemId = "test_sword";
            sword.displayName = "Test Sword";
            sword.category = ItemCategory.Sword;
            sword.type = ItemType.ActiveTool;
            sword.tier = ItemTier.Common;
            sword.shape = ItemShape.CreateRectangle(1, 3);
            AddItem(sword);
            
            ItemData gem = CreateInstance<ItemData>();
            gem.itemId = "test_gem";
            gem.displayName = "Test Gem";
            gem.category = ItemCategory.Gem;
            gem.type = ItemType.Modifier;
            gem.tier = ItemTier.Uncommon;
            gem.shape = new ItemShape(new Vector2Int[] { Vector2Int.zero });
            gem.modifierZones = new ModifierPattern();
            gem.modifierZones.affectedCells = new Vector2Int[] { new Vector2Int(-1, 0) };
            AddItem(gem);
            
            Debug.Log("Created test items in database");
        }
    }
}