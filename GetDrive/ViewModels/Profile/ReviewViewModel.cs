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
        private ReviewListModel review = new();

        [ObservableProperty]
        private int rating;

        public ObservableCollection<string> StarImages { get; set; } = new ObservableCollection<string>();

        [ObservableProperty]
        private string message = string.Empty;

        [ObservableProperty]
        private string messageColour = "#000000";

        public ReviewViewModel(IRoutingService routingService, IReviewClient reviewClient, IMapper mapper)
        {
            _routingService = routingService;
            _reviewClient = reviewClient;
            _mapper = mapper;
            InitializeStarRatings();
        }

        public override async Task OnAppearingAsync()
        {
            await base.OnAppearingAsync();
        }

        private void InitializeStarRatings()
        {
            for (int i = 0; i < 5; i++)
            {
                StarImages.Add("star_empty.svg");
            }
        }

        [RelayCommand]
        private void Rate(int starValue)
        {
            Rating = starValue;
            for (int i = 0; i < StarImages.Count; i++)
            {
                StarImages[i] = i < starValue ? "star_filled.svg" : "star_empty.svg";
            }
        }

        [RelayCommand]
        private async Task AddReview()
        {
            var createReviewDTO = _mapper.Map<ReviewDTO>(Review);
            var result = await _reviewClient.CreateReviewAsync(createReviewDTO);
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
