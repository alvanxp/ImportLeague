namespace ImportLeague.Api.Models
{
    public class PlayerCount
    {
        public PlayerCount(int total)
        {
            Total = total;
        }
        public int Total { get; set; }
    }
}
