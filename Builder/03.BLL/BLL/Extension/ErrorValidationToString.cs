using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;


namespace BLL.Extension
{
    public static class ErrorValidationToString
    {
        public static string FluentErrorString(this List<FluentValidation.Results.ValidationFailure> failures)
        {
            var stringBuilder = new StringBuilder();

            foreach (var sb in failures)
            {
                stringBuilder.Append(sb);
                stringBuilder.Append(Environment.NewLine);
            }

            return stringBuilder.ToString();
        }
    }
}
