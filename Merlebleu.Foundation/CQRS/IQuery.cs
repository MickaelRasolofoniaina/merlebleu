using MediatR;

namespace Merlebleu.Foundation.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse> where TResponse : notnull
{

}
