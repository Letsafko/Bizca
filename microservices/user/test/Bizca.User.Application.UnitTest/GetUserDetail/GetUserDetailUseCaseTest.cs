namespace Bizca.User.Application.UnitTest.GetUser.Detail
{
    using Bizca.Core.Domain;
    using Bizca.Core.Support.Test;
    using Bizca.Core.Support.Test.Builders;
    using Bizca.User.Application.UseCases.GetUserDetail;
    using Bizca.User.Application.UseCases.GetUsersByCriteria;
    using Bizca.User.Domain.Agregates.Repositories;
    using Core.Domain.Referential.Model;
    using Core.Domain.Referential.Repository;
    using Core.Domain.Referential.Services;
    using Microsoft.AspNetCore.Mvc;
    using NFluent;
    using NSubstitute;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public sealed class GetUserDetailUseCaseTest
    {
        [Theory]
        [ClassData(typeof(GetUserDetailUseCaseDataSetup))]
        public void OneOfArgumentIsNull_ThrowArgumentNullException((IGetUserDetailOutput output,
            IUserRepository userRepository,
            IReferentialService referentialService) tuple)
        {
            Check.ThatCode(() =>
            {
                GetUserDetailUseCaseBuilder.Instance
                    .WithOutputPort(tuple.output)
                    .WithUserRepository(tuple.userRepository)
                    .WithPartnerRepository(tuple.referentialService)
                    .Build();
            })
            .Throws<ArgumentNullException>();
        }

        [Fact]
        public async Task InvalidPartnerCode_Return_ModelStateError()
        {
            //arrange
            var argCapture = new ArgCapture<Notification>();
            var request = new GetUserDetailQuery("test", "badPartner");
            GetUserDetailUseCaseBuilder builder = GetUserDetailUseCaseBuilder.Instance.WithGetPartnerByCode(default);

            //act
            await builder.Build().Handle(request, default).ConfigureAwait(false);
            var expected = new KeyValuePair<string, string[]>(nameof(request.PartnerCode),
                new string[] { $"partner::{request.PartnerCode} is invalid." });

            //assert
            Check.That(argCapture[0].IsValid).IsFalse();
            Check.That(argCapture[0]).IsInstanceOf<Notification>();
            Check.That(argCapture[0].Errors).ContainsPair(expected.Key, expected.Value);
        }

        [Fact]
        public async Task UserDoesNotExist_Return_NotFound()
        {
            //arrange
            var presenter = new GetUserDetailPresenter();
            var request = new GetUserDetailQuery("test", "bizca");
            GetUserDetailUseCaseBuilder builder = GetUserDetailUseCaseBuilder.Instance
                            .WithGetPartnerByCode(PartnerBuilder.Instance.Build())
                            .WithOutputPort(presenter)
                            .WithGetUserByPartnerIdAndExternalUserId(default);

            //act
            await builder.Build().Handle(request, default).ConfigureAwait(false);

            //assert
            Check.That(presenter.ViewModel).IsInstanceOf<NotFoundResult>();
        }

        [Fact]
        public async Task UserExist_Return_OkObjectResult()
        {
            //arrange
            dynamic user = GetDynamicUser();
            var argCapture = new ArgCapture<GetUserDetail>();
            var request = new GetUserDetailQuery("test", "bizca");
            GetUserDetailUseCaseBuilder builder = GetUserDetailUseCaseBuilder.Instance
                            .WithGetPartnerByCode(PartnerBuilder.Instance.Build())
                            .WithGetUserByPartnerIdAndExternalUserId(user);

            //act
            await builder.Build().Handle(request, default).ConfigureAwait(false);
            builder.WithArgumentOkCapture(argCapture);

            //assert
            GetUserDetail userDto = BuildDto(user);
            Check.That(argCapture[0]).IsInstanceOf<GetUserDetail>();
            Check.That(userDto).HasFieldsWithSameValues(argCapture[0]);
        }

        #region private helpers

        private DynamicDictionary GetDynamicUser()
        {
            dynamic user = new DynamicDictionary();
            user.userCode = Guid.NewGuid().ToString();
            user.externalUserId = "externalUserId";
            user.email = "aa@aa.fr";
            user.emailActive = 1;
            user.emailConfirmed = 1;
            user.phone = "0123456789";
            user.phoneActive = 1;
            user.phoneConfirmed = 0;
            user.whatsapp = "0123456789";
            user.whatsappActive = 0;
            user.whatsappConfirmed = 0;
            user.civilityCode = "Mr";
            user.lastName = "lastName";
            user.firstName = "firstName";
            user.birthCity = "birthCity";
            user.birthDate = DateTime.Today.AddYears(20);
            user.birthCountryCode = "FR";
            user.economicActivityCode = "Craftsman";
            return user;
        }
        private GetUsers BuildDto(dynamic result)
        {
            return GetUsersBuilder.Instance
                .WithUserId(result.userId)
                .WithUserCode(result.userCode.ToString())
                .WithExternalUserId(result.externalUserId)
                .WithEmail(result.email, result.emailActive, result.emailConfirmed)
                .WithPhoneNumber(result.phone, result.phoneActive, result.phoneConfirmed)
                .WithWhatsapp(result.whatsapp, result.whatsappActive, result.whatsappConfirmed)
                .WithCivility(result.civilityCode)
                .WithLastName(result.lastName)
                .WithFirstName(result.firstName)
                .WithBirthCity(result.birthCity)
                .WithBirthDate(result.birthDate.ToString("yyyy-MM-dd"))
                .WithBirthCountry(result.birthCountryCode)
                .WithEconomicActivity(result.economicActivityCode)
                .Build();
        }

        #endregion
    }

    internal sealed class GetUserDetailUseCaseBuilder
    {
        private IGetUserDetailOutput output;
        private IUserRepository userRepository;
        private IReferentialService referentialService;
        private GetUserDetailUseCaseBuilder()
        {
            output = Substitute.For<IGetUserDetailOutput>();
            userRepository = Substitute.For<IUserRepository>();
            referentialService = Substitute.For<IReferentialService>();
        }

        internal static GetUserDetailUseCaseBuilder Instance => new GetUserDetailUseCaseBuilder();
        internal GetUserDetailUseCase Build()
        {
            return new GetUserDetailUseCase(referentialService, userRepository, output);
        }

        internal GetUserDetailUseCaseBuilder WithOutputPort(IGetUserDetailOutput output)
        {
            this.output = output;
            return this;
        }
        internal GetUserDetailUseCaseBuilder WithUserRepository(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            return this;
        }
        internal GetUserDetailUseCaseBuilder WithPartnerRepository(IReferentialService referentialService)
        {
            this.referentialService = referentialService;
            return this;
        }

        internal GetUserDetailUseCaseBuilder WithGetPartnerByCode(Partner partner)
        {
            referentialService.GetPartnerByCodeAsync(Arg.Any<string>()).Returns(partner);
            return this;
        }
        internal GetUserDetailUseCaseBuilder WithGetUserByPartnerIdAndExternalUserId(Dictionary<ResultName, IEnumerable<dynamic>> result)
        {
            userRepository.GetByPartnerIdAndExternalUserIdAsync(Arg.Any<int>(), Arg.Any<string>())
                .Returns(result);
            return this;
        }
        internal GetUserDetailUseCaseBuilder WithArgumentOkCapture(ArgCapture<GetUserDetail> argDto)
        {
            output.Received(1).Ok(argDto.Capture());
            return this;
        }
    }
    internal sealed class GetUserDetailUseCaseDataSetup : TheoryData<(IGetUserDetailOutput, IUserRepository, IPartnerRepository)>
    {
        public GetUserDetailUseCaseDataSetup()
        {
            IGetUserDetailOutput output = Substitute.For<IGetUserDetailOutput>();
            IUserRepository userRepository = Substitute.For<IUserRepository>();
            IPartnerRepository partnerRepository = Substitute.For<IPartnerRepository>();

            Add((default, userRepository, partnerRepository));
            Add((output, default, partnerRepository));
            Add((output, userRepository, default));
        }
    }
    internal sealed class GetUserDetailPresenter : IGetUserDetailOutput
    {
        public IActionResult ViewModel { get; private set; } = new NoContentResult();
        public void Invalid(Notification notification)
        {
            ViewModel = new BadRequestObjectResult(notification.Errors);
        }

        public void NotFound(string message)
        {
            ViewModel = new NotFoundObjectResult(message);
        }

        public void Ok(GetUserDetail userDetail)
        {
            ViewModel = new OkResult();
        }
    }
}