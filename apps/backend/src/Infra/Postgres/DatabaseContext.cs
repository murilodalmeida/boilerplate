using FwksLabs.Boilerplate.Infra.Postgres.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FwksLabs.Boilerplate.Infra.Postgres;

public sealed class DatabaseContext : DbContext, IDatabaseContext
{

}