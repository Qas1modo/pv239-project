using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GetDrive.Api;
using GetDrive.Clients;
using GetDrive.Models;
using GetDrive.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDrive.ViewModels
{
    public partial class ReviewViewModel : ViewModelBase
    {
        private readonly IRoutingService _routingService;
        private readonly IReviewClient _reviewClient;
        private readonly IMapper _mapper;

        [ObservableProperty]
        private ReviewDTO review = new();

        [ObservableProperty]
        private int score;

        [RelayCommand]
        public void SetScore(String ratingString)
        {
            int.TryParse(ratingString, out int rating);
            Score = rating;
            Review.Score = rating;
        }

        [ObservableProperty]
        private string message = string.Empty;

        [ObservableProperty]
        private string messageColour = "#000000";

        public ReviewViewModel(IRoutingService routingService, IReviewClient reviewClient, IMapper mapper)
        {
            _routingService = routingService;
            _reviewClient = reviewClient;
            _mapper = mapper;
            Score = 0;
        }

        public override async Task OnAppearingAsync()
        {
            await base.OnAppearingAsync();
        }

        [RelayCommand]
        private async Task AddReview()
        {
            var userIdString = await SecureStorage.GetAsync("UserId");
            if (!int.TryParse(userIdString, out int userId))
            {
                var authRoute = _routingService.GetRouteByViewModel<AuthViewModel>();
                await Shell.Current.GoToAsync(authRoute);
            }
            Review.UserId = userId;

            var result = await _reviewClient.CreateReviewAsync(Review);
            MessageColour = "#FF0000";
            if (result.StatusCode == 200)
            {
                Message = "Review successfully created";
                MessageColour = "#00FF00";
            }
            if (result.StatusCode == 401)
            {
                Message = "You must sign in before you can create a review.";
            }
            else if (!string.IsNullOrEmpty(result.ErrorMessage))
            {
                Message = result.ErrorMessage;
            }
            else
            {
                Message = "Unknown error!";
            }
        }
    }
}
