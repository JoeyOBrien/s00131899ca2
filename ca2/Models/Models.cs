using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using WebGrease.Css.Extensions;

namespace ca2.Models
{
    public enum Sex { Male, Female }

    public class MovieDbInitialiser : DropCreateDatabaseAlways<MovieDb>
    {
        protected override void Seed(MovieDb context)
        {
            #region//create list of actors
            var actors = new List<Actor>() 
            {
                new Actor(){ScreenName = "Actor 1",Sex = Sex.Male},
                new Actor(){ScreenName = "Actor 2",Sex = Sex.Female},
                new Actor(){ScreenName = "Actor 3",Sex = Sex.Male},
                new Actor(){ScreenName = "Actor 4",Sex = Sex.Female},
                new Actor(){ScreenName = "Actor 5",Sex = Sex.Male},
                new Actor(){ScreenName = "Actor 6",Sex = Sex.Female},
                new Actor(){ScreenName = "Actor 7",Sex = Sex.Male},
                new Actor(){ScreenName = "Actor 8",Sex = Sex.Female},
                new Actor(){ScreenName = "Actor 9",Sex = Sex.Male},
                new Actor(){ScreenName = "Actor 10",Sex = Sex.Female}
            };
            //adds actors to database and saves changes
            actors.ForEach(act => context.Actors.Add(act));
            context.SaveChanges();
            #endregion

            #region//create list of directors
            var directors = new List<Director>()
            {
                new Director(){Name="Director 1"},
                new Director(){Name="Director 2"},
                new Director(){Name="Director 3"},
            };
            directors.ForEach(dir => context.Directors.Add(dir));
            context.SaveChanges();
            #endregion

            #region//create list of movies
            var movies = new List<Movie>()
            {
                new Movie() {
                    Title = "Movie 1",
                    ReleaseDate = new DateTime(2015,1,1),
                    Director= getRandomNumEntity(directors,1).First(),
                    MovieActors = new List<MovieActor>()},
                    new Movie() {
                    Title = "Movie 2",
                    ReleaseDate = new DateTime(2015,2,2),
                    Director= getRandomNumEntity(directors,1).First(),
                    MovieActors = new List<MovieActor>()},
                    new Movie() {
                    Title = "Movie 3",
                    ReleaseDate = new DateTime(2015,3,3),
                    Director= getRandomNumEntity(directors,1).First(),
                    MovieActors = new List<MovieActor>()},
                    new Movie() {
                    Title = "Movie 4",
                    ReleaseDate = new DateTime(2015,4,4),
                    Director= getRandomNumEntity(directors,1).First(),
                    MovieActors = new List<MovieActor>()},
                    new Movie() {
                    Title = "Movie 5",
                    ReleaseDate = new DateTime(2015,5,5),
                    Director= getRandomNumEntity(directors,1).First(),
                    MovieActors = new List<MovieActor>()},

            };
            movies.ForEach(mov => context.Movies.Add(mov));
            context.SaveChanges();
            #endregion

            //adds random number of actors to each movie in the database
            Random r = new Random();
            context.Movies.ForEach(mov => getRandomNumEntity(actors, r.Next(5)).ForEach(act => mov.MovieActors.Add(
                    new MovieActor()
                    {
                        MovieId = mov.MovieId,
                        ActorId = act.ActorId
                    })));

            base.Seed(context);
        }

        //method to return a random number of objects of a type
        private IEnumerable<T> getRandomNumEntity<T>(List<T> dataSet, int takeNumber = 3, bool noRepeat = true)
        {
            Random rnd = new Random(Environment.TickCount);
            var returnList = new List<T>();
            T selected;
            int skipVal;
            System.Threading.Thread.Sleep(150); // helps with improving randomisation
            while (returnList.Count < takeNumber)   // repeat for number required
            {
                do
                {
                    skipVal = (int)(rnd.NextDouble() * dataSet.Count);  // offset into collection randomly
                    selected = dataSet.Skip(skipVal).Take(1).First();   // take 1 from there
                } while (returnList.Contains(selected) || !noRepeat);    // repeat if already chosen unless indicated in noRepeat
                returnList.Add(selected);
            }
            return returnList;
        }
    }

    public class MovieDb : DbContext
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Director> Directors { get; set; }

        public MovieDb()
            : base("MovieDb")
        { }
    }

    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        [Display(Name = "Movie Title"), Required]
        public string Title { get; set; }
        [Display(Name = "Release Date"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime ReleaseDate { get; set; }
        public int DirectorId { get; set; }
        // Nav properties
        public virtual Director Director { get; set; }
        public virtual List<MovieActor> MovieActors { get; set; }
        // Helper to serve up Actors in a Movie
        public virtual List<Actor> Actors
        {
            get { return (MovieActors == null) ? null : MovieActors.Select(ma => ma.Actor).ToList(); }
        }
    }

    public class Actor
    {
        [Key]
        public int ActorId { get; set; }
        [Display(Name = "Screen Name"), Required]
        public string ScreenName { get; set; }
        [Required]
        public Sex Sex { get; set; }
        public int selectedMovieId { get; set; }
        public virtual List<MovieActor> ActorMovies { get; set; }
        [NotMapped]
        public virtual List<Movie> Movies
        {
            get { return (ActorMovies == null) ? null : ActorMovies.Select(am => am.Movie).ToList(); }
        }
    }

    public class Director
    {
        [Key]
        public int DirectorId { get; set; }
        [Required]
        public string Name { get; set; }
    }

    public class MovieActor
    {
        [Key, Column(Order = 0)]    // Composite key (first key)
        public int MovieId { get; set; }
        [Key, Column(Order = 1)]        // Composite key (second key)
        public int ActorId { get; set; }
        // Nav Properties
        public virtual Movie Movie { get; set; }
        public virtual Actor Actor { get; set; }
    }


}