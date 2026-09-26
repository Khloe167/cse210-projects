namespace OnlineOrdering
{
    public class Address
    {
        private string _streetAddress;
        private string _city;
        private string _stateProvince;
        private string _country;

        public Address(string streetAddress, string city, string stateProvince, string country)
        {
            _streetAddress = streetAddress;
            _city = city;
            _stateProvince = stateProvince;
            _country = country;
        }

        public bool IsInUSA()
        {
            return _country.Trim().Equals("USA", System.StringComparison.OrdinalIgnoreCase) || 
                   _country.Trim().Equals("United States", System.StringComparison.OrdinalIgnoreCase);
        }

        public string GetFullAddress()
        {
            return $"{_streetAddress}\n{_city}, {_stateProvince}\n{_country}";
        }
    }
}