using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JRPGGame.Data
{
    [System.Serializable]
    public class ItemShape
    {
        [Header("Shape Definition")]
        public Vector2Int[] occupiedCells = new Vector2Int[] { Vector2Int.zero };
        public Vector2Int pivotPoint = Vector2Int.zero;
        
        [Header("Rotation Support")]
        [Range(1, 4)] public int rotationStates = 1; // 1, 2, or 4 possible rotations

        public ItemShape()
        {
            occupiedCells = new Vector2Int[] { Vector2Int.zero };
        }

        public ItemShape(Vector2Int[] cells)
        {
            occupiedCells = cells ?? new Vector2Int[] { Vector2Int.zero };
        }

        public Vector2Int[] GetRotatedShape(int rotation)
        {
            if (rotation == 0 || rotationStates == 1)
                return occupiedCells;

            Vector2Int[] rotatedCells = new Vector2Int[occupiedCells.Length];
            
            for (int i = 0; i < occupiedCells.Length; i++)
            {
                Vector2Int cell = occupiedCells[i];
                
                // Rotate around pivot point
                Vector2Int relativeCell = cell - pivotPoint;
                Vector2Int rotatedRelative = RotateVector2Int(relativeCell, rotation);
                rotatedCells[i] = rotatedRelative + pivotPoint;
            }
            
            return rotatedCells;
        }

        private Vector2Int RotateVector2Int(Vector2Int vector, int rotation)
        {
            return (rotation % 4) switch
            {
                1 => new Vector2Int(-vector.y, vector.x),  // 90 degrees
                2 => new Vector2Int(-vector.x, -vector.y), // 180 degrees
                3 => new Vector2Int(vector.y, -vector.x),  // 270 degrees
                _ => vector // 0 degrees (no rotation)
            };
        }

        // Helper method to create common shapes
        public static ItemShape CreateRectangle(int width, int height)
        {
            List<Vector2Int> cells = new List<Vector2Int>();
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    cells.Add(new Vector2Int(x, y));
                }
            }
            
            return new ItemShape
            {
                occupiedCells = cells.ToArray(),
                pivotPoint = Vector2Int.zero,
                rotationStates = (width == height) ? 1 : 2 // Square shapes don't need rotation
            };
        }

        // Helper method to create L-shaped items (like the crowbar example)
        public static ItemShape CreateLShape()
        {
            return new ItemShape
            {
                occupiedCells = new Vector2Int[]
                {
                    new Vector2Int(0, 0),
                    new Vector2Int(0, 1),
                    new Vector2Int(0, 2),
                    new Vector2Int(1, 0)
                },
                pivotPoint = Vector2Int.zero,
                rotationStates = 4
            };
        }

        // Helper method to create T-shaped items
        public static ItemShape CreateTShape()
        {
            return new ItemShape
            {
                occupiedCells = new Vector2Int[]
                {
                    new Vector2Int(0, 1),
                    new Vector2Int(1, 0),
                    new Vector2Int(1, 1),
                    new Vector2Int(1, 2)
                },
                pivotPoint = Vector2Int.one,
                rotationStates = 4
            };
        }
    }
}