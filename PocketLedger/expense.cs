using System.IO;
using System.Text.Json;

namespace PocketLedger;

public class Expenses
{
    public string? name { get; set; }
    public string? category { get; set; }

    public decimal amount { get; set; }
    public int e_id { get; set; }

    public DateTime date { get; set; }

}