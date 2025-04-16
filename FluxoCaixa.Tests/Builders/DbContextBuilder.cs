using FluxoCaixa.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FluxoCaixa.Tests.Builders;

public static class DbContextBuilder
{
    public static AppDbContext Create()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }
}
