using System;
using System.Security.Cryptography;
using System.Text;
using QhTemplate.ApplicationCore.Exceptions;

namespace QhTemplate.ApplicationService.Utils
{
    public class Encryption
    {
        public static string GetMd5Encryption(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new Exception("内容不能为空");
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(content));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
            }
        }
    }
}