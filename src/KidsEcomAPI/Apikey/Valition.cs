namespace KidsEcomAPI.Apikey
{
    public class Valition : Check
    {
        private IConfiguration _configuration;
        public Valition(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public bool CheckKey(string KeyApi)
        {
            if (string.IsNullOrEmpty(KeyApi)) { 
            return false;
            }
            string? key = _configuration.GetValue<string>(BienSo.ApiKey);
            if(key == null || key != KeyApi)
            {
                return false;
            }
            return true;
        }
    }
}
