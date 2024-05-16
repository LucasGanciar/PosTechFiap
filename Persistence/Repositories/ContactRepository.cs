﻿using Dapper;
using Domain.Models;
using Domain.Request;
using Infrastructure.Context;
using Persistence.Contract;
using Persistence.Sql;

namespace Persistence.Repositories;
public class ContactRepository(IDbContext db) : IContactRepository
{
    public async Task<bool> Create(string Telephone, string Name, int DDD, string Email)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Name", Name);
        parameters.Add("@DDD", DDD);
        parameters.Add("@Telephone", Telephone);
        parameters.Add("@Email", Email);

        return await db.Context.ExecuteScalarAsync<bool>(ContactSql.Insert, parameters);
    }

    public async Task<IEnumerable<Contact>> Get() => await db.Context.QueryAsync<Contact>(ContactSql.Select);

    public async Task<bool> Exists(int DDD, string Telephone)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@DDD", DDD);
        parameters.Add("@Telephone", Telephone);

        return await db.Context.ExecuteScalarAsync<bool>(ContactSql.Exists, parameters);
    }

    public async Task<bool> Update(UpdateContactRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Id", request.Id);
        parameters.Add("@Name", request.Name);
        parameters.Add("@DDD", request.DDD);
        parameters.Add("@Telephone", request.Telephone);
        parameters.Add("@Email", request.Email);

        return await db.Context.ExecuteScalarAsync<int>(ContactSql.Update, parameters) > 0;
    }

    public async Task<bool> Delete(int id)
    {
        return await db.Context.ExecuteScalarAsync<bool>(ContactSql.Delete, new { id });
    }
}