using FSL.DatabaseQuery.Core.Models;
using FSL.Framework.Core.ApiClient.Provider;
using FSL.Framework.Core.ApiClient.Service;
using FSL.Framework.Core.Authentication.Service;
using FSL.Framework.Core.Factory.Service;
using System.Threading.Tasks;

namespace FSL.DatabaseQuery.Core.Service
{
    public sealed class DatabaseApiClientService : IApiClientService
    {
        private readonly MyConfiguration _configuration;
        private readonly IFactoryService _factoryService;
        //private readonly ILoggedUserService _loggedUserService;

        public DatabaseApiClientService(
            MyConfiguration configuration,
            IFactoryService factoryService//,
            //ILoggedUserService loggedUserService
            )
        {
            _configuration = configuration;
            _factoryService = factoryService;
            //_loggedUserService = loggedUserService;
        }

        public async Task<IApiClientProvider> CreateInstanceAsync()
        {
            var instance = _factoryService
               .InstanceOf<IApiClientProvider>()
               .UseJsonContentType()
               .UseBaseUrl(_configuration.ApiUrl);

            //var user = await _loggedUserService.GetLoggedUserAsync<MyLoggedUser>();

            //instance.UseAuthenticationBearer(user?.Data?.AccessToken);

            return await Task.FromResult(instance);
        }
    }
}
