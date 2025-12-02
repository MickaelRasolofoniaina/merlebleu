using MediatR;

namespace Merlebleu.Foundation.CQRS;

public interface ICommand : IRequest<Unit>
{

}

public interface ICommand<out TResponse> : IRequest<TResponse> where TResponse : notnull
{

}
