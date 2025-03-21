namespace FinanceSimplified.Models;

public class Wallet
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N").Substring(0, 20); // Shorter ID format without hyphens
    public string UserId { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public bool IsConnected { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastUpdatedAt { get; set; }

    // Navigation properties
    public virtual User User { get; set; } = null!;
    public virtual ICollection<TokenBalance> TokenBalances { get; set; } = new List<TokenBalance>();
}

public class WalletDto
{
    public string Id { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public bool IsConnected { get; set; }
    public List<TokenBalanceDto> Balances { get; set; } = new List<TokenBalanceDto>();
}
