using Npgsql;

internal static class HerokuIntegration
{
    private static string ConnectionStringFromUrl(string url)
    {
        // postgres://user:password@host:port/database

        var uri = new Uri(url);
        var builder = new NpgsqlConnectionStringBuilder();

        var userParts = uri.UserInfo.Split(':');

        builder.Username = userParts[0];
        builder.Password = userParts[1];
        builder.Host = uri.Host;
        builder.Port = uri.Port;
        builder.Database = uri.AbsolutePath.TrimStart('/');
        builder.SslMode = SslMode.Prefer;
        builder.TrustServerCertificate = true;

        return builder.ConnectionString;
    }

    public static IWebHostBuilder UseHerokuPort(this IWebHostBuilder builder)
    {
        // Port settings
        var port = Environment.GetEnvironmentVariable("PORT");
        if (!string.IsNullOrWhiteSpace(port))
        {
            Console.WriteLine($".NET 6 + IWebHostBuilder + Heroku's port found: {port} !");
            builder.UseUrls("http://*:" + port);
        }

        return builder;
    }

    public static WebApplication UseHerokuPort(this WebApplication app)
    {
        // Port settings
        var port = Environment.GetEnvironmentVariable("PORT");
        if (!string.IsNullOrWhiteSpace(port))
        {
            Console.WriteLine($".NET 6 + WebApplication + Heroku's port found: {port} !");
            app.Urls.Add("http://*:" + port);
        }

        return app;
    }

    public static IConfigurationBuilder UseHerokuPostgres(this IConfigurationBuilder builder)
    {
        // Database settings
        //var databaseUrl = "postgres://utpeyrcqogggsr:bb9c21e7c9562ce6fbf0a687243f0561dc16693452a8fe38d7b48e6347cc7123@ec2-52-209-111-18.eu-west-1.compute.amazonaws.com:5432/d37rjdsl2pn6en";
        var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
        if (!string.IsNullOrWhiteSpace(databaseUrl))
        {
            Environment.SetEnvironmentVariable("ConnectionStrings__Database", ConnectionStringFromUrl(databaseUrl));
            builder.AddEnvironmentVariables();
        }
        
        return builder;
    }
}
