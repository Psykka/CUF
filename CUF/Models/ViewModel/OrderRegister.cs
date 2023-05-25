namespace CUF.Models.ViewModels;

public class OrderRegister
{
    public OrderRegister()
    {
        OrderList = new List<OrderModel>();
    }

    public List<OrderModel> OrderList { get; set; }
}