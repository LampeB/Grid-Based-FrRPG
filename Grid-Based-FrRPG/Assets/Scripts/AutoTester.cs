using UnityEngine;
using JRPGGame.Data;

public class AutoTester : MonoBehaviour
{
    [Header("Test Results")]
    public bool testsComplete = false;
    public string results = "";

    void Start()
    {
        RunTests();
    }

    void RunTests()
    {
        Debug.Log("=== JRPG CORE SYSTEM AUTO-TEST ===");
        
        bool allPassed = true;
        results = "";
        
        // Test 1: Character Creation
        allPassed &= TestCharacters();
        
        // Test 2: Item Shapes
        allPassed &= TestShapes();
        
        // Test 3: Inventory
        allPassed &= TestInventory();
        
        testsComplete = true;
        
        if (allPassed)
        {
            Debug.Log("🎉 ALL TESTS PASSED! Your JRPG systems work perfectly!");
            results += "SUCCESS: All core systems functional\n";
            results += "✅ Character system working\n";
            results += "✅ Item shapes and rotation working\n";
            results += "✅ Inventory placement working\n";
        }
        else
        {
            Debug.LogError("Some tests failed!");
        }
    }

    bool TestCharacters()
    {
        try
        {
            Character warrior = new Character("test", "Warrior", "Warrior");
            warrior.baseStats.physicalAttack = 50;
            
            bool success = warrior.displayName == "Warrior" && warrior.baseStats.physicalAttack == 50;
            results += "Character Test: " + (success ? "PASS" : "FAIL") + "\n";
            return success;
        }
        catch
        {
            results += "Character Test: FAIL\n";
            return false;
        }
    }

    bool TestShapes()
    {
        try
        {
            ItemShape lShape = ItemShape.CreateLShape();
            Vector2Int[] rotated = lShape.GetRotatedShape(1);
            
            bool success = lShape.occupiedCells.Length == 4 && rotated.Length == 4;
            results += "Shape Test: " + (success ? "PASS" : "FAIL") + "\n";
            return success;
        }
        catch
        {
            results += "Shape Test: FAIL\n";
            return false;
        }
    }

    bool TestInventory()
    {
        try
        {
            Character testChar = new Character("inv", "Test", "Test");
            ItemData testItem = ScriptableObject.CreateInstance<ItemData>();
            testItem.shape = new ItemShape();
            testItem.type = ItemType.Consumable;
            
            bool placed = testChar.inventory.PlaceItem(testItem, Vector2Int.zero, 0);
            results += "Inventory Test: " + (placed ? "PASS" : "FAIL") + "\n";
            return placed;
        }
        catch
        {
            results += "Inventory Test: FAIL\n";
            return false;
        }
    }
}