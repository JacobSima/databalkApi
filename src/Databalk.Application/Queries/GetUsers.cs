
using Databalk.Application.Abstractions;
using Databalk.Application.DTO;

namespace Databalk.Application.Queries;

public class GetUsers : IQuery<IEnumerable<UserDto>> { }
