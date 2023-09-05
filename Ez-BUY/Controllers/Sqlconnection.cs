using OnlineMovieV04.Models;

namespace OnlineMovieV04.Controllers
{
    internal class Sqlconnection
    {
        private OnlineMovieTicketEntities onlineMovieTicketEntities;

        public Sqlconnection(OnlineMovieTicketEntities onlineMovieTicketEntities)
        {
            this.onlineMovieTicketEntities = onlineMovieTicketEntities;
        }
    }
}