using System.Threading.Tasks;

namespace OrderProcessor
{
    public interface IOrderLogic
    {
        Task WriteOrder(Order order);
    }
}