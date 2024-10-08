﻿using Domain.Ecommerce.Model;

namespace Data.Ecommerce.Interface
{
    public interface IOrderProductRepository
    {
        Task<IEnumerable<OrderProduct>> GetAsync();
        Task<List<OrderProductResponse>> PostAsync(OrderProductRequest orderProductRequest);
        Task PutAsync(OrderProduct orderProduct);
        Task<OrderProduct?> GetByIdAsync(int id);
        Task DeleteAsync(int id);
    }
}
