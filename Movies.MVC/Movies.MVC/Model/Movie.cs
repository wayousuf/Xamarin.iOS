using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Movies.MVC.Model
{
    public class Movie
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string ArtworkUri { get; set; }
        public string Genre { get; set; }
        public string ContentAdvisoryRating { get; set; }
        public string Description { get; set; }
    }
}