using magnadigi.Models;


namespace magnadigi.Services.ISecurityService
{
    public class ISecurityService
    {

        UsersDAO usersDAO = new UsersDAO();
        public ISecurityService()
        {

        }
        /**
         * This method checks for a username and password match 
         * @param UserModel the userModel from the sign in screen
         * @return UserModel with detailed parameters from the data base
         * 
         */
        public UserModel isValid(UserModel user)
        {
            UserModel returnIfFoundModel = new UserModel();
            //returnIfFoundModel = usersDAO.findUserByNameAndPassword(user);

            return returnIfFoundModel;
        }


    }
}

