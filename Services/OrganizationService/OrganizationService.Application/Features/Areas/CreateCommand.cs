using Application.Core;
using AutoMapper;
using FluentValidation;
using MediatR;
using OrganizationService.Application.Core;
using OrganizationService.Application.Core.DTOs.Areas;
using OrganizationService.Application.Core.Interfaces;
using OrganizationService.Domain.Models;

namespace OrganizationService.Application.Features.Areas;

public class CreateCommand
{
    public class Command : IRequest<Response<AreaRDTO>>
    {
        public AreaCUD areaCud { get; set; }
    }
    
    public class CommandValidator : AbstractValidator<Command>
    {
        public CommandValidator()
        {
            RuleFor(x => x.areaCud).SetValidator(new Validator());
        }
    }
    
    public class Handler : IRequestHandler<Command, Response<AreaRDTO>>
    {
        private readonly IArea _area;
        private readonly IMapper _mapper;

        public Handler(IArea area, IMapper mapper)
        {
            _area = area;
            _mapper = mapper;
        }
        
        public async Task<Response<AreaRDTO>> Handle(Command request, CancellationToken cancellationToken)
        {
            var area = _mapper.Map<Area>(request.areaCud);
            await _area.AddAsync(area);
            return Response<AreaRDTO>.Success(_mapper.Map<AreaRDTO>(area));
        }
    }
}