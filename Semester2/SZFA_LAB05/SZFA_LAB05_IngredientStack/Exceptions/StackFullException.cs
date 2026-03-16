using SZFA_LAB05_IngredientStack.Models;

namespace SZFA_LAB05_IngredientStack.Exceptions
{
    public class StackFullException : StackException
    {
        public StackFullException(IngredientStack stack, string message) : base(stack, message)
        {
        }
    }
}
