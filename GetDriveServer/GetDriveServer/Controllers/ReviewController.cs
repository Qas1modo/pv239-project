using BL.DTOs;
using BL.ResponseDTOs;
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
        [ProducesResponseType(typeof(ApiErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiSuccessResponseDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> AddReview([FromBody] ReviewDTO reviewDTO)
        {
            if (!ModelState.IsValid)
            {
                var errorMessage = ModelState.Values.SelectMany(v => v.Errors)
                               .Select(e => e.ErrorMessage)
                               .Aggregate((a, b) => a + Environment.NewLine + b);
                return BadRequest(new ApiErrorResponseDTO(errorMessage));
            }
            if (!int.TryParse(User.Identity?.Name, out int authorId))
            {
                return BadRequest(new ApiErrorResponseDTO("Cannot get logged in user!"));
            }
            var result = await reviewService.AddReview(reviewDTO, authorId);
            if (!result)
            {
                return BadRequest(new ApiErrorResponseDTO("Review can be added only once to valid user"));
            }
            return Ok(new ApiSuccessResponseDTO("Review added successfully"));
        }


        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ApiErrorResponseDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiSuccessResponseDTO), StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteReview(int id)
        {
            if (!int.TryParse(User.Identity?.Name, out int authorId))
            {
                return BadRequest(new ApiErrorResponseDTO("Cannot get logged in user!"));
            }
            var result = await reviewService.DeleteReview(id, authorId);
            if (!result)
            {
                return BadRequest(new ApiErrorResponseDTO("Review can be deleted only by its creator and must exist"));
            }
            return Ok(new ApiSuccessResponseDTO("Review added successfully"));
        }
    }
}
