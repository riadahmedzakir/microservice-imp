using FluentValidation;

using identity.API.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity.API.Validators
{
    public class HasValidUserInformation : AbstractValidator<User>
    {
        public HasValidUserInformation()
        {

        }
    }
}
