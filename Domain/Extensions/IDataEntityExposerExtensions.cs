namespace Domain.Extensions
{
    public static class IDataEntityExposerExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TIDomainDataEntity"></typeparam>
        /// <param name="domainEntity"></param>
        /// <returns></returns>
        public static IDataEntityExposer<TIDomainDataEntity> ExposeDataEntity<TIDomainDataEntity>(this IDataEntityExposer domainEntity)
        {
            return (IDataEntityExposer<TIDomainDataEntity>)domainEntity;
        }

    }
}
