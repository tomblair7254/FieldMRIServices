#nullable enable
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FieldMRIServices.Model
{
    public class InventoryModel
    {
        public int Id { get; set; }

        private string _images = ",,,none,cell"; // Default value with three empty paths, color, and scope

        public string Images
        {
            get => _images;
            set
            {
                _images = value ?? ",,,none,cell";
                ParseImages();
            }
        }

        public string ImagePaths { get; set; } = ",,"; // Default value with three empty paths
        public string SelectedImageColour { get; set; } = "none";
        public string SelectedImageScope { get; set; } = "cell"; // Ensure this property is public

        private void ParseImages()
        {
            var parts = _images.Split(',');
            ImagePaths = string.Join(",", parts.Take(3)); // Join the first three parts as image paths

            // Log the _images string before parsing
            Console.WriteLine($"Parsing _images: {_images}");

            // Log the parsed ImagePaths
            Console.WriteLine($"Parsed ImagePaths: {ImagePaths}");

            // The color should be at the fourth index
            SelectedImageColour = parts.Length > 3 ? parts[3] : "none";
            // The scope should be at the fifth index
            SelectedImageScope = parts.Length > 4 ? parts[4] : "cell";

            // Log the parsed color and scope
            Console.WriteLine($"Parsed SelectedImageColour: {SelectedImageColour}, SelectedImageScope: {SelectedImageScope}");
        }

        public void UpdateImages()
        {
            // Log the current ImagePaths before updating
            Console.WriteLine($"Current ImagePaths before UpdateImages: {ImagePaths}");

            var paths = ImagePaths.Split(',').Take(3).ToArray();
            while (paths.Length < 3)
            {
                paths = paths.Append("").ToArray();
            }

            _images = $"{string.Join(",", paths)},{SelectedImageColour},{SelectedImageScope}";

            // Log the updated _images string
            Console.WriteLine($"UpdateImages called. New _images value: {_images}");
        }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; } = ",none,cell";

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

        public string SelectedTagScope { get; set; } = "none";
        public string SelectedTagColour { get; set; } = "none";
        public string SelectedNameColour { get; set; } = "none";
        public string SelectedQtyColour { get; set; } = "none";
        public string SelectedPinColour { get; set; } = "none";
        public string SelectedLocationColour { get; set; } = "none";
        public string SelectedBarCodeColour { get; set; } = "none";
        public string SelectedStatusColour { get; set; } = "none";
        public string SelectedMRITSColour { get; set; } = "none";

        public InventoryModel()
        {
            ParseProperties();
        }

        public void UpdateTag()
        {
            var parts = Tag?.Split(',');
            var newTag = parts?[0];
            if (!string.IsNullOrEmpty(SelectedTagColour))
            {
                newTag += $",{SelectedTagColour}";
            }
            if (!string.IsNullOrEmpty(SelectedTagScope))
            {
                newTag += $",{SelectedTagScope}";
            }
            Tag = newTag;
        }

        public void UpdateProperty(ref string property, string selectedColour, string selectedScope)
        {
            var parts = property?.Split(',');
            var newProperty = parts?[0];
            if (!string.IsNullOrEmpty(selectedColour))
            {
                newProperty += $",{selectedColour}";
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
            var qty = Qty;
            var pin = Pin;
            var location = Location;
            var barCode = BarCode;
            var status = Status;
            var mrits = MRITS;

            UpdateProperty(ref name, SelectedNameColour, "cell");
            UpdateProperty(ref qty, SelectedQtyColour, "cell");
            UpdateProperty(ref pin, SelectedPinColour, "cell");
            UpdateProperty(ref location, SelectedLocationColour, "cell");
            UpdateProperty(ref barCode, SelectedBarCodeColour, "cell");
            UpdateProperty(ref status, SelectedStatusColour, "cell");
            UpdateProperty(ref mrits, SelectedMRITSColour, "cell");

            Name = name;
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
            string selectedNameColour, selectedQtyColour, selectedPinColour, selectedLocationColour, selectedBarCodeColour, selectedStatusColour, selectedMRITSColour;

            Name = ParseProperty(Name, "cell", out selectedNameColour);
            Qty = ParseProperty(Qty, "cell", out selectedQtyColour);
            Pin = ParseProperty(Pin, "cell", out selectedPinColour);
            Location = ParseProperty(Location, "cell", out selectedLocationColour);
            BarCode = ParseProperty(BarCode, "cell", out selectedBarCodeColour);
            Status = ParseProperty(Status, "cell", out selectedStatusColour);
            MRITS = ParseProperty(MRITS, "cell", out selectedMRITSColour);

            SelectedNameColour = selectedNameColour;
            SelectedQtyColour = selectedQtyColour;
            SelectedPinColour = selectedPinColour;
            SelectedLocationColour = selectedLocationColour;
            SelectedBarCodeColour = selectedBarCodeColour;
            SelectedStatusColour = selectedStatusColour;
            SelectedMRITSColour = selectedMRITSColour;
        }

        private string ParseProperty(string property, string defaultScope, out string selectedColour)
        {
            var parts = property.Split(',');
            var value = parts.Length > 0 ? parts[0] : "";
            selectedColour = parts.Length > 1 ? parts[1] : "none";
            var scope = parts.Length > 2 ? parts[2] : defaultScope;
            return $"{value},{selectedColour},{scope}";
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
            SelectedTagColour = "none";
            SelectedTagScope = "none";

            if (!string.IsNullOrEmpty(Tag))
            {
                var parts = Tag.Split(',');
                if (parts.Length > 1)
                {
                    SelectedTagColour = parts[1];
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
    }
}