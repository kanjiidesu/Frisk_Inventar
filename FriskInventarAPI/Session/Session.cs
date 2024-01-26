namespace FriskInventarAPI.SessionHandler
// This code has been inspired by teachers project
{
    public class Session
    {
        public int UserId {get;set;}
        public string SessionId {get;set;}
        public DateTime LastUpdated {get;set;}
        
        TimeSpan aliveTime = TimeSpan.FromMinutes(30);

        public Session(int userId): this(DateTime.Now) 
        {
            UserId = userId;
        }
        public Session(DateTime lastUpdated) 
        {
            SessionId = CreateSessionId();
            LastUpdated = lastUpdated;
        }

        public static string CreateSessionId()
        {
            const int sessionIdLength = 32;
            const string chars = "123456789qwertyuiopasdfghjklzxcvbnm";

            Random randomGenerator = new Random();
            string sessionId = new string(Enumerable.Repeat(chars, sessionIdLength)
                                 .Select(s => s[randomGenerator.Next(s.Length)]).ToArray());

            return sessionId;
        }

        public bool IsExpired()
        {
            return DateTime.Now - LastUpdated > aliveTime;
        }
    }
}