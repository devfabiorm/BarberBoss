using AutoMapper;
using BarberBoss.Communication.Requests;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Users;
using BarberBoss.Domain.Services.LoggedUser;
using BarberBoss.Exception;
using BarberBoss.Exception.Messages;
using FluentValidation.Results;

namespace BarberBoss.Application.UseCases.Users.Update;
public class UpdateUserUseCase : IUpdateUserUseCase
{
    private readonly IReadOnlyUserRepository _readOnlyRepository;
    private readonly ILoggedUser _loggedUser;
    private readonly IUpdateOnlyUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserUseCase(
        IReadOnlyUserRepository readOnlyRepository, 
        ILoggedUser loggedUser, 
        IUpdateOnlyUserRepository repository, 
        IMapper mapper, 
        IUnitOfWork unitOfWork)
    {
        _readOnlyRepository = readOnlyRepository;
        _loggedUser = loggedUser;
        _repository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(RequestUpdateUserJson request)
    {
        await Validate(request);

        var loggedUser = await _loggedUser.Get();

        var user = await _repository.GetById(loggedUser.Id);

        _mapper.Map(request, user);

        _repository.Update(user);

        await _unitOfWork.Commit();
    }

    private async Task Validate(RequestUpdateUserJson request)
    {
        var validator = new UpdateUserValidator();

        var result = validator.Validate(request);

        var emailExists = await _readOnlyRepository.HasActiveEmail(request.Email);

        if (emailExists) 
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_EXISTS));
        }

        if (!result.IsValid) 
        { 
            var errors = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errors);
        }
    }
}
