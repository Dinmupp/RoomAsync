namespace Domain.User.Specifications
{
    public class GetByNameSpec : Specification<UserEntity>
    {
        private readonly string _username;

        public GetByNameSpec(string username)
        {
            _username = username;
        }

        public override bool IsSatisfiedBy(UserEntity user)
        {
            return user.Username == _username;
        }
    }
}
