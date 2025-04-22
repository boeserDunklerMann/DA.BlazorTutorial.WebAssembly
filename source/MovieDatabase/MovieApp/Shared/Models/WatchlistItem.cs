using System;
using System.Collections.Generic;

namespace MovieApp.Server.Models;

public partial class WatchlistItem
{
    public int WatchlistItemId { get; set; }

    public int WatchlistId { get; set; }

    public int MovieId { get; set; }

    public virtual Movie Movie { get; set; } = null!;

    public virtual Watchlist Watchlist { get; set; } = null!;
}
