namespace Shop
{
    public interface IOrderRepository
    {
        Order CreateOrder();
        Order GetById(int id);
        void UpdateOrder(Order order);
    }
}