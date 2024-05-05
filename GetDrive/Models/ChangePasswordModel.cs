using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDrive.Models
{
    public partial class ChangePasswordModel : ModelBase
    {
        private string oldPassword = string.Empty;
        private string newPassword = string.Empty;
        private string confirmPassword = string.Empty;

        public string OldPassword
        {
            get => oldPassword;
            set => SetProperty(ref oldPassword, value);
        }

        public string NewPassword
        {
            get => newPassword;
            set => SetProperty(ref newPassword, value);
        }

        public string ConfirmPassword
        {
            get => confirmPassword;
            set => SetProperty(ref confirmPassword, value);
        }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(OldPassword) &&
                   !string.IsNullOrWhiteSpace(NewPassword) &&
                   !string.IsNullOrWhiteSpace(ConfirmPassword) &&
                   NewPassword == ConfirmPassword &&
                   NewPassword.Length >= 8;
        }
    }

}
