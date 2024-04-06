using BL.DTOs;
using BL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GetDriveServer.Controllers
{
    [Route("review")]
    public class ReviewController : Controller
    {
        private readonly IReviewService reviewService;

        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddReview([FromBody] ReviewDTO reviewDTO)
        {
            if (reviewDTO == null)
            {
                return BadRequest("Invalid Data");
            }
            if (!int.TryParse(User.Identity?.Name, out int authorId))
            {
                return BadRequest("Cannot get logged in user!");
            }
            var result = await reviewService.AddReview(reviewDTO, authorId);
            if (!result)
            {
                return BadRequest("Review can be added only once to valid user");
            }
            return Ok("Review added succesfully");
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteReview(int id)
        {
            if (!int.TryParse(User.Identity?.Name, out int authorId))
            {
                return BadRequest("Cannot get logged in user!");
            }
            var result = await reviewService.DeleteReview(id, authorId);
            if (!result)
            {
                return BadRequest("Review can be deleted only by its creator and must exist");
            }
            return Ok("Review added succesfully");
        }
    }
}
