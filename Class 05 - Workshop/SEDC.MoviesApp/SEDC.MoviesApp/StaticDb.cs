using SEDC.MoviesApp.Enums;
using SEDC.MoviesApp.Models;

namespace SEDC.MoviesApp
{
    public static class StaticDb
    {
        public static int MovieId = 5;
        public static List<Movie> Movies = new List<Movie>()
        {
             new Movie()
             {  Id =  1,
                 Title = "The Expendables",
                 Description = "The most viewed action movie in the last decade, starring Stallone, Schwarzenegger, Statham, Jet Li, etc.",
                 Year = 2010,
                 Genre = Genre.Action
             },
            new Movie()
             {  Id =  2,
                 Title = "Grown Ups",
                 Description = "Grown Ups is a 2010 American comedy film directed by Dennis Dugan, written by Adam Sandler and Fred Wolf.",
                 Year = 2010,
                 Genre = Genre.Comedy
             },
            new Movie()
             {  Id =  3,
                 Title = "Hustle",
                 Description = "After a down-on-his-luck basketball scout discovers an extraordinary player abroad and brings him to the NBA.",
                 Year = 2022,
                 Genre = Genre.Drama
             },
            new Movie()
             {  Id =  4,
                 Title = "Project Adam",
                 Description = "The Adam Project is a 2022 American science fiction adventure film, starring Ryan Reynolds as the main act.",
                 Year = 2022,
                 Genre = Genre.SciFi,
             },
            new Movie()
             {  Id =  5,
                 Title = "The Gray Man",
                 Description = "When a shadowy CIA agent uncovers damning agency secrets, he's hunted across the globe by. With Ryan Gosling",
                 Year = 2022,
                 Genre = Genre.Action,
             },
        };
    }
}
