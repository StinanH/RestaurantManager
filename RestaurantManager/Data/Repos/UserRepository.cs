using RestaurantManager.Data.Repos.IRepos;

namespace RestaurantManager.Data.Repos
{
    public class UserRepository : UserIRepository
    {
        public readonly RestaurantManagerContext _context;

        public UserRepository(RestaurantManagerContext context)
        {

            _context = context;
        }
    }
}
