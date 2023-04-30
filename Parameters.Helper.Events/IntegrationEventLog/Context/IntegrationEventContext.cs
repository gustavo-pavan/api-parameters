using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Parameters.Helper.Events.IntegrationEventLog.Entity;

namespace Parameters.Helper.Events.IntegrationEventLog.Context;

public class IntegrationEventContext : DbContext
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<IntegrationEventContext> _logger;


#pragma warning disable CS8618
    public IntegrationEventContext(DbContextOptions<IntegrationEventContext> options, IConfiguration configuration,
        ILogger<IntegrationEventContext> logger)
        : base(options)
    {
        _configuration = configuration;
        _logger = logger;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        _logger.LogInformation("Init method configure integration event context");

        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer(_configuration["ConnectionStrings_Integration"],
                b => b.MigrationsAssembly("Parameters.Helper.Events"));

        base.OnConfiguring(optionsBuilder);
        _logger.LogInformation("Finish method configure integration event context");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<IntegrationEventLogEntry>(ConfigureIntegrationEventLogEntry);
    }

    private void ConfigureIntegrationEventLogEntry(EntityTypeBuilder<IntegrationEventLogEntry> builder)
    {
        builder.ToTable("IntegrationEventLog");

        builder.HasKey(e => e.EventId);

        builder.Property(x => x.CreationDate).IsRequired();
        builder.Property(x => x.EventTypeName).HasMaxLength(250).IsRequired();
        builder.Property(x => x.Payload).IsRequired();
        builder.Property(x => x.TimesSent).IsRequired();
        builder.Property(x => x.TransactionId).HasMaxLength(200).IsRequired();

        builder.Ignore(x => x.EventTypeShortName);
        builder.Ignore(x => x.IntegrationEvent);

        builder.HasIndex(x => new { x.TransactionId, x.State });
    }

    public DbSet<IntegrationEventLogEntry> IntegrationEventLogs { get; set; }
}