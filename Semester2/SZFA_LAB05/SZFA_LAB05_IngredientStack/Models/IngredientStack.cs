using SZFA_LAB05_IngredientStack.Exceptions;

namespace SZFA_LAB05_IngredientStack.Models
{
    public class IngredientStack
    {
        FoodIngredient[] ingredients;
        int lastItemIdx;

        public IngredientStack(int stackSize)
        {
            ingredients = new FoodIngredient[stackSize];
            lastItemIdx = -1;
        }

        public bool Empty()
        {
            int i = 0;
            while (i < ingredients.Length && ingredients[i] is null)
            {
                i++;
            }

            return i == ingredients.Length;
        }

        public void Push(FoodIngredient ingredient)
        {
            if (lastItemIdx == ingredients.Length - 1)
            {
                throw new StackFullException(this, "Betelt a verem.");
            }

            ingredients[lastItemIdx++] = ingredient;
        }

        public FoodIngredient Pop()
        {
            if (lastItemIdx < 0)
            {
                throw new StackEmptyException(this, "Üres a verem.");
            }

            FoodIngredient ingredient = ingredients[lastItemIdx];
            ingredients[lastItemIdx--] = null;

            return ingredient;
        }
    }
}
