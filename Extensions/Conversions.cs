using FieldMRIServices.Entities;
using FieldMRIServices.Model;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Extensions
{
    public static class Conversions
    {
        public static async Task<List<OtherModel>> Convert(this IQueryable<Other> others)
        {
            return await (from e in others
                          select new OtherModel
                          {
                              Id = e.Id,
                              Qty = e.Qty,
                              Model = e.Model,
                              Type = e.Type,
                              Status = e.Status,
                              ComputerName = e.ComputerName,
                              Tag = e.Tag,
                              GeneralImage = e.GeneralImage,
                              BarCodes = e.BarCodes,
                              Serial = e.Serial,
                          }).ToListAsync();
        }
        public static Other Convert(this OtherModel otherModel)
        {
            return new Other
            {
                Qty = otherModel.Qty,
                Model = otherModel.Model,
                Type = otherModel.Type,
                Status = otherModel.Status,
                ComputerName = otherModel.ComputerName,
                Tag = otherModel.Tag,
                GeneralImage = otherModel.GeneralImage,
                BarCodes = otherModel.BarCodes,
                Serial = otherModel.Serial
            };
        }



    }
}
