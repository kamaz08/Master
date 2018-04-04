using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lista2.Hash;

namespace Lista2
{
    public class HashModel
    {
        [Key]
        public int Id { get; set; }
        public HashEnum HashEnum { get; set; }
        public int Order { get; set; }
        public byte[] Hash { get; set; }
    }
}
