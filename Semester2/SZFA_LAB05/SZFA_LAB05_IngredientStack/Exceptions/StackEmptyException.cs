using SZFA_LAB05_IngredientStack.Models;

namespace SZFA_LAB05_IngredientStack.Exceptions
{
    public class StackEmptyException : StackException
    {
        public StackEmptyException(IngredientStack stack, string message) : base(stack, message)
        {
        }
    }
}
