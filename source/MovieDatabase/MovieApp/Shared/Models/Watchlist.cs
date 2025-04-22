using System;
using System.Collections.Generic;

namespace MovieApp.Server.Models;

public partial class Watchlist
{
    public int WatchlistId { get; set; }

    public int UserId { get; set; }

    public DateTime DateCreated { get; set; }

    public virtual ICollection<WatchlistItem> WatchlistItems { get; set; } = new List<WatchlistItem>();
}
