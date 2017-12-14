using Foundation;
using Movies.MVC.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UIKit;

namespace Movies.MVC
{
    public partial class MovieTableViewController : UITableViewController, IUISearchResultsUpdating
    {
        private UISearchController _searchController;
        private NSString _cellId = new NSString("MovieCell");
        private IReadOnlyList<Movie> _movies;

        public MovieTableViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            _searchController = new UISearchController(searchResultsController: null)
            {
                HidesNavigationBarDuringPresentation = true,
                ObscuresBackgroundDuringPresentation = false,
                SearchResultsUpdater = this,
            };

            _searchController.SearchBar.Placeholder = "Search Movies";

            TableView.TableHeaderView = _searchController.SearchBar;

        }

        private async Task UpdateMovieListings(string searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                var movieService = new MovieService();
                _movies = await movieService.GetMoviesForSearchAsync(searchTerm);
            }
            else
            {
                _movies = null;
            }

            TableView.ReloadData();
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return _movies?.Count ?? 0;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(_cellId);

            if (cell == null)
                cell = new UITableViewCell(UITableViewCellStyle.Subtitle, _cellId);

            var movie = _movies[indexPath.Row];
            cell.TextLabel.Text = movie.Title;
            cell.DetailTextLabel.Text = movie.Description;

            return cell;
        }

        public async void UpdateSearchResultsForSearchController(UISearchController searchController)
        {
            var searchTerm = searchController.SearchBar.Text;
            await UpdateMovieListings(searchTerm);
        }
    }
}