﻿using System;
using System.Text;

namespace JPush.Api.Util
{
    class Base64
    {
        public static String getBase64Encode(String str)
        {
            byte[] bytes = Encoding.Default.GetBytes(str);
            //
            return Convert.ToBase64String(bytes);
        }
    }
}
