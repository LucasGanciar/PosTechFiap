﻿using Application.Contracts;
using Application.Mediator.Command;
using Domain.Models;
using Domain.Request;
using Persistence.Contract;
using System.Text.RegularExpressions;

namespace Application.Services;
public class ContactService(IContactRepository _contactRepository) : IContactService
{
    public async Task<bool> Create(CreateContactCommand request)
    {
        if (!IsValid(request.Telephone, request.Name, request.DDD)) return false;
        return await _contactRepository.Create(request.Telephone, request.Name, request.DDD, request.Email);
    }

    public async Task<IEnumerable<Contact>> Get() => await _contactRepository.Get();

    public async Task<IEnumerable<Contact>> Get(int? DDD)
    {
        var result = await _contactRepository.Get();
        return DDD is null ? result : result.Where(x => x.DDD == DDD);
    }

    public async Task<bool> Exists(int DDD, string Telephone) => await _contactRepository.Exists(DDD, Telephone);

    public async Task<bool> Update(UpdateContactRequest request)
    {
        if (!IsValid(request.Telephone, request.Name, request.DDD)) return false;
        return await _contactRepository.Update(request);
    }

    public async Task<bool> Delete(int id) => await _contactRepository.Delete(id);

    private bool IsValid(string Telephone, string Name, int DDD)
    {
        if (request.Telephone.Length != 9) return false;
        if(string.IsNullOrEmpty(request.Name)) return false;
        if(string.IsNullOrEmpty(request.Telephone)) return false;
        if(request.DDD < 1) return false;
        if (Regex.IsMatch(request.Name, @"/^[A-ZÀ-Ÿ][A-zÀ-ÿ']+\s([A-zÀ-ÿ']\s?)*[A-ZÀ-Ÿ][A-zÀ-ÿ']+$/")) return false;
        if (!string.IsNullOrEmpty(request.Email) && !Regex.IsMatch(request.Email, @"^[\w.-]+@[a-zA-Z\d.-]+.[a-zA-Z]{2,}$")) return false;

        return true;
    }
}
