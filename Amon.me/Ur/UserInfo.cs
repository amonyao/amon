namespace Me.Amon.Ur
{
    public class UserInfo : IUserInfo
    {
        public string code { get; private set; }

        public UserInfo()
        {
            code = "U00000001001";
        }
    }
}
