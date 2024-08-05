using Domain.Ecommerce.Model;

namespace WebApi.Ecommerce.Data.Interface
{
    public interface IGeolocationRepository
    {
        Task PostAsync(Place place);
    }
}
