namespace EBusinessWeb.ResultMessages
{
    public static class Messages
    {
        public static class Blog
        {
            public static string Add(string message)
            {
                return $"{message} uğurla yükləndi!";
            }

            public static string Update(string message)
            {
                return $"{message} uğurla yeniləndi!";
            }

            public static string Remove(string message)
            {
                return $"{message} uğurla Silindi!";
            }

        }
    }
}
