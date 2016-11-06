using Microsoft.VisualStudio.TestTools.UnitTesting;
using zad1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using interfaces;
using Repositories;
using Models;

namespace zad1.Tests
{
    [TestClass()]
    public class TodoRepositoryTestsTests
    {
        private ITodoRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            repository = new TodoRepository();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddingNullToDatabaseThrowsException()
        {
            ITodoRepository repository = new TodoRepository();
            repository.Add(null);
        }

        [TestMethod]
        public void AddingItemWillAddToDatabase()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            repository.Add(todoItem);
            Assert.AreEqual(1, repository.GetAll().Count);
            Assert.IsTrue(repository.Get(todoItem.Id) != null);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateTodoItemException))]
        public void AddingExistingItemWillThrowException()
        {
            ITodoRepository repository = new TodoRepository();
            var todoItem = new TodoItem(" Groceries ");
            repository.Add(todoItem);
            repository.Add(todoItem);
        }

        [TestMethod]
        public void GettingItemFromEmptyDatabase()
        {
            var todoItem = new TodoItem("Something");
            Assert.AreNotEqual(todoItem,repository.Get(todoItem.Id));
            Assert.IsNull(repository.Get(todoItem.Id));
        }

        [TestMethod]
        public void GettingItemFromNonEmptyDatabase()
        {
            var todoItem = new TodoItem("Something");
            repository.Add(todoItem);
            Assert.AreEqual(todoItem, repository.Get(todoItem.Id));
        }

        

        [TestMethod]
        public void GettingAcitveOnesFromDataBase()
        {
            var todoItemA = new TodoItem("Something");
            todoItemA.MarkAsCompleted();

            var todoItemNotA = new TodoItem("Something else");

            repository.Add(todoItemA);
            repository.Add(todoItemNotA);

            Assert.AreEqual(1, repository.GetActive().Count);

            todoItemNotA.MarkAsCompleted();
            Assert.AreEqual(0, repository.GetCompleted().Count);
        }

        public void GettingAllItems()
        {
            var todoItem1 = new TodoItem("Something");
            var todoItem2 = new TodoItem("Something else");

            repository.Add(todoItem1);
            repository.Add(todoItem2);

            Assert.AreEqual(2,repository.GetAll().Count);
        }

        [TestMethod]
        public void GettingCompletedOnesFromDataBase()
        {
            var todoItemC = new TodoItem("Something");
            todoItemC.MarkAsCompleted();

            var todoItemNotC = new TodoItem("Something else");

            repository.Add(todoItemC);
            repository.Add(todoItemNotC);

            Assert.AreEqual(1, repository.GetCompleted().Count);

            todoItemNotC.MarkAsCompleted();
            Assert.AreEqual(2, repository.GetCompleted().Count);
        }

        [TestMethod]
        public void GettingFilteredOnesFromDataBase()
        {
            var todoItem1 = new TodoItem("Something");
            var todoItem2 = new TodoItem("Something else");

            repository.Add(todoItem1);
            repository.Add(todoItem2);

            Assert.AreEqual(1,repository.GetFiltered(i => i.Text.Length > 9).Count);
            Assert.AreEqual("Something", repository.GetFiltered(i => i.Text.Length < 10).First().Text);
        }

        [TestMethod]
        public void MarkingAsCompleted()
        {
            var todoItem1 = new TodoItem("Something");
            var todoItem2 = new TodoItem("Something else");

            todoItem1.MarkAsCompleted();

            Assert.IsTrue(todoItem1.IsCompleted);
            Assert.IsFalse(todoItem2.IsCompleted);
        }

        [TestMethod]
        public void RemovingItemFromEmptyDatabase()
        {
            Assert.IsFalse(repository.Remove(Guid.NewGuid()));
        }

        [TestMethod]
        public void RemovingItemFromNonEmptyDatabase()
        {
            var todoItem = new TodoItem("Something");

            repository.Add(todoItem);
            Assert.AreEqual(1, repository.GetAll().Count);

            repository.Remove(todoItem.Id);
            Assert.AreEqual(0, repository.GetAll().Count);
            Assert.AreNotEqual(todoItem,repository.Get(todoItem.Id));
            Assert.IsNull(repository.Get(todoItem.Id));
        }

        [TestMethod]
        public void UpdateItemInDatabase()
        {
            var todoItem = new TodoItem("Something");

            repository.Add(todoItem);
            todoItem.MarkAsCompleted();
            repository.Update(todoItem);

            Assert.AreEqual(todoItem,repository.GetAll().First());

            repository.Update(new TodoItem("Something else"));
            Assert.AreEqual(2, repository.GetAll().Count);
        }

    }
}