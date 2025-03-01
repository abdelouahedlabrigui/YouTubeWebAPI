using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YouTubeWebAPI.Models.Auhentication;

namespace YouTubeWebAPI.Repository
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(User users);
    }
}