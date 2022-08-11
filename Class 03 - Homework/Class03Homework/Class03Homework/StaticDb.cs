using Class03Homework.Models;

namespace Class03Homework
{
    public static class StaticDb
    {
        public static List<Book> Books = new List<Book>()
        {
            new Book() { Author = "JK Rowling", Title = "Harry Potter" },
            new Book() { Author = "F. Scott Fitzgerald", Title = "The Great Gatsby" },
            new Book() { Author = "Arthur Conan Doyle", Title = "Sherlock Holmes" }
        };
    }
}
