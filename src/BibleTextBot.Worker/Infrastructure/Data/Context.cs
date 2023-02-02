namespace BibleTextBot.Worker.Infrastructure.Data
{
    using ApplicationCore.Interfaces;
    using MongoDB.Driver;

    public class Context : IContext
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<Context> _logger;
        private readonly MongoSettings _mongoSettings;

        public Context(
            IConfiguration configuration,
            ILogger<Context> logger)
        {
            _configuration = configuration;
            _logger = logger;

            _mongoSettings = _configuration
                .GetSection("MongoSettings")
                .Get<MongoSettings>();

            ConnectDb();
        }

        private IMongoDatabase _db { get; set; }
        private MongoClient _mongoClient { get; set; }
        public IClientSession Session { get; set; }

        private void ConnectDb()
        {
            try
            {
                var settingDb = MongoClientSettings
                    .FromUrl(new MongoUrl(_mongoSettings.Connection));

                settingDb.ConnectTimeout = TimeSpan.FromMilliseconds(10000); ;

                if (_mongoSettings.IsSSL)
                {
                    settingDb.SslSettings = new SslSettings
                    {
                        EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12
                    };
                }

                _mongoClient = new MongoClient(settingDb);
                _db = _mongoClient.GetDatabase(_mongoSettings.DatabaseName);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}/n{ex.GetBaseException().Message}/n{ex.StackTrace}/n");
                throw new Exception("Fail connection MongoDb", ex);
            }
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return _db.GetCollection<T>(typeof(T).Name);
        }
    }
}