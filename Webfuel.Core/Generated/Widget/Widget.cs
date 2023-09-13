using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webfuel
{
    public partial class Widget
    {
        public Guid Id  { get; set; } = Guid.Empty;
        public string Name  { get; set; } = String.Empty;
        public int Age  { get; set; } = 0;
        public Widget Copy()
        {
            var entity = new Widget();
            entity.Id = Id;
            entity.Name = Name;
            entity.Age = Age;
            return entity;
        }
    }
}

