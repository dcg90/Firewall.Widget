using System.Threading.Tasks;

namespace FirewallWidget.Manager.Contracts
{
    public interface IManagerService<TCreateUpdateDto, TDetailsDto, TKey>
    {
        Task<ServiceResult<TDetailsDto>> CreateAsync(TCreateUpdateDto dto);

        Task<ServiceResult<TDetailsDto>> ReadAsync(TKey key);

        Task<ServiceResult<TDetailsDto>> UpdateAsync(TCreateUpdateDto dto);

        Task<ServiceResult<TKey>> DeleteAsync(TKey key);
    }
}
