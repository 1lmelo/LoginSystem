﻿using LoginSystem.Enum;

namespace LoginSystem.Models.Interfaces
{
    public interface IUtil
    {
        string EncryptPassword(string password);
        string ShowAlert(Alerts obj, string message);
    }
}
