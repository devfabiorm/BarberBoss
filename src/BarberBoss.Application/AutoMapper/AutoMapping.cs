﻿using AutoMapper;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Entities;

namespace BarberBoss.Application.AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();

        EntityToResponse();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestInvoiceJson, Invoice>();
        CreateMap<RequestRegisterBarberShopJson, BarberShop>();
        CreateMap<RequestRegisterUserJson, User>();
        CreateMap<RequestUpdateUserJson, User>();
    }

    private void EntityToResponse()
    {
        CreateMap<Invoice, ResponseInvoiceJson>();
        CreateMap<Invoice, ResponseInvoiceShortJson>();
        CreateMap<BarberShop, ResponseBarberShopJson>();
        CreateMap<BarberShop, ResponseBarberShopShortJson>();
        CreateMap<User, ResponseUserJson>();
    }
}
