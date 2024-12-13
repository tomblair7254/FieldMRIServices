#nullable enable
using System.ComponentModel.DataAnnotations;
using FieldMRIServices.SyncfusionSearch;

namespace FieldMRIServices.Model
{
    public class InventoryModel
    {
        public int Id { get; set; }

        private string _images;

        public string SelectedImagecolor { get; set; }
        public string SelectedImageScope { get; set; }
        public List<string> ImagePaths { get; set; } = new List<string>();

        public string Images
        {
            get => _images;
            set
            {
                _images = value;
                ParseImages();
            }
        }

        public InventoryModel()
        {
            _images = string.Empty; // Initialize _images to an empty string
            SelectedImagecolor = "none";
            SelectedImageScope = "cell";
            ParseProperties();
        }

        private void ParseImages()
        {
            if (string.IsNullOrEmpty(_images))
            {
                ImagePaths = new List<string>();
                SelectedImagecolor = "none";
                SelectedImageScope = "cell";
                return;
            }

            var parts = _images.Split(',');
            ImagePaths = parts.Take(3).Where(p => !string.IsNullOrWhiteSpace(p)).ToList();
            SelectedImagecolor = parts.Length > 3 ? parts[3] : "none";
            SelectedImageScope = parts.Length > 4 ? parts[4] : "cell";

            // Log the parsed values
            Console.WriteLine($"Parsed ImagePaths: {string.Join(",", ImagePaths)}");
            Console.WriteLine($"Parsed SelectedImagecolor: {SelectedImagecolor}, SelectedImageScope: {SelectedImageScope}");
        }

        public void UpdateImages()
        {
            // Log the current ImagePaths before updating
            Console.WriteLine($"Current ImagePaths before UpdateImages: {string.Join(",", ImagePaths)}");

            var paths = ImagePaths.ToArray();
            while (paths.Length < 3)
            {
                paths = paths.Append("").ToArray();
            }

            _images = $"{string.Join(",", paths)},{SelectedImagecolor},{SelectedImageScope}";

            // Log the updated _images string
            Console.WriteLine($"UpdateImages called. New _images value: {_images}");
        }

        public void SetImagePaths(string imagePaths)
        {
            var paths = imagePaths.Split(',').Take(3).ToArray();
            while (paths.Length < 3)
            {
                paths = paths.Append("").ToArray();
            }

            _images = $"{string.Join(",", paths)},{SelectedImagecolor},{SelectedImageScope}";

            // Log the updated _images string
            Console.WriteLine($"SetImagePaths called. New _images value: {_images}");
        }

        public void AddImagePath(string imageUrl)
        {
            var paths = ImagePaths;
            if (paths.Count < 3)
            {
                paths.Add(imageUrl);
            }
            else
            {
                paths[2] = imageUrl; // Replace the last image if there are already 3 images
            }

            ImagePaths = paths;
            UpdateImages();
        }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = ",none,cell";

        public string InventoryNumber { get; set; }

        public string Qty { get; set; } = ",none,cell";

        public string Pin { get; set; } = ",none,cell";

        public string Tag
        {
            get => _tag;
            set
            {
                _tag = value;
                ParseTag();
            }
        }
        private string _tag = ",none,dynamic";

        public string Location { get; set; } = ",none,cell";

        public string BarCode { get; set; } = ",none,cell";
        public string Status { get; set; } = ",none,cell";
        public string MRITS { get; set; } = ",none,cell";
        public string SelectedInventoryNumbercolor { get; set; }
        public string SelectedTagScope { get; set; } = "none";
        public string SelectedTagcolor { get; set; } = "none";
        public string SelectedNamecolor { get; set; } = "none";
        public string SelectedQtycolor { get; set; } = "none";
        public string SelectedPincolor { get; set; } = "none";
        public string SelectedLocationcolor { get; set; } = "none";
        public string SelectedBarCodecolor { get; set; } = "none";
        public string SelectedStatuscolor { get; set; } = "none";
        public string SelectedMRITScolor { get; set; } = "none";

        public void UpdateTag()
        {
            var parts = Tag?.Split(',');
            var newTag = parts?[0];
            if (!string.IsNullOrEmpty(SelectedTagcolor))
            {
                newTag += $",{SelectedTagcolor}";
            }
            if (!string.IsNullOrEmpty(SelectedTagScope))
            {
                newTag += $",{SelectedTagScope}";
            }
            Tag = newTag;
        }

        public void UpdateProperty(ref string property, string selectedcolor, string selectedScope)
        {
            var parts = property?.Split(',');
            var newProperty = parts?[0];
            if (!string.IsNullOrEmpty(selectedcolor))
            {
                newProperty += $",{selectedcolor}";
            }
            if (!string.IsNullOrEmpty(selectedScope))
            {
                newProperty += $",{selectedScope}";
            }
            property = newProperty;
        }

        public void UpdateAllProperties()
        {
            UpdateImages();
            UpdateTag();

            var name = Name;
            var inventorynumber = InventoryNumber;
            var qty = Qty;
            var pin = Pin;
            var location = Location;
            var barCode = BarCode;
            var status = Status;
            var mrits = MRITS;

            UpdateProperty(ref name, SelectedNamecolor, "cell");
            UpdateProperty(ref qty, SelectedQtycolor, "cell");
            UpdateProperty(ref pin, SelectedPincolor, "cell");
            UpdateProperty(ref location, SelectedLocationcolor, "cell");
            UpdateProperty(ref barCode, SelectedBarCodecolor, "cell");
            UpdateProperty(ref status, SelectedStatuscolor, "cell");
            UpdateProperty(ref mrits, SelectedMRITScolor, "cell");

            Name = name;
            InventoryNumber = inventorynumber;
            Qty = qty;
            Pin = pin;
            Location = location;
            BarCode = barCode;
            Status = status;
            MRITS = mrits;
        }

        public void ParseProperties()
        {
            ParseImages();
            ParseTag();
            string selectedNamecolor, selectedInventoryNumbercolor, selectedQtycolor, selectedPincolor, selectedLocationcolor, selectedBarCodecolor, selectedStatuscolor, selectedMRITScolor;

            Name = ParseProperty(Name, "cell", out selectedNamecolor);
            //InventoryNumber = ParseProperty(InventoryNumber, "cell", out selectedInventoryNumbercolor);
            Qty = ParseProperty(Qty, "cell", out selectedQtycolor);
            Pin = ParseProperty(Pin, "cell", out selectedPincolor);
            Location = ParseProperty(Location, "cell", out selectedLocationcolor);
            BarCode = ParseProperty(BarCode, "cell", out selectedBarCodecolor);
            Status = ParseProperty(Status, "cell", out selectedStatuscolor);
            MRITS = ParseProperty(MRITS, "cell", out selectedMRITScolor);

            SelectedNamecolor = selectedNamecolor;
            //SelectedInventoryNumbercolor = selectedInventoryNumbercolor;
            SelectedQtycolor = selectedQtycolor;
            SelectedPincolor = selectedPincolor;
            SelectedLocationcolor = selectedLocationcolor;
            SelectedBarCodecolor = selectedBarCodecolor;
            SelectedStatuscolor = selectedStatuscolor;
            SelectedMRITScolor = selectedMRITScolor;
        }

        private string ParseProperty(string property, string defaultScope, out string selectedcolor)
        {
            var parts = property.Split(',');
            var value = parts.Length > 0 ? parts[0] : "";
            selectedcolor = parts.Length > 1 ? parts[1] : "none";
            var scope = parts.Length > 2 ? parts[2] : defaultScope;
            return $"{value},{selectedcolor},{scope}";
        }

        public string TagFirstPart
        {
            get => Tag?.Split(',')[0];
            set
            {
                if (Tag != null)
                {
                    var parts = Tag.Split(',');
                    parts[0] = value;
                    Tag = string.Join(",", parts);
                }
                else
                {
                    Tag = value;
                }
                ParseTag();
            }
        }

        public void ParseTag()
        {
            SelectedTagcolor = "none";
            SelectedTagScope = "none";

            if (!string.IsNullOrEmpty(Tag))
            {
                var parts = Tag.Split(',');
                if (parts.Length > 1)
                {
                    SelectedTagcolor = parts[1];
                }
                if (parts.Length > 2)
                {
                    SelectedTagScope = parts[2];
                }
            }
        }

        public string NameFirstPart
        {
            get => Name?.Split(',')[0];
            set
            {
                if (Name != null)
                {
                    var parts = Name.Split(',');
                    parts[0] = value;
                    Name = string.Join(",", parts);
                }
                else
                {
                    Name = value;
                }
            }
        }

        public string InventoryNumberFirstPart
        {
            get => Name?.Split(',')[0];
            set
            {
                if (Name != null)
                {
                    var parts = Name.Split(',');
                    parts[0] = value;
                    Name = string.Join(",", parts);
                }
                else
                {
                    Name = value;
                }
            }
        }

        public string QtyFirstPart
        {
            get => Qty?.Split(',')[0];
            set
            {
                if (Qty != null)
                {
                    var parts = Qty.Split(',');
                    parts[0] = value;
                    Qty = string.Join(",", parts);
                }
                else
                {
                    Qty = value;
                }
            }
        }

        public string PinFirstPart
        {
            get => Pin?.Split(',')[0];
            set
            {
                if (Pin != null)
                {
                    var parts = Pin.Split(',');
                    parts[0] = value;
                    Pin = string.Join(",", parts);
                }
                else
                {
                    Pin = value;
                }
            }
        }

        public string LocationFirstPart
        {
            get => Location?.Split(',')[0];
            set
            {
                if (Location != null)
                {
                    var parts = Location.Split(',');
                    parts[0] = value;
                    Location = string.Join(",", parts);
                }
                else
                {
                    Location = value;
                }
            }
        }

        public string BarCodeFirstPart
        {
            get => BarCode?.Split(',')[0];
            set
            {
                if (BarCode != null)
                {
                    var parts = BarCode.Split(',');
                    parts[0] = value;
                    BarCode = string.Join(",", parts);
                }
                else
                {
                    BarCode = value;
                }
            }
        }

        public string StatusFirstPart
        {
            get => Status?.Split(',')[0];
            set
            {
                if (Status != null)
                {
                    var parts = Status.Split(',');
                    parts[0] = value;
                    Status = string.Join(",", parts);
                }
                else
                {
                    Status = value;
                }
            }
        }

        public string MRITSFirstPart
        {
            get => MRITS?.Split(',')[0];
            set
            {
                if (MRITS != null)
                {
                    var parts = MRITS.Split(',');
                    parts[0] = value;
                    MRITS = string.Join(",", parts);
                }
                else
                {
                    MRITS = value;
                }
            }
        }
        
        public string SearchKeywords
        {
            get => this.ToSearchKeywords();
            set => _ = value;
        } 
    }
}