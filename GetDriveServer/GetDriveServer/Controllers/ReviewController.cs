using BL.DTOs;
using BL.Services;
using Microsoft.AspNetCore.Mvc;

namespace GetDriveServer.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService reviewService;

        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpPost]
        public IActionResult AddReview([FromBody] ReviewDTO reviewDTO)
        {
            if (!int.TryParse(User.Identity?.Name, out int userId))
            {
                return BadRequest("Cannot get logged in user!");
            }
            if (reviewDTO.AuthorId != userId)
            {
                return BadRequest("You are not allowed to add reviews as current user");
            }
            var result = reviewService.AddReview(reviewDTO);
            if (result == null)
            {
                return BadRequest("Review can be added only if the ride departure is in past");
            }
            return Ok(result);
        }

    }
}
