using RestaurantManager.Data.Repos.IRepos;

namespace RestaurantManager.Data.Repos
{
    public class BookingRepository : BookingIRepository
    {
        public readonly RestaurantManagerContext _context;

        public BookingRepository(RestaurantManagerContext context) {

            _context = context;
        }

    }
}
