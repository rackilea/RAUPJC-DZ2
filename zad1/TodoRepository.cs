using interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Repositories
{
    /// <summary >
    /// Class that encapsulates all the logic for accessing TodoTtems .
    /// </ summary >
    public class TodoRepository : ITodoRepository
    {
        /// <summary >
        /// Repository does not fetch todoItems from the actual database ,
        /// it uses in memory storage for this excersise .
        /// </ summary >
        private readonly List<TodoItem> _inMemoryTodoDatabase;
        public TodoRepository(List<TodoItem> initialDbState = null)
        {
            _inMemoryTodoDatabase = initialDbState ?? new List<TodoItem>();

            //if (initialDbState != null)
            //{
            //    _inMemoryTodoDatabase = initialDbState;
            //}
            //else
            //{
            //    _inMemoryTodoDatabase = new List<TodoItem>();
            //}

            // Shorter way to write this in C# using ?? operator :
            // _inMemoryTodoDatabase = initialDbState ?? new List < TodoItem >() ;
            // x ?? y -> if x is not null , expression returns x. Else y. 
        }

        public void Add(TodoItem todoItem)
        {
            if (todoItem == null)
            {
                throw new ArgumentNullException(nameof(todoItem), "Null cannot be added to the list.");
            }
            TodoItem id = Get(todoItem.Id);
            if (id!=null) throw new DuplicateTodoItemException();
            
            else
            {
                _inMemoryTodoDatabase.Add(todoItem);
            }
        }

        public TodoItem Get(Guid todoId)
        {
            return _inMemoryTodoDatabase.FirstOrDefault(i => i.Id.Equals(todoId));
        }

        public List<TodoItem> GetActive()
        {
            List<TodoItem> active=_inMemoryTodoDatabase.Where(i => i.DateCompleted==null).ToList();
            return active;
        }

        public List<TodoItem> GetAll()
        {
            List<TodoItem> descending=_inMemoryTodoDatabase.OrderByDescending(i=>i.DateCompleted).ToList();
            return descending;
        }

        public List<TodoItem> GetCompleted()
        {
            List<TodoItem> completed = _inMemoryTodoDatabase.Where(i => i.IsCompleted == true).ToList();
            return completed;
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction)
        {
            List<TodoItem> fitsFilterFunction=_inMemoryTodoDatabase.Where(i=>filterFunction(i)).ToList();
            return fitsFilterFunction;
        }

        public bool MarkAsCompleted(Guid todoId)
        {
            if (todoId == null)
            {
                return false;
            }
            TodoItem helpItem = Get(todoId);
            if (helpItem == null)
            {
                return false;
            }
            else
            {
               helpItem.MarkAsCompleted();
                return helpItem.IsCompleted;
            }
        }

        public bool Remove(Guid todoId)
        {
            if (todoId == null)
            {
                return false;
            }
            return _inMemoryTodoDatabase.Remove(Get(todoId));
        }

        public void Update(TodoItem todoItem)
        {
            var index = _inMemoryTodoDatabase.IndexOf(todoItem);
            if (index == -1)
            {
                _inMemoryTodoDatabase.Add(todoItem);
            }
            else
            {
                TodoItem helpItem = _inMemoryTodoDatabase.First(i => i.Id.Equals(todoItem.Id));
                helpItem.Text = todoItem.Text;
                helpItem.IsCompleted = todoItem.IsCompleted;
                helpItem.DateCompleted = todoItem.DateCompleted;
                helpItem.DateCreated = todoItem.DateCreated;
                
            }
        }
    }

}