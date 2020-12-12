﻿namespace Bizca.Core.Application.Abstracts.Queries
{
    using System.Threading.Tasks;
    public interface IProcessQuery
    {
        Task ProcessQueryAsync<TResult>(IQuery<TResult> query);
    }
}
