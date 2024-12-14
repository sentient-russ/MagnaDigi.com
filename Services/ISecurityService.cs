using magnadigi.Models;


namespace magnadigi.Services.ISecurityService
{
    public class ISecurityService
    {
        UsersDAO usersDAO = new UsersDAO();
        public ISecurityService()
        {

        }
        public UserModel isValid(UserModel user)
        {
            UserModel returnIfFoundModel = new UserModel();
            //returnIfFoundModel = usersDAO.findUserByNameAndPassword(user);

            return returnIfFoundModel;
        }


    }
}

