using System.Collections.Generic;
using UnityEngine;

namespace JRPGGame.Data
{
    [System.Serializable]
    public class PlacedItem
    {
        public ItemData item;
        public Vector2Int position;
        public int rotation;
        public List<Vector2Int> occupiedCells = new List<Vector2Int>();
        public List<ModifierApplication> appliedModifiers = new List<ModifierApplication>();

        public PlacedItem()
        {
            occupiedCells = new List<Vector2Int>();
            appliedModifiers = new List<ModifierApplication>();
        }

        public PlacedItem(ItemData itemData, Vector2Int pos, int rot)
        {
            item = itemData;
            position = pos;
            rotation = rot;
            occupiedCells = new List<Vector2Int>();
            appliedModifiers = new List<ModifierApplication>();
            
            if (item != null && item.shape != null)
            {
                Vector2Int[] shape = item.shape.GetRotatedShape(rotation);
                foreach (Vector2Int cell in shape)
                {
                    occupiedCells.Add(position + cell);
                }
            }
        }
    }

    [System.Serializable]
    public class ModifierApplication
    {
        public ItemData modifierItem;
        public Vector2Int modifierPosition;
        
        public ModifierApplication()
        {
        }
    }
}