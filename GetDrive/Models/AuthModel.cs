using CommunityToolkit.Mvvm.ComponentModel;

namespace GetDrive.Models;

public class AuthModel : ModelBase
{
    private RegistrationModel registration = new();
    private LoginModel login = new();
    private ChangePasswordModel changePassword = new();
    private bool isRegistering;
    private bool isLoggedIn;
    private string statusMessage = string.Empty;
    private string messageColour ="#000000";
    private string changePasswordStatusMessage = string.Empty;
    private string toggleButtonText = "Switch to Login form";

    public RegistrationModel Registration
    {
        get => registration;
        set => SetProperty(ref registration, value);
    }

    public LoginModel Login
    {
        get => login;
        set => SetProperty(ref login, value);
    }

    public bool IsRegistering
    {
        get => isRegistering;
        set => SetProperty(ref isRegistering, value);
    }

    public bool IsLoggedIn
    {
        get => isLoggedIn;
        set => SetProperty(ref isLoggedIn, value);
    }

    public string StatusMessage
    {
        get => statusMessage;
        set => SetProperty(ref statusMessage, value);
    }
    public string MessageColour
    {
        get => messageColour;
        set => SetProperty(ref messageColour, value);
    }

    public string ChangePasswordStatusMessage
    {
        get => changePasswordStatusMessage;
        set => SetProperty(ref changePasswordStatusMessage, value);
    }

    public string ToggleButtonText
    {
        get => toggleButtonText;
        set => SetProperty(ref toggleButtonText, value);
    }


    public ChangePasswordModel ChangePassword
    {
        get => changePassword;
        set => SetProperty(ref changePassword, value);
    }

}
