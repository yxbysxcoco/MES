using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SQ_Render.Const
{
    public static class Rules
    {
        public static readonly string IsEmail = "email";
        public static readonly string NotNull = "required";
        public static readonly string IsDate = "date";
        public static readonly string IsUrl = "url";
        public static readonly string IsIdCard = "identity";
        public static readonly string NotNullEmail = "required|email";
        public static readonly string NotNullDate = "required|date";
        public static readonly string NotNullUrl = "required|url";
        public static readonly string NotNullIdCard = "required|identity";
    }
}