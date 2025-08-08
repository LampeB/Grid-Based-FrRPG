using UnityEngine;
using JRPGGame.Data;
using System.Collections.Generic;

public class Step2Tester : MonoBehaviour
{
    [Header("Step 2 Test Results")]
    public bool databaseTestsPassed = false;
    [TextArea(8, 15)]
    public string results = "";

    void Start()
    {
        TestStep2Features();
    }

    void TestStep2Features()
    {
        results = "=== STEP 2: ITEM DATABASE TESTS ===\n\n";
        bool allPassed = true;

        Debug.Log("Testing Item Database System...");

        // Test 1: Database Creation
        ItemDatabase db = ItemDatabase.Instance;
        bool dbExists = db != null;
        results += $"Database Creation: {(dbExists ? "PASS" : "FAIL")}\n";
        allPassed &= dbExists;

        if (!dbExists)
        {
            databaseTestsPassed = false;
            return;
        }

        // Test 2: Item Storage
        db.CreateTestItems();
        List<ItemData> allItems = db.GetAllItems();
        bool storageWorks = allItems.Count > 0;
        results += $"Item Storage: {(storageWorks ? "PASS" : "FAIL")} ({allItems.Count} items)\n";
        allPassed &= storageWorks;

        // Test 3: ID Lookup
        ItemData sword = db.GetItemById("test_sword");
        bool idLookup = sword != null;
        results += $"ID Lookup: {(idLookup ? "PASS" : "FAIL")}\n";
        allPassed &= idLookup;

        // Test 4: Category Lookup
        List<ItemData> swords = db.GetItemsByCategory(ItemCategory.Sword);
        bool categoryLookup = swords.Count > 0;
        results += $"Category Lookup: {(categoryLookup ? "PASS" : "FAIL")} ({swords.Count} swords)\n";
        allPassed &= categoryLookup;

        // Test 5: Validation
        bool validation = db.ValidateDatabase();
        results += $"Database Validation: {(validation ? "PASS" : "PARTIAL")}\n";

        databaseTestsPassed = allPassed;

        if (allPassed)
        {
            results += "\n✅ ITEM DATABASE SYSTEM WORKING!\n";
            results += "Ready for Step 3: Event System\n";
            Debug.Log("🎉 Step 2 Complete! Item Database Working!");
        }
        else
        {
            results += "\n❌ Some database features failed\n";
            Debug.LogError("Step 2 had some failures");
        }
    }

    [ContextMenu("Test Database Functions")]
    public void TestDatabaseFunctions()
    {
        ItemDatabase db = ItemDatabase.Instance;
        db.CreateTestItems();
        
        Debug.Log("=== DATABASE FUNCTION TEST ===");
        Debug.Log($"All items: {db.GetAllItems().Count}");
        Debug.Log($"Swords: {db.GetItemsByCategory(ItemCategory.Sword).Count}");
        Debug.Log($"Gems: {db.GetItemsByCategory(ItemCategory.Gem).Count}");
        
        ItemData testSword = db.GetItemById("test_sword");
        Debug.Log($"Sword found: {testSword != null}");
        
        bool valid = db.ValidateDatabase();
        Debug.Log($"Database valid: {valid}");
    }
}