using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CortWebAPI.Models
{
    public class EncryptionResponse
    {
        public string Encrypted { get; set; }
        public string Decrypted { get; set; }
    }
}
