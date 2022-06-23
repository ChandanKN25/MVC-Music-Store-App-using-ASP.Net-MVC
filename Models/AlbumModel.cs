using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class AlbumModel
    {

        public int AlbumId { get; set; }
        public int GenreId { get; set; }
        public int ArtistId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string AlbumArtUrl { get; set; }
        public GenreModel Genre { get; set; }

        public Artist Artist { get; set; }

        public virtual List<OrderDetail> OrderDetails { get; set; }

    }
       
         



        }