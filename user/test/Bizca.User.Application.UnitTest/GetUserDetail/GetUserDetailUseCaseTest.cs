namespace Bizca.User.Application.UnitTest.GetUserDetail
{
    using Bizca.Core.Application.Abstracts;
    using Bizca.Core.Domain.Partner;
    using Bizca.Core.Support.Test;
    using Bizca.Core.Support.Test.Builders;
    using Bizca.User.Application.UseCases.GetUserDetail;
    using Bizca.User.Domain.Agregates.Users;
    using Bizca.User.Domain.Agregates.Users.Repositories;
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
        public void OneOfArgumentIsNull_ThrowArgumentNullException((IOutputPort output,
            IUserRepository userRepository,
            IPartnerRepository partnerRepository) tuple)
        {
            Check.ThatCode(() =>
            {
                GetUserDetailUseCaseBuilder.Instance
                    .WithOutputPort(tuple.output)
                    .WithUserRepository(tuple.userRepository)
                    .WithPartnerRepository(tuple.partnerRepository)
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
            builder.WithArgumentOutputCapture(argCapture);
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
            var argCapture = new ArgCapture<GetUserDetailDto>();
            var request = new GetUserDetailQuery("test", "bizca");
            GetUserDetailUseCaseBuilder builder = GetUserDetailUseCaseBuilder.Instance
                            .WithGetPartnerByCode(PartnerBuilder.Instance.Build())
                            .WithGetUserByPartnerIdAndExternalUserId(user);

            //act
            await builder.Build().Handle(request, default).ConfigureAwait(false);
            builder.WithArgumentOkCapture(argCapture);

            //assert
            GetUserDetailDto userDto = BuildDto(user);
            Check.That(argCapture[0]).IsInstanceOf<GetUserDetailDto>();
            Check.That(userDto).HasFieldsWithSameValues(argCapture[0]);
        }

        #region private helpers

        private DynamicDictionary GetDynamicUser()
        {
            dynamic user = new DynamicDictionary();
            user.userCode = Guid.NewGuid().ToString();
            user.externalUserId = "externalUserId";
            user.email = "aa@aa.fr";
            user.phoneNumber = "0123456789";
            user.civilityCode = "Mr";
            user.lastName = "lastName";
            user.firstName = "firstName";
            user.birthCity = "birthCity";
            user.birthDate = DateTime.Today.AddYears(20);
            user.birthCountryCode = "FR";
            user.economicActivityCode = "Craftsman";
            return user;
        }
        private GetUserDetailDto BuildDto(dynamic result)
        {
            return GetUserDetailBuilder.Instance
                .WithUserCode(result.userCode)
                .WithExternalUserId(result.externalUserId)
                .WithEmail(result.email)
                .WithPhoneNumber(result.phoneNumber)
                .WithCivility(result.civilityCode)
                .WithLastName(result.lastName)
                .WithFirstName(result.firstName)
                .WithChannels((NotificationChanels)1)
                .WithBirthCity(result.birthCity)
                .WithBirthDate(result.birthDate.ToString("yyyy/MM/dd"))
                .WithBirthCountry(result.birthCountryCode)
                .WithEconomicActivity(result.economicActivityCode)
                .Build();
        }

        #endregion
    }

    internal sealed class GetUserDetailUseCaseBuilder
    {
        private IOutputPort output;
        private IUserRepository userRepository;
        private IPartnerRepository partnerRepository;
        private GetUserDetailUseCaseBuilder()
        {
            output = Substitute.For<IOutputPort>();
            userRepository = Substitute.For<IUserRepository>();
            partnerRepository = Substitute.For<IPartnerRepository>();
        }

        internal static GetUserDetailUseCaseBuilder Instance => new GetUserDetailUseCaseBuilder();
        internal GetUserDetailUseCase Build()
        {
            return new GetUserDetailUseCase(output, partnerRepository, userRepository);
        }

        internal GetUserDetailUseCaseBuilder WithOutputPort(IOutputPort output)
        {
            this.output = output;
            return this;
        }
        internal GetUserDetailUseCaseBuilder WithUserRepository(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            return this;
        }
        internal GetUserDetailUseCaseBuilder WithPartnerRepository(IPartnerRepository partnerRepository)
        {
            this.partnerRepository = partnerRepository;
            return this;
        }

        internal GetUserDetailUseCaseBuilder WithGetPartnerByCode(Partner partner)
        {
            partnerRepository.GetByCodeAsync(Arg.Any<string>())
                .Returns(partner);
            return this;
        }
        internal GetUserDetailUseCaseBuilder WithGetUserByPartnerIdAndExternalUserId(DynamicDictionary user)
        {
            userRepository.GetById(Arg.Any<int>(), Arg.Any<string>())
                .Returns(user);
            return this;
        }
        internal GetUserDetailUseCaseBuilder WithArgumentOutputCapture(ArgCapture<Notification> argNotification)
        {
            output.Received(1).Invalid(argNotification.Capture());
            return this;
        }
        internal GetUserDetailUseCaseBuilder WithArgumentOkCapture(ArgCapture<GetUserDetailDto> argDto)
        {
            output.Received(1).Ok(argDto.Capture());
            return this;
        }
    }
    internal sealed class GetUserDetailUseCaseDataSetup : TheoryData<(IOutputPort, IUserRepository, IPartnerRepository)>
    {
        public GetUserDetailUseCaseDataSetup()
        {
            IOutputPort output = Substitute.For<IOutputPort>();
            IUserRepository userRepository = Substitute.For<IUserRepository>();
            IPartnerRepository partnerRepository = Substitute.For<IPartnerRepository>();

            Add((default, userRepository, partnerRepository));
            Add((output, default, partnerRepository));
            Add((output, userRepository, default));
        }
    }
    internal sealed class GetUserDetailPresenter : IOutputPort
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
            ViewModel = new OkResult();
        }
    }
}
