using InvoiceXml.Application.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceXml.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(EmailDto email);
    }
}
