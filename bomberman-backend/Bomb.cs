using System;
using System.Collections.Generic;

namespace bomberman_backend;

public partial class Bomb
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string YCordinate { get; set; } = null!;

    public string XCordinate { get; set; } = null!;

    public int ExplosionRadius { get; set; }

    public int FuseTime { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
