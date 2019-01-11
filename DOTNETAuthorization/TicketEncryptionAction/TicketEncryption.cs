using DOTNETAuthorization.RandomsHepler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOTNETAuthorization.TicketEncryptionAction
{
    /// <summary>
    /// 加密key,实际中请求配置文件配置
    /// </summary>
    public class TicketEncryption
    {
        private static readonly string key = "yvDlky7GXGtlPCGr";
        
        public static string GenerateTicket(string guid, string client)
        {
            //随机key
            string randomKey = Randoms.GetRandomString(15);
            var keys = key + randomKey;
        }
    }
}
