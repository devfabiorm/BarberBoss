﻿using AutoMapper;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Repositories;
using BarberBoss.Domain.Repositories.Users;
using BarberBoss.Domain.Security.Cryptography;
using BarberBoss.Exception;

namespace BarberBoss.Application.UseCases.Users.Register;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IWriteOnlyUserRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IPasswordEncrypter _passwordEncrypter;

    public RegisterUserUseCase(
        IWriteOnlyUserRepository repository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IPasswordEncrypter passwordEncrypter)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _passwordEncrypter = passwordEncrypter;
    }

    public async Task<ResponseUserJson> Execute(RequestRegisterUserJson request)
    {
        Validate(request);

        var user = _mapper.Map<User>(request);
        user.Password = _passwordEncrypter.Encrypt(request.Password);
        user.UserIdentifier = Guid.NewGuid();

        await _repository.Create(user);

        await _unitOfWork.Commit();

        return _mapper.Map<ResponseUserJson>(user);
    }

    private void Validate(RequestRegisterUserJson request)
    {
        var validator = new RegisterUserValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errors = result.Errors
                .Select(error => error.ErrorMessage)
                .ToList();

            throw new ErrorOnValidationException(errors);
        }
    }
}