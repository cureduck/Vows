using System;

namespace Model
{
    public enum IngredientUnit { Spoon, Cup, Bowl, Piece }

// Custom serializable class
    [Serializable]
    public class Ingredient
    {
        public string name;
        public int amount = 1;
        public IngredientUnit unit;
    }
}