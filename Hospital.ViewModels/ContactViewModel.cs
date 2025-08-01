using Hospital.Model;

namespace Hospital.ViewModels
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int HospitalInfoId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }


        public ContactViewModel()
        {
        }

        public ContactViewModel(Contact model)
        {
            Id = model.Id;
            Email = model.Email;
            Phone = model.Phone;
            HospitalInfoId = model.HospitalId;
      
            if(model.Hospital != null)
            {
                Name = model.Hospital.Name;
                City = model.Hospital.City;
                Country = model.Hospital.Country;
            }
        }

        public Contact ConvertViewModel()
        {
            return new Contact
            {
                Id = Id,
                HospitalId = HospitalInfoId,
                Email = Email,
                Phone = Phone
            };
        }
    }
}
