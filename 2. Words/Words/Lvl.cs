using System;
using System.Collections.Generic;

namespace Words;

public partial class Lvl
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public short? Value { get; set; }

    public short? Time { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<Word> Words { get; } = new List<Word>();
}
