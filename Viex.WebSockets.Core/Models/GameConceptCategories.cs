using System.Collections.Generic;

namespace Viex.WebSockets.Core.Models
{
    public static class GameConceptCategories
    {
        public static readonly string Animal = "Animal";
        public static readonly string Color = "Color";
        public static readonly string CountryCity = "Country/City";
        public static readonly string FirstName = "First Name";
        public static readonly string FruitVeggie = "Fruit/Veggie";
        public static readonly string LastName = "Last Name";
        public static readonly string Thing = "Thing";

        public static readonly IList<string> All = new List<string>
        {
            Animal,
            Color,
            CountryCity,
            FirstName,
            FruitVeggie,
            LastName,
            Thing,
        };
    }
}
