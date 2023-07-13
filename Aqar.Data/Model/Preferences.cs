using System;
using System.Collections.Generic;
using System.Text;

namespace Aqar.Data.Model
{
    public class Preferences
    {
        public string UserId { get; set; }
        public AppUser? User { get; set; }
        public int EstateId { get; set; }
        public Estate Estate { get; set; }
    }
}