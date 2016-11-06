using System;
using System.Runtime.Serialization;

namespace Repositories
{
    public class DuplicateTodoItemException : Exception
    {
        public DuplicateTodoItemException() : base("Item with the same id already exist in list!")
        {
        }
    }
}