namespace JWTMysql.Models
{
    public class ResponseLogin
    {
        public string Token { get; set; }
        public int ExpireDate { get; set; }
    }
}
