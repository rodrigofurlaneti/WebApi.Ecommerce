using Domain.Ecommerce.Model;

namespace Data.Ecommerce.Interface
{
    public interface IGeolocationRepository
    {
        Task PostAsync(Place place);
    }
}
