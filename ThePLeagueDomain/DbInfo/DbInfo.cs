namespace ThePLeagueDomain.DbInfo
{
  public class DbInfo : IDbInfo
  {
    public DbInfo(string connectionString)
    {
      ConnectionString = connectionString;
    }
    public string ConnectionString { get; set; }
  }
}