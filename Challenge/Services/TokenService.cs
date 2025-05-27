using Application.Settings;
using Common.Services;
using DotNetTraining.Repositories;
using System.Data;

namespace DotNetTraining.Services
{
    public class TokenService(IServiceProvider services, ApplicationSetting setting, IDbConnection connection) : BaseService(services)
    {
        private readonly TokenRepository _repository = new (connection);




    }
}
