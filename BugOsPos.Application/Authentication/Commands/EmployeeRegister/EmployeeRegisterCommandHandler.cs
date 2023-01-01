using BugOsPos.Application.Common.Interfaces.Authentication;
using BugOsPos.Application.Common.Interfaces.Persistence;
using BugOsPos.Domain.Common.ErrorsCollection;
using BugOsPos.Domain.EmployeeAggregate;
using BugOsPos.Domain.FranchiseAggregate.ValueObjects;
using BugOsPos.Domain.GroupAggregate;
using BugOsPos.Domain.GroupAggregate.ValueObjects;
using ErrorOr;
using MediatR;

namespace BugOsPos.Application.Authentication.Commands.EmployeeRegister;

public sealed class EmployeeRegisterCommandHandler :
    IRequestHandler<EmployeeRegisterCommand, ErrorOr<EmployeeRegisterResult>>
{
    private readonly IPasswordProvider _passwordHasher;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IGroupRepository _groupRepository;

    public EmployeeRegisterCommandHandler(
        IPasswordProvider passwordHasher,
        IEmployeeRepository employeeRepository,
        IGroupRepository groupRepository)
    {
        _passwordHasher = passwordHasher;
        _employeeRepository = employeeRepository;
        _groupRepository = groupRepository;
    }

    public async Task<ErrorOr<EmployeeRegisterResult>> Handle(
        EmployeeRegisterCommand request,
        CancellationToken cancellationToken)
    {
        Group? group = null;
        if (request.GroupId is int groupId)
        {
            group = await _groupRepository.GetGroupById(GroupId.New(groupId));
            if (group is null)
                return Errors.Group.NonExistentId;
        } 
            
        var password = _passwordHasher.GenerateRandomPassword();
        var (hashedPassword, salt) = _passwordHasher.HashPassword(password);

        var roles = new HashSet<EmployeeRole>();
        foreach (var roleName in request.Roles)
        {
            if (Enum.TryParse(roleName, out EmployeeRole role))
                roles.Add(role);
        }

        var employeeId = _employeeRepository.NextIdentity();
        var employee = Employee.New(
            employeeId,
            $"{request.FranchiseId}{employeeId.Value}",
            hashedPassword,
            salt,
            FranchiseId.New(request.FranchiseId),
            group?.Id,
            request.ReadAccess,
            request.Email,
            request.Name,
            request.Surname,
            request.PhoneNumber,
            request.Address,
            request.BankAccount,
            request.Employment,
            roles.ToList(),
            request.DateOfBirth);

        await _employeeRepository.Add(employee);

        return new EmployeeRegisterResult(employee, password);
    }
}
