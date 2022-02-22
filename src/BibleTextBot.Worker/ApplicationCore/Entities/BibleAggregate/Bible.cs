namespace BibleTextBot.Worker.ApplicationCore.Entities.BibleAggregate
{
    using Interfaces;
    using System.Collections.Generic;

    public class Bible : IAggregateRoot
    {
        private readonly List<Book> _bookItems = new List<Book>();

        public IReadOnlyCollection<Book> BookItems => _bookItems.AsReadOnly();

        public void AddBook(Book book)
        {
            _bookItems.Add(book);
        }
    }
}