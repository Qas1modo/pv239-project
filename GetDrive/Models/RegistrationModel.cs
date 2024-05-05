using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Maui.ApplicationModel.Permissions;
using System.Xml.Linq;

namespace GetDrive.Models
{
    public partial class RegistrationModel : ModelBase
    {
        private string name = string.Empty;
        private string email = string.Empty;
        private string phone = string.Empty;
        private string password = string.Empty;
        private string repeatPassword = string.Empty;

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        public string Email
        {
            get => email;
            set => SetProperty(ref email, value);
        }

        public string Phone
        {
            get => phone;
            set => SetProperty(ref phone, value);
        }

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public string RepeatPassword
        {
            get => repeatPassword;
            set => SetProperty(ref repeatPassword, value);
        }

        public bool IsFormValid()
        {
            return !string.IsNullOrWhiteSpace(Name) &&
                   !string.IsNullOrWhiteSpace(Email) &&
                   !string.IsNullOrWhiteSpace(Phone) &&
                   !string.IsNullOrWhiteSpace(Password) &&
                   Password == RepeatPassword &&
                   Password.Length >= 8;
        }
    }
}
