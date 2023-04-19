namespace DattingAppUpdate.Entites
{
    public class Likes
    {
        public User LikerUser { get; set; }
        public int LikerUserId { get; set; }
        public User LikedUser { get; set; }
        public int LikedUserId { get; set; }
    }
}
