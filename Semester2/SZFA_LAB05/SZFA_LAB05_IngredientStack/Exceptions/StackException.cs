using SZFA_LAB05_IngredientStack.Models;

namespace SZFA_LAB05_IngredientStack.Exceptions
{
    public class StackException : Exception
    {
        public IngredientStack Stack { get; private set; }

        public StackException(IngredientStack stack, string message) : base(message)
        {
            Stack = stack;
        }
    }
}
