namespace Bizca.Bff.WebApi.UseCases.V10.CreateOrUpdateUserPassword
{
    using Bizca.Bff.Application.UseCases.CreateOrUpdateUserPassword;
    internal sealed class CreateOrUpdateUserPasswordResponse
    {
        public CreateOrUpdateUserPasswordResponse(CreateOrUpdateUserPasswordDto createOrUpdateUserPasswordDto)
        {
            Success = createOrUpdateUserPasswordDto.Success;
        }

        /// <summary>
        ///     Indicates whether creation or update of password ended successfully.
        /// </summary>
        public bool Success { get; }
    }
}