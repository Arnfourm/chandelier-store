using microservice.SupplyAPI.Domain.DTO.Requests;
using microservices.SupplyAPI.Domain.DTO.Requests;
using microservices.SupplyAPI.Domain.DTO.Responses;
using microservices.SupplyAPI.Domain.Interfaces.Services;

namespace microservice.SupplyAPI.Domain.Services
{
    public class TokenService : ITokenService
    {
        private IConfiguration _config;

        private readonly string _userService;

        private string? _token;

        public TokenService(IConfiguration config)
        {
            _config = config;

            _userService = config["Microservices:UserMicroservice:Url"]
                ?? throw new ArgumentException("User microservice url is null");
        }

        public async Task<string> GetTokenAsync()
        {
            if (_token != null) return _token;

            UserLoginRequestDTO loginRequest = new UserLoginRequestDTO
            {
                Email = _config["Microservices:UserMicroservice:AccountEmail"] ?? throw new ArgumentException("User microservice email is null"),
                Password = _config["Microservices:UserMicroservice:AccountPass"] ?? throw new ArgumentException("User microservice password is null")
            };

            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (HttpClient httpClient = new HttpClient(handler))
                {
                    HttpResponseMessage response = await httpClient.PostAsJsonAsync($"{_userService}/Auth/LogIn", loginRequest);

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new Exception($"Unable to login as microservice");
                    }

                    UserLoginResponseDTO userResponse = await response.Content.ReadFromJsonAsync<UserLoginResponseDTO>()
                    ?? throw new Exception("Unable to format json as object while tring to login as microservice");

                    _token = userResponse.accessToken;
                }
            }

            return _token;
        }
    }
}
