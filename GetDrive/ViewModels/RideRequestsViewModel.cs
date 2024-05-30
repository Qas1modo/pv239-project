using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetDrive.Clients;
using GetDrive.Models;
using System.Collections.ObjectModel;

namespace GetDrive.ViewModels;

public partial class RideRequestsViewModel : ViewModelBase
{
    private readonly IUserRideClient _userRideClient;
    private readonly IMapper _mapper;

    [ObservableProperty]
    private ObservableCollection<RideListModel>? rideRequests;

    [ObservableProperty]
    private RideRequestsModel rideModel = new();

    public RideRequestsViewModel(IUserRideClient requestsClient, IMapper mapper)
    {
        _userRideClient = requestsClient;
        _mapper = mapper;
    }

    public override async Task OnAppearingAsync()
    {
        var token = await SecureStorage.GetAsync("Token");
        if (string.IsNullOrEmpty(token))
        {
            await Shell.Current.GoToAsync("//auth");
            return;
        }
        await ShowReceived();
    }

    [RelayCommand]
    public async Task ShowReceived()
    {
        RideModel.Incoming = false;
        RideModel.Outgoing = false;
        RideRequests = new ObservableCollection<RideListModel>();
        var requests = await _userRideClient.GetDriverRequests();
        RideModel.Incoming = true;
        if (requests.StatusCode == 200)
        {
            if (requests.Response == null)
            {
                return;
            }
            if (requests.Response.Count() == 0)
            {
                RideModel.Message = "No incoming requests found";
                return;
            }

            RideModel.Message = string.Empty;
            RideRequests = _mapper.Map<ObservableCollection<RideListModel>>(requests.Response);
        }
        else if (!string.IsNullOrEmpty(requests.ErrorMessage))
        {
            RideModel.Message = requests.ErrorMessage;
        }
        else
        {
            RideModel.Message = "Unknown Error Occurred";
        }
    }

    [RelayCommand]
    public async Task ShowSent()
    {
        RideModel.Incoming = false;
        RideModel.Outgoing = false;
        RideRequests = new ObservableCollection<RideListModel>();
        var requests = await _userRideClient.GetPassengerRequests();
        RideModel.Outgoing = true;
        if (requests.StatusCode == 200)
        {
            if (requests.Response == null)
            {
                return;
            }
            if (requests.Response.Count() == 0)
            {
                RideModel.Message = "No outgoing requests found";
                return;
            }
            RideModel.Message = string.Empty;
            RideRequests = _mapper.Map<ObservableCollection<RideListModel>>(requests.Response);
            return;
        }
    }

    [RelayCommand]
    public async Task AcceptRide(int id)
    {
        var result = await _userRideClient.AcceptRide(id);
        if (result.StatusCode == 200)
        {
            var request = RideRequests?.FirstOrDefault(r => r.Id == id);
            if (request == null) return;
            RideRequests?.Remove(request);
            if (RideRequests?.Count == 0)
            {
                RideModel.Message = "No incoming requests found";
            }
        }
        else if (!string.IsNullOrEmpty(result.ErrorMessage))
        {
            RideModel.Message = result.ErrorMessage;
        }
        else
        {
            RideModel.Message = "Unknown Error Occurred";
        }
    }

    [RelayCommand]
    public async Task DeleteRequest(int id)
    {
        var result = await _userRideClient.DeleteRequest(id);
        if (result.StatusCode == 200)
        {
            var request = RideRequests?.FirstOrDefault(r => r.Id == id);
            if (request == null) return;
            RideRequests?.Remove(request);
            if (RideRequests?.Count == 0)
            {
                RideModel.Message = "No incoming requests found";
            }
        }
        else if (!string.IsNullOrEmpty(result.ErrorMessage))
        {
            RideModel.Message = result.ErrorMessage;
        }
        else
        {
            RideModel.Message = "Unknown Error Occurred";
        }
    }
}

