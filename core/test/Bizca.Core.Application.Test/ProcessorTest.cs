namespace Bizca.Core.Application.Test
{
    using Bizca.Core.Application.Abstracts.Commands;
    using Bizca.Core.Application.Abstracts.Queries;
    using Bizca.Core.Application.Test.Cqrs;
    using Bizca.Core.Support.Test;
    using MediatR;
    using NFluent;
    using NSubstitute;
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public sealed class ProcessorTest
    {
        [Fact]
        public void Process_ArgumentCtorIsNull_ThrowArgumentNullException()
        {
            Check.ThatCode(() => new Processor(default))
            .Throws<ArgumentNullException>();
        }

        [Fact]
        public async Task ProcessCommand_Receive_Send()
        {
            //arrance
            var arg = new ArgCapture<FakeCommand>();
            IMediator _mediator = Substitute.For<IMediator>();
            await _mediator.Send(arg.Capture()).ConfigureAwait(false);

            //act
            await new Processor(_mediator).ProcessCommandAsync(new FakeCommand()).ConfigureAwait(false);

            //assert
            Check.That(arg[0]).IsInstanceOf<FakeCommand>();
            Check.That(arg[0]).InheritsFrom<ICommand<FakeResponse>>();
        }

        [Fact]
        public async Task ProcessCommand_Receive_Send2()
        {
            //arrance
            var arg = new ArgCapture<FakeCommand2>();
            IMediator _mediator = Substitute.For<IMediator>();
            await _mediator.Send(arg.Capture()).ConfigureAwait(false);

            //act
            await new Processor(_mediator).ProcessCommandAsync(new FakeCommand2()).ConfigureAwait(false);

            //assert
            Check.That(arg[0]).IsInstanceOf<FakeCommand2>();
            Check.That(arg[0]).InheritsFrom<ICommand>();
        }

        [Fact]
        public async Task ProcessQuery2_Receive_Send()
        {
            //arrance
            var arg = new ArgCapture<FakeQuery2>();
            IMediator _mediator = Substitute.For<IMediator>();
            await _mediator.Send(arg.Capture()).ConfigureAwait(false);

            //act
            await new Processor(_mediator).ProcessQueryAsync(new FakeQuery2()).ConfigureAwait(false);

            //assert
            Check.That(arg[0]).IsInstanceOf<FakeQuery2>();
            Check.That(arg[0]).InheritsFrom<IQuery>();
        }

        [Fact]
        public async Task ProcessQuery_Receive_Send()
        {
            //arrance
            var arg = new ArgCapture<FakeQuery>();
            IMediator _mediator = Substitute.For<IMediator>();
            await _mediator.Send(arg.Capture()).ConfigureAwait(false);

            //act
            await new Processor(_mediator).ProcessQueryAsync(new FakeQuery()).ConfigureAwait(false);

            //assert
            Check.That(arg[0]).IsInstanceOf<FakeQuery>();
            Check.That(arg[0]).InheritsFrom<IQuery<FakeResponse>>();
        }

        [Fact]
        public async Task ProcessNotification_Receive_Publish()
        {
            //arrance
            var arg = new ArgCapture<FakeNotification>();
            IMediator _mediator = Substitute.For<IMediator>();
            await _mediator.Publish(arg.Capture()).ConfigureAwait(false);

            //act
            await new Processor(_mediator).ProcessNotificationAsync(new FakeNotification()).ConfigureAwait(false);

            //assert
            Check.That(arg[0]).IsInstanceOf<FakeNotification>();
            Check.That(arg[0]).InheritsFrom<INotification>();
        }
    }
}
