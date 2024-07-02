using FieldMRIServices.Entities;
using FieldMRIServices.Model;
using Microsoft.EntityFrameworkCore;

namespace FieldMRIServices.Extensions.Services
{
    public static class Conversions
    {
        public static Inventory Convert(this InventoryModel inventoryModel)
        {
            return new Inventory
            {
                InventoryName = inventoryModel.InventoryName,
                Description = inventoryModel.Description,
                Pin = inventoryModel.Pin,
                Serial = inventoryModel.Serial,
                Location = inventoryModel.Location,
                DateRemoved = inventoryModel.DateRemoved,
                RemovedBy = inventoryModel.RemovedBy,
                DateReturned = inventoryModel.DateReturned,
                ReturnedBy = inventoryModel.ReturnedBy,
                Status = inventoryModel.Status,
            };
        }

        public static async Task<List<InventoryModel>> Convert(this IQueryable<Inventory> inventories)
        {
            return await (from invent in inventories
                          select new InventoryModel
                          {
                              Id = invent.Id,
                              InventoryName = invent.InventoryName,
                              Description = invent.Description,
                              Pin = invent.Pin,
                              Serial = invent.Serial,
                              Location = invent.Location,
                              DateRemoved = invent.DateRemoved,
                              RemovedBy = invent.RemovedBy,
                              DateReturned = invent.DateReturned,
                              ReturnedBy = invent.ReturnedBy,
                              Status = invent.Status,

                          }).ToListAsync();
        }



    }
}
