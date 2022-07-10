using BLL.Models;

namespace Web_API.Models
{
    /// <summary>
    /// The class is a wrapper for two signatures: the JWT token and the authorized user
    /// </summary>
    class Response
    {
        /// <summary>
        /// JWT Token
        /// </summary>
        public readonly string token;

        /// <summary>
        /// The user has logged in to the system
        /// </summary>
        public UserModel User { get; set; }
        public Response(string token, UserModel user)
        {
            this.token = token;
            this.User = user;
        }
    }
}
