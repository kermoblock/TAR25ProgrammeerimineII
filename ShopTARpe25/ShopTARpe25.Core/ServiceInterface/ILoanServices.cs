using ShopTARpe25.Core.Domain;
using ShopTARpe25.Core.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopTARpe25.Core.ServiceInterface
{
    public interface ILoanServices
    {
        Task<Loan> Create(LoanDto dto);
        Task<Loan> BookReturn(LoanDto dto);
    }
}
