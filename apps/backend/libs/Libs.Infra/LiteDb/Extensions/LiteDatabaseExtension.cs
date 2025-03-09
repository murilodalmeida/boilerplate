using FwksLabs.Libs.Core.Extensions;
using LiteDB;

namespace FwksLabs.Libs.Infra.LiteDb.Extensions;

public static class LiteDatabaseExtension
{
    public static ILiteCollection<T> GetNamedCollection<T>(this LiteDatabase database) =>
        database.GetCollection<T>(typeof(T).Name.PluralizeEntity());
}