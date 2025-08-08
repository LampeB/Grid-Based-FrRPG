using UnityEngine;
using JRPGGame.Data;
using System.Collections.Generic;

public class DatabaseValidator : MonoBehaviour
{
    [Header("Database Reference")]
    public ItemDatabase database;
    
    [Header("Validation Results")]
    public bool isValid = false;
    public int totalItems = 0;
    public int errorCount = 0;
    
    [Header("Validation Report")]
    [TextArea(8, 15)]
    public string report = "";

    void Start()
    {
        ValidateDatabase();
    }

    [ContextMenu("Validate Database")]
    public void ValidateDatabase()
    {
        if (database == null)
        {
            database = ItemDatabase.Instance;
        }
        
        if (database == null)
        {
            report = "No database found!";
            return;
        }

        List<ItemData> allItems = database.GetAllItems();
        totalItems = allItems.Count;
        errorCount = 0;
        
        report = "DATABASE VALIDATION REPORT:\n\n";
        report += $"Total Items: {totalItems}\n\n";
        
        foreach (ItemData item in allItems)
        {
            if (item == null)
            {
                report += "ERROR: Found null item\n";
                errorCount++;
                continue;
            }
            
            if (string.IsNullOrEmpty(item.itemId))
            {
                report += $"ERROR: {item.displayName} has no ID\n";
                errorCount++;
            }
            
            if (string.IsNullOrEmpty(item.displayName))
            {
                report += $"ERROR: {item.itemId} has no name\n";
                errorCount++;
            }
            
            if (item.shape == null || item.shape.occupiedCells.Length == 0)
            {
                report += $"ERROR: {item.displayName} has invalid shape\n";
                errorCount++;
            }
        }
        
        if (errorCount == 0)
        {
            report += "✅ ALL ITEMS VALID!\n";
            isValid = true;
        }
        else
        {
            report += $"\n❌ Found {errorCount} errors\n";
            isValid = false;
        }
        
        Debug.Log("Database validation complete");
    }

    [ContextMenu("Test Database Functions")]
    public void TestDatabaseFunctions()
    {
        if (database == null) return;
        
        Debug.Log("=== DATABASE FUNCTION TEST ===");
        
        List<ItemData> allItems = database.GetAllItems();
        Debug.Log($"GetAllItems(): {allItems.Count} items");
        
        List<ItemData> swords = database.GetItemsByCategory(ItemCategory.Sword);
        Debug.Log($"GetItemsByCategory(Sword): {swords.Count} items");
        
        if (allItems.Count > 0)
        {
            ItemData testItem = database.GetItemById(allItems[0].itemId);
            Debug.Log($"GetItemById(): {(testItem != null ? "SUCCESS" : "FAILED")}");
        }
    }
}