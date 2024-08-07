﻿using FieldMRIServices.Entities;

namespace FieldMRIServices.Model
{
    public class MemoryModel
    {
        public int Id { get; set; }
        public string Qunity { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string Size { get; set; }
        public string Status { get; set; }
        public string ComputerName { get; set; }
        public string GeneralImage { get; set; }
        public string Location { get; set; }
        public string Tag { get; set; }
        public string BarCodes { get; set; }
        public string Serial { get; set; }

        public List<MemoryPhoto> MemoryPhoto { get; set; }
    }
}
