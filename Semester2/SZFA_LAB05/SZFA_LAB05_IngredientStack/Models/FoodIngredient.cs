using SZFA_LAB05_IngredientStack.Enums;

namespace SZFA_LAB05_IngredientStack.Models
{
    public class FoodIngredient
    {
        string name;
        int amount;
        FoodIngredientType unit;

        public FoodIngredient(string name, int amount, FoodIngredientType unit)
        {
            this.name = name;
            this.amount = amount;
            this.unit = unit;
        }

        public override string ToString()
        {
            return $"Name: {name}, Amount: {amount} {unit}";
        }
    }
}
