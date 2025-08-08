using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace JRPGGame.Data
{
    [System.Serializable]
    public class GridInventory
    {
        [Header("Grid Configuration")]
        public Vector2Int gridSize = new Vector2Int(6, 8);
        public List<PlacedItem> placedItems = new List<PlacedItem>();
        
        public GridInventory()
        {
            placedItems = new List<PlacedItem>();
        }

        public GridInventory(Vector2Int size)
        {
            gridSize = size;
            placedItems = new List<PlacedItem>();
        }

        public bool CanPlaceItem(ItemData item, Vector2Int position, int rotation)
        {
            if (item == null || item.shape == null) return false;
            
            Vector2Int[] rotatedShape = item.shape.GetRotatedShape(rotation);
            
            foreach (Vector2Int cell in rotatedShape)
            {
                Vector2Int finalPosition = position + cell;
                
                if (finalPosition.x < 0 || finalPosition.x >= gridSize.x ||
                    finalPosition.y < 0 || finalPosition.y >= gridSize.y)
                {
                    return false;
                }
                
                if (IsCellOccupied(finalPosition))
                {
                    return false;
                }
            }
            
            return true;
        }

        public bool PlaceItem(ItemData item, Vector2Int position, int rotation)
        {
            if (!CanPlaceItem(item, position, rotation))
                return false;
            
            PlacedItem placedItem = new PlacedItem(item, position, rotation);
            placedItems.Add(placedItem);
            
            if (item.type == ItemType.ActiveTool)
            {
                UpdateModifierEffects(placedItem);
            }
            
            if (item.type == ItemType.Modifier)
            {
                UpdateAllModifierEffects();
            }
            
            return true;
        }

        public void RemoveItem(PlacedItem item)
        {
            placedItems.Remove(item);
            UpdateAllModifierEffects();
        }

        public bool IsCellOccupied(Vector2Int position)
        {
            foreach (PlacedItem item in placedItems)
            {
                if (item.occupiedCells.Contains(position))
                {
                    return true;
                }
            }
            return false;
        }

        private void UpdateAllModifierEffects()
        {
            foreach (PlacedItem item in placedItems)
            {
                if (item.item.type == ItemType.ActiveTool)
                {
                    UpdateModifierEffects(item);
                }
            }
        }

        private void UpdateModifierEffects(PlacedItem activeItem)
        {
            activeItem.appliedModifiers.Clear();
            
            foreach (PlacedItem modifier in placedItems)
            {
                if (modifier.item.type == ItemType.Modifier)
                {
                    if (ModifierAffectsItem(modifier, activeItem))
                    {
                        ModifierApplication application = new ModifierApplication
                        {
                            modifierItem = modifier.item,
                            modifierPosition = modifier.position
                        };
                        activeItem.appliedModifiers.Add(application);
                    }
                }
            }
        }

        private bool ModifierAffectsItem(PlacedItem modifier, PlacedItem target)
        {
            if (modifier.item.modifierZones == null)
                return false;
            
            foreach (Vector2Int targetCell in target.occupiedCells)
            {
                foreach (Vector2Int modifierCell in modifier.item.modifierZones.affectedCells)
                {
                    Vector2Int modifierWorldPos = modifier.position + modifierCell;
                    if (modifierWorldPos == targetCell)
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }
    }
}