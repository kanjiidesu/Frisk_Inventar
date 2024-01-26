namespace FriskInventarAPI.SessionHandler
// This code has been inspired by teachers project
{
    public static class SessionManager
    {
        static Dictionary<string, Session> sessions = new();

        // here we add the session to the dictionary
        public static void Add(Session session) {
            sessions[session.SessionId] = session;
        }

        public static Session GetSession(string sessionId) {
            if (!sessions.ContainsKey(sessionId))
            {
                return null;
            }
            return sessions[sessionId];
        }

        // we have to check if it exists or not
        public static bool Check(string sessionId) {
            if (!sessions.ContainsKey(sessionId))
            {
                return false;
            }
            if (sessions[sessionId].IsExpired())
            {
                sessions.Remove(sessionId);
                return false;
            }   
            return true;
        }
    }
}