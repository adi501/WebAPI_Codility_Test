using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Codility_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public const string CountryCodeHeaderName = "x-test-country-code";

        private readonly IRepository _repository;

        public ValuesController(
            IRepository repository
        )
        {
            _repository = repository;
        }

        // Return UnauthorizedResult() or OkObjectResult(ICollection<Store>)
        public IActionResult GetStores()
        {
            string countryCode = "";
            Request.Headers.TryGetValue(CountryCodeHeaderName, out var headers);
            if (headers.Count > 1 || string.IsNullOrWhiteSpace(headers.FirstOrDefault()))
            {
                return new UnauthorizedResult();
            }
            else
            {
                countryCode = headers.FirstOrDefault();
            }
            List<Store> objStores = new List<Store>();
            objStores = _repository.GetStores(s => s.CountryCode == countryCode, false).ToList();
            return Ok(objStores);
            // Console.WriteLine("Sample debug output");
            //throw new NotImplementedException("TODO");
        }

        // Return UnauthorizedResult(), NotFoundResult(), ForbidResult() or OkObjectResult(Store)
        public IActionResult GetStore(int storeId, bool includeCustomers = false)
        {
            string countryCode = "";
            Request.Headers.TryGetValue(CountryCodeHeaderName, out var headers);
            if (headers.Count > 1 || string.IsNullOrWhiteSpace(headers.FirstOrDefault()))
            {
                return new UnauthorizedResult();
            }
            else
            {
                countryCode = headers.FirstOrDefault();
            }
            List<Store> objStores = new List<Store>();
            objStores = _repository.GetStores(s => s.StoreId == storeId).ToList();
            if (objStores.Count == 0)
            {
                return new NotFoundResult();
            }
            else
            {
                List<Store> objStoresWithCountry = new List<Store>();
                objStoresWithCountry = objStores.Where(o => o.CountryCode == countryCode).ToList();
                if (objStoresWithCountry.Count == 0)
                {
                    return StatusCode(403);
                }
                else
                {
                    return Ok(objStoresWithCountry);
                }
            }

            // throw new NotImplementedException("TODO");
        }

        // Return UnauthorizedResult(), BadRequestResult() or OkObjectResult(Customer)
        public IActionResult CreateCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                Request.Headers.TryGetValue(CountryCodeHeaderName, out var headers);
                if (headers.Count > 1 || string.IsNullOrWhiteSpace(headers.FirstOrDefault()))
                {
                    return new UnauthorizedResult();
                }
                return Ok(_repository.AddCustomer(customer));
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
