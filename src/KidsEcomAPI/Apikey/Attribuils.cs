using Microsoft.AspNetCore.Mvc;

namespace KidsEcomAPI.Apikey
{
    public class Attribuils : ServiceFilterAttribute
    {
        public Attribuils() : base(typeof(Fillter))
        {
        }
    }
}
