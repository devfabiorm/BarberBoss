using AutoMapper;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Users;
using BarberBoss.Domain.Security.Cryptography;
using BarberBoss.Domain.Security.Tokens;
using BarberBoss.Exception;
using BarberBoss.Exception.Messages;
using FluentValidation.Results;

namespace BarberBoss.Application.UseCases.Users.Register;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IReadOnlyUserRepository _readOnlyRepository;
    private readonly IWriteOnlyUserRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPasswordEncrypter _passwordEncrypter;
    private readonly IAccessTokenGenerator _tokenGenerator;

    public RegisterUserUseCase(
        IReadOnlyUserRepository readOnlyRepository,
        IWriteOnlyUserRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IPasswordEncrypter passwordEncrypter,
        IAccessTokenGenerator tokenGenerator)
    {
        _readOnlyRepository = readOnlyRepository;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _passwordEncrypter = passwordEncrypter;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        var user = _mapper.Map<User>(request);
        user.Password = _passwordEncrypter.Encrypt(request.Password);
        user.UserIdentifier = Guid.NewGuid();

        await _repository.Create(user);

        await _unitOfWork.Commit();

        var response =  _mapper.Map<ResponseRegisteredUserJson>(user);
        response.Token = _tokenGenerator.Generate(user);

        return response;
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();

        var result = validator.Validate(request);

        var emailExists = await _readOnlyRepository.HasActiveEmail(request.Email);

        if (emailExists) 
        {
            result.Errors.Add(new ValidationFailure(string.Empty, ResourceErrorMessages.EMAIL_ALREADY_EXISTS));
        }

        if (!result.IsValid)
        {
            var errors = result.Errors
                .Select(error => error.ErrorMessage)
                .ToList();

            throw new ErrorOnValidationException(errors);
        }
    }
}
