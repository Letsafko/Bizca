namespace Bizca.User.WebApi.UseCases.V1.GetUser
{
    using Bizca.Core.Application.Abstracts;
    using Bizca.User.Application.UseCases.GetUserDetail;
    using Microsoft.AspNetCore.Mvc;

    public sealed class GetUserDetailPresenter : IOutputPort
    {
        public IActionResult ViewModel { get; private set; } = new NoContentResult();
        public void Invalid(Notification notification)
        {
            ViewModel = new BadRequestObjectResult(notification.Errors);
        }

        public void NotFound()
        {
            ViewModel = new NotFoundResult();
        }

        public void Ok(GetUserDetailDto userDetail)
        {
            ViewModel = new OkObjectResult(new GetUserDetailResponse(userDetail));
        }
    }
}
