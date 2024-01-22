using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Kaizen.Authorization.Users;

namespace Kaizen.Enums
{
    public enum BulkProcessType
    {
        SupportType,
        User,
        SessionGroup,
        Session
    }
}
