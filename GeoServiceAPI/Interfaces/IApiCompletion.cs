using GeoServiceAPI.Model.Input;
using GeoServiceAPI.Model.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoServiceAPI.Interfaces {
    public interface IApiCompletion {
        ContinentDTOutput AddContinent(ContinentDTOInput continent);
        ContinentDTOutput GetContinentForId(int id);
        CountryDTOutput GetCountryForId(int id);
        ContinentDTOutput UpdateContinent(ContinentDTOInput continent);
        void DeleteContinent(int id);
        CountryDTOutput UpdateCountry(CountryDTOInput countryIn);
        CountryDTOutput AddCountry(CountryDTOInput country);
        void DeleteCountry(int countryId);

        CityDTOutput AddCity(CityDTOInput city);
        CityDTOutput GetCityForId(int id);
        void DeleteCity(int cityId);
        CityDTOutput UpdateCity(CityDTOInput city);


        RiverDTOutput AddRiver(RiverDTOInput rivier);

        RiverDTOutput GetRiverForId(int id);
        void DeleteRiver(int id);
        RiverDTOutput UpdateRiver(RiverDTOInput rivier);
    }
}
