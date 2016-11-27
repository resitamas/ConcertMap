using Maddalena;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountriesService
{
    public class CountryManager
    {

        public static IEnumerable<ReducedCountry> GetCountries()
        {
            List<ReducedCountry> countries = new List<ReducedCountry>();
            var srb = Country.UnitedStates;
            foreach (var country in Country.All.ToList())
            {

                countries.Add(new ReducedCountry() {

                    ISO2 = country.CountryCode.ToString(),
                    ISO3 = country.ISO3.ToString(),
                    CommonName = country.CommonName,
                    OfficalName = country.OfficialName,
                    Region = country.Region

                });

            }

            return countries;
        }

    }
}
