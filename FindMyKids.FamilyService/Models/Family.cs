using System;
using System.Collections.Generic;
using System.Text;

namespace FindMyKids.FamilyService.Models
{
    public class Family
    {
        public string Name { get; set; }
        public Guid ID { get; set; }
        public ICollection<Member> Members { get; set; }

        public Family()
        {
            this.Members = new List<Member>();
        }

        public Family(string name) : this()
        {
            this.Name = name;
        }

        public Family(string name, Guid id) : this(name)
        {
            this.ID = id;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
