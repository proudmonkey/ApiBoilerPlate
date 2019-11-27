using System;

namespace ApiBoilerPlate.Infrastructure.Helpers
{
    public static class PropertyValidation
    {
        public static bool IsValidDateTime(DateTime date)
        {
            if (date == default(DateTime))
                return false;
            return true;
        }
    }
}
