namespace ECommerceApp3.Interfaces
{
    public interface INetworkConnection//interfaz de coneccion de red
    {
         bool IsConnected { get;}

        void CheckNetworkConnection();
    }
}
