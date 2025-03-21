using FinanceSimplified.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceSimplified.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;
    public DbSet<Wallet> Wallets { get; set; } = null!;
    public DbSet<Token> Tokens { get; set; } = null!;
    public DbSet<TokenBalance> TokenBalances { get; set; } = null!;
    public DbSet<StakingPosition> StakingPositions { get; set; } = null!;
    public DbSet<StakingApyRate> StakingApyRates { get; set; } = null!;
    public DbSet<UserWallet> UserWallets { get; set; } = null!;
    public DbSet<Notification> Notifications { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure relationships
        modelBuilder.Entity<User>()
            .HasOne(u => u.Wallet)
            .WithOne(w => w.User)
            .HasForeignKey<Wallet>(w => w.UserId);

        modelBuilder.Entity<TokenBalance>()
            .HasKey(tb => new { tb.WalletId, tb.TokenId });

        modelBuilder.Entity<TokenBalance>()
            .HasOne(tb => tb.Wallet)
            .WithMany(w => w.TokenBalances)
            .HasForeignKey(tb => tb.WalletId);

        modelBuilder.Entity<TokenBalance>()
            .HasOne(tb => tb.Token)
            .WithMany(t => t.TokenBalances)
            .HasForeignKey(tb => tb.TokenId);

        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.User)
            .WithMany(u => u.Transactions)
            .HasForeignKey(t => t.UserId);

        // Seed data for development
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = "1",
                Name = "Test User",
                Email = "test@example.com",
                PasswordHash = "jGl25bVBBBW96Qi9Te4V37Fnqchz/Eu4qB9vKrRIqRg=", // "password123" hashed with SHA-256
                CreatedAt = DateTime.UtcNow
            }
        );

        modelBuilder.Entity<Token>().HasData(
            new Token
            {
                Id = "1",
                Symbol = "ETH",
                Name = "Ethereum",
                Decimals = 18,
                ContractAddress = "0x0000000000000000000000000000000000000000", // Native ETH
                IsActive = true
            },
            new Token
            {
                Id = "2",
                Symbol = "FIN",
                Name = "Finance Token",
                Decimals = 18,
                ContractAddress = "0x1234567890abcdef1234567890abcdef12345678", // Example address
                IsActive = true
            }
        );

        // Seed data for staking APY rates
        modelBuilder.Entity<StakingApyRate>().HasData(
            new StakingApyRate
            {
                Id = 1,
                TokenSymbol = "ETH",
                DurationDays = 30,
                APY = 5.2m,
                IsActive = true
            },
            new StakingApyRate
            {
                Id = 2,
                TokenSymbol = "ETH",
                DurationDays = 90,
                APY = 6.5m,
                IsActive = true
            },
            new StakingApyRate
            {
                Id = 3,
                TokenSymbol = "FIN",
                DurationDays = 30,
                APY = 12.5m,
                IsActive = true
            },
            new StakingApyRate
            {
                Id = 4,
                TokenSymbol = "FIN",
                DurationDays = 90,
                APY = 15.0m,
                IsActive = true
            }
        );
    }
}
