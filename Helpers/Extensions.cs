using System;
using Microsoft.Extensions.DependencyInjection;
using webapi_basic_mysql.Data.AuthRepository;
using webapi_basic_mysql.Data.UserRepository;

namespace webapi_basic_mysql.Helpers
{
    public static class Extensions
    {
        public static void AddRepositories(this IServiceCollection services) {
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        public static int ToUnixTimestamp(this DateTime dateTime)
        {
            return (int)Math.Truncate((dateTime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds);
        }

        public static string EncodeToBase64(this string stringToEncode) {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(stringToEncode);
            string base64String = System.Convert.ToBase64String(toEncodeAsBytes);
            return base64String;
        }

        public static string DecodeFromBase64(this string stringToDecode) {
            byte[] encodedDataAsBytes = System.Convert.FromBase64String(stringToDecode);
            string returnValue = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
            return returnValue;
        }
    }
}