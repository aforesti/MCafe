using MediatR;

namespace MCafe.Admin.Contracts;

public record GetProducts : IRequest<ProductDto[]>;

public record GetProductById(Guid Id) : IRequest<ProductDto?>;
