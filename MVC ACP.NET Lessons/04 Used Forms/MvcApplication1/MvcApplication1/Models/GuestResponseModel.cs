using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcApplication1.Models
{
    public class GuestResponseModel : IDataErrorInfo
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool? WillAttend { get; set; }
        public string Error { get { return null; } }

        public string this[string propName]
        {
            get
            {
                if ((propName == "Name") && string.IsNullOrEmpty(Name))
                    return "Имя не введено";
                else if ((propName == "Email") && string.IsNullOrEmpty(Email))
                    return "Некорректный е-майл";
                else if ((propName == "Phone") && string.IsNullOrEmpty(Phone))
                    return "Телефон не введен";
                else if ((propName == "WillAttend") && !WillAttend.HasValue)
                    return "Никак не определишься ?";

                return null;
            }
        }

    }

}
