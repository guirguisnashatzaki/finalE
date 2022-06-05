using finalproject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using finalproject.Data;
using Microsoft.EntityFrameworkCore;

namespace finalproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly finalprojectContext _context;

        public ValuesController(finalprojectContext context)
        {

            _context = context;
        }

        Countries product = new Countries();
        [HttpPost]
        public async Task<ActionResult> post()
        {
            HttpClient client = new HttpClient();
            //Countries product = new Countries();
            HttpResponseMessage response = await client.GetAsync("https://countriesnow.space/api/v0.1/countries/population");
            string res = "";


            res = await response.Content.ReadAsStringAsync();


            var _dataResponse = JsonConvert.DeserializeObject<test>(res);


            /*foreach(Countries c in _dataResponse.data)
            {
                if (_context.Countries == null)
                {
                    return Problem("Entity set 'finalprojectContext.Countries'  is null.");
                }
                _context.Countries.Add(c);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (CountryExists(c.Country))
                    {
                        return BadRequest("Country Exists");
                    }
                    else
                    {
                        throw;
                    }
                }
            }*/

            int o = 0;
            int p = 0;
            for (int i = 0; i < _dataResponse.data.ToArray().Length; i++)
            {
                Countries c = _dataResponse.data.ToArray()[i];
                c.Id = i + 1;
                //c.code = _dataResponse.data.ToArray()[i].code;
                //c.country = _dataResponse.data.ToArray()[i].country;
               // c.Iso3 = _dataResponse.data.ToArray()[i].Iso3;

                for (int j = 0; j < c.populationCounts.ToArray().Length; j++)
                {
                    c.populationCounts.ToArray()[j].Id = o + 1;
                    o++;
                }
                p++;
                _context.Countries.Add(c);
                await _context.SaveChangesAsync();
            }





            return NoContent();
        }
        private bool CountryExists(string id)
        {
            return (_context.Countries?.Any(e => e.country == id)).GetValueOrDefault();
        }

        [HttpGet]
        public async Task<ICollection<Countries>> get()
        {
            HttpClient client = new HttpClient();
            //Countries product = new Countries();
            HttpResponseMessage response = await client.GetAsync("https://countriesnow.space/api/v0.1/countries/population");
            string res = "";


            res = await response.Content.ReadAsStringAsync();


            var _dataResponse = JsonConvert.DeserializeObject<test>(res);


            /*foreach(Countries c in _dataResponse.data)
            {
                if (_context.Countries == null)
                {
                    return Problem("Entity set 'finalprojectContext.Countries'  is null.");
                }
                _context.Countries.Add(c);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (CountryExists(c.Country))
                    {
                        return BadRequest("Country Exists");
                    }
                    else
                    {
                        throw;
                    }
                }
            }*/


            return _dataResponse.data;
        }

        [HttpGet("pagination")]
        public async Task<ActionResult<IEnumerable<Countries>>> GetPops([FromQuery] PaginationParams @params)
        {
            if (_context.Countries == null)
            {
                return NotFound();
            }
            return await _context.Countries.Skip((@params.Page - 1) * @params.ItemsPerPage).Take(@params.ItemsPerPage).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Pop>>> Getspecificcountries(int id)
        {
            if (_context.Pops == null)
            {
                return NotFound();
            }
            return await _context.Pops.Where(p=>p.Countryid==id).ToListAsync();
               
        }

    }
}




public partial class test
{
    public test()
    {
        
    }

    public bool error { get; set; }=false;
    public string msg { get; set; } = null!;
    public ICollection<Countries> data { get; set; } = null!;

}