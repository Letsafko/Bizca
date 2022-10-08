namespace Bizca.Bff.WebApi.UseCases.V10.GetUsers
{
    using Application.UseCases.GetUsers;
    using Core.Api.Modules.Presentation;
    using Core.Api.Modules.Presentation.HttpStrategies;
    using Microsoft.AspNetCore.Mvc;
    using ViewModels;

    /// <summary>
    ///     Get users by criteria presenter.
    /// </summary>
    public sealed class GetUsersPresenter : PresenterBase, IGetUsersOutput
    {
        /// <summary>
        ///     Creates an instance of <see cref="GetUsersPresenter" />
        /// </summary>
        /// <param name="strategyFactory"></param>
        public GetUsersPresenter(IHttpStrategyFactory strategyFactory) : base(strategyFactory)
        {
        }

        /// <summary>
        ///     Standard output.
        /// </summary>
        /// <param name="pagedUsers"></param>
        public void Ok(GetPagedUsersDto pagedUsers)
        {
            if (!(pagedUsers is null)) ViewModel = new OkObjectResult(new UserPaginationViewModel(pagedUsers));
        }
    }
}