using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class StoreController : Controller
    {
       
        // GET: Store
        public ActionResult Index()
        {
            var genres = new List<GenreModel>();

            using (iLead2021Entities1 ctx = new iLead2021Entities1())
            {
                foreach (var dbrow in ctx.Genres)
                {
                    genres.Add(new GenreModel { Name = dbrow.Name });
                }

            }
            //           var genres = new List<GenreModel>
            //{
            //new GenreModel { Name = "Disco"},
            //new GenreModel { Name = "Jazz"},
            //new GenreModel { Name = "Rock"}
            //};
            return View(genres);


        }
        // GET: /Store/Browse
        public ActionResult Browse(string genre)
        {
            GenreModel genreModel = new GenreModel();

            using (iLead2021Entities1 ctx = new iLead2021Entities1())
            {
                List<AlbumModel> albums = new List<AlbumModel>();
                var dbGenre = ctx.Genres.Include("Albums").Single(g => g.Name == genre);
                // genreModel = new GenreModel { Name = genre, Description = dbGenre.Description };
                // var genreModel = new GenreModel { Name = genre };
                foreach (var item in  dbGenre.Albums)
                {
                    albums.Add(new AlbumModel
                    {
                        AlbumId = item.AlbumId,
                        Title = item.Title,
                        Artist = new Artist { Name = item.Artist.Name },
                        ArtistId = item.ArtistId,
                        Genre = new GenreModel { Name = item.Genre.Name, Description = item.Genre.Description },
                        GenreId = item.GenreId,
                        Price = item.Price,
                        AlbumArtUrl = item.AlbumArtUrl
                    });
                }

                genreModel = new GenreModel
                {
                    Name = dbGenre.Name,
                    Description = dbGenre.Description,
                    Albums = albums
                };
            }
            return View(genreModel);
        }

        //
        // GET: /Store/Details
        public ActionResult Details(int AlbumId)
        {
            //var album = new Album { Title = "Album " + id };
            AlbumModel albumModel = new AlbumModel();
            using (iLead2021Entities1 ctx = new iLead2021Entities1())
            {
                // var result = ctx.Albums.Where(x => x.AlbumId == AlbumId).ToList();
                var result = ctx.Albums.Find(AlbumId);
                ViewBag.AlbumArtUrl = result.AlbumArtUrl;
                ViewBag.ArtistName = result.Artist.Name;
                ViewBag.GenreName = result.Genre.Name;
                return View(result);

            }
           
        }

        //
        // GET: /Store/GenreMenu
        [ChildActionOnly]
        public ActionResult GenreMenu()
        {
            iLead2021Entities1 ctx = new iLead2021Entities1();
            var genres = ctx.Genres.ToList();
            return PartialView(genres);
        }


    }
}