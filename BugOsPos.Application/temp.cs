//using BugOsPos.Application.Common.Interfaces.Persistence;
//using BugOsPos.Domain.Common.ErrorsCollection;
//using BugOsPos.Domain.EmployeeAggregate.ValueObjects;
//using BugOsPos.Domain.FranchiseAggregate.ValueObjects;
//using ErrorOr;
//using FluentValidation;
//using MediatR;

//namespace BugOsPos.Application.Employees;

//public sealed record TemplateCommand(int Id, int FranchiseId) : IRequest<ErrorOr<TemplateeResult>>;

//public sealed record TemplateeResult();

//public sealed class TemplateValidator : AbstractValidator<TemplateCommand>
//{
//    public TemplateValidator()
//    {
//        RuleFor(x => x.Id).NotEmpty();
//        RuleFor(x => x.FranchiseId).NotEmpty();
//    }
//}

//public sealed class TemplateCommandHandler : IRequestHandler<TemplateCommand, ErrorOr<TemplateResult>>
//{
//    private readonly IEmployeeRepository _employeeRepository;

//    public TemplateCommandHandler(IEmployeeRepository employeeRepository)
//    {
//        _employeeRepository = employeeRepository;
//    }

//    public async Task<ErrorOr<TemplateResult>> Handle(TemplateCommand request, CancellationToken cancellationToken)
//    {
//        var employee = await _employeeRepository.GetEmployeeById(EmployeeId.New(request.Id));
//        if (employee is null)
//            return Errors.Employee.NotFound;

//        if (employee.FranchiseId != FranchiseId.New(request.FranchiseId))
//            return Errors.Employee.NotFound;

//        await _employeeRepository.Delete(employee.Id);
//        return new DeleteEmployeeResult();
//    }
//}
