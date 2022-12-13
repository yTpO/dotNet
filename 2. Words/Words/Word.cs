using System;
using System.Collections.Generic;

namespace Words;

public partial class Word
{
    public int Id { get; set; }

    public string? Value { get; set; }

    public DateTime? CreatedOn { get; set; }

    public bool? IsActive { get; set; }

    public int? LvlId { get; set; }

    public virtual Lvl? Lvl { get; set; }
}
