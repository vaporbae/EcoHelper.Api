﻿namespace EcoHelper.Api.Models
{
    public class ValidationError
    {
        public ValidationError(string field, string message)
        {
            Field = field != string.Empty ? field : null;
            Message = message;
        }

        public string Field { get; }

        public string Message { get; }
    }
}
