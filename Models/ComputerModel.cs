#nullable enable
using FieldMRIServices.Model.Abstractions;

namespace FieldMRIServices.Model
{
    public class ComputerModel : ISortableModel
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

        public ComputerModel()
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

        public string ComputerName { get; set; } = ",none,cell";
        public string InventoryNumber { get; set; }
        public string Location { get; set; } = ",none,cell";
        public string Serial { get; set; } = ",none,cell";
        public string HardDrive { get; set; } = ",none,cell";
        public string Memory { get; set; } = ",none,cell";
        public string Player { get; set; } = ",none,cell";
        public string Network { get; set; } = ",none,cell";
        public string SCSI { get; set; } = ",none,cell";
        public string MultipleCom { get; set; } = ",none,cell";
        public string Video { get; set; } = ",none,cell";
        public string Status { get; set; } = ",none,cell";
        public string Qty { get; set; } = ",none,cell";
        public string BarCodes { get; set; } = ",none,cell";
        public string Pin { get; set; } = ",none,cell";
        public string Fiber { get; set; } = ",none,cell";
        public string IEEE { get; set; } = ",none,cell";
        public string Modisk { get; set; } = ",none,cell";
        public string SASRaid { get; set; } = ",none,cell";
        public string Processors { get; set; } = ",none,cell";
        public string Monitor { get; set; } = ",none,cell";
        public string Windows { get; set; } = ",none,cell";
        public string Software { get; set; } = ",none,cell";
        public string Version { get; set; } = ",none,cell";
        public string Usb { get; set; } = ",none,cell";
        public string Comm { get; set; } = ",none,cell";
        public string FMS { get; set; } = ",none,cell";

        // New properties for selected color and scope
        public string? SelectedTagScope { get; set; } = "none"; // Default value
        public string? SelectedTagcolor { get; set; } = "none"; // Default value

        // Individual color properties
        public string? SelectedComputerNamecolor { get; set; } = "none";
        public string? SelectedLocationcolor { get; set; } = "none";
        public string? SelectedSerialcolor { get; set; } = "none";
        public string? SelectedHardDrivecolor { get; set; } = "none";
        public string? SelectedMemorycolor { get; set; } = "none";
        public string? SelectedPlayercolor { get; set; } = "none";
        public string? SelectedNetworkcolor { get; set; } = "none";
        public string? SelectedSCSIcolor { get; set; } = "none";
        public string? SelectedMultipleComcolor { get; set; } = "none";
        public string? SelectedVideocolor { get; set; } = "none";
        public string? SelectedStatuscolor { get; set; } = "none";
        public string? SelectedQtycolor { get; set; } = "none";
        public string? SelectedBarCodescolor { get; set; } = "none";
        public string? SelectedPincolor { get; set; } = "none";
        public string? SelectedFibercolor { get; set; } = "none";
        public string? SelectedIEEEcolor { get; set; } = "none";
        public string? SelectedModiskcolor { get; set; } = "none";
        public string? SelectedSASRaidcolor { get; set; } = "none";
        public string? SelectedProcessorscolor { get; set; } = "none";
        public string? SelectedMonitorcolor { get; set; } = "none";
        public string? SelectedWindowsrcolor { get; set; } = "none";
        public string? SelectedSoftwarecolor { get; set; } = "none";
        public string? SelectedVersioncolor { get; set; } = "none";
        public string? SelectedUsbcolor { get; set; } = "none";
        public string? SelectedCommcolor { get; set; } = "none";
        public string? SelectedFMScolor { get; set; } = "none";

        // Tag property with backing field and parsing logic
        public string? Tag
        {
            get => _tag;
            set
            {
                _tag = value;
                ParseTag();
            }
        }
        private string? _tag = ",none,dynamic";

        // Method to update the Tag property
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

        // Computed property to get the first part of the Tag
        public string? TagFirstPart
        {
            get
            {
                return Tag?.Split(',')[0];
            }
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
                ParseTag(); // Ensure ParseTag is called after setting Tag
            }
        }

        // Method to parse the Tag and set SelectedTagcolor and SelectedTagScope
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

        // Methods to update individual properties
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

            var computerName = ComputerName;
            var inventorynumber = InventoryNumber;
            var location = Location;
            var serial = Serial;
            var hardDrive = HardDrive;
            var memory = Memory;
            var player = Player;
            var network = Network;
            var scsi = SCSI;
            var multipleCom = MultipleCom;
            var video = Video;
            var status = Status;
            var qty = Qty;
            var barCodes = BarCodes;
            var pin = Pin;
            var fiber = Fiber;
            var ieee = IEEE;
            var modisk = Modisk;
            var sasRaid = SASRaid;
            var processors = Processors;
            var monitor = Monitor;
            var windows = Windows;
            var software = Software;
            var version = Version;
            var usb = Usb;
            var comm = Comm;
            var fms = FMS;

            UpdateProperty(ref computerName, SelectedComputerNamecolor ?? "none", "cell");
            UpdateProperty(ref location, SelectedLocationcolor ?? "none", "cell");
            UpdateProperty(ref serial, SelectedSerialcolor ?? "none", "cell");
            UpdateProperty(ref hardDrive, SelectedHardDrivecolor ?? "none", "cell");
            UpdateProperty(ref memory, SelectedMemorycolor ?? "none", "cell");
            UpdateProperty(ref player, SelectedPlayercolor ?? "none", "cell");
            UpdateProperty(ref network, SelectedNetworkcolor ?? "none", "cell");
            UpdateProperty(ref scsi, SelectedSCSIcolor ?? "none", "cell");
            UpdateProperty(ref multipleCom, SelectedMultipleComcolor ?? "none", "cell");
            UpdateProperty(ref video, SelectedVideocolor ?? "none", "cell");
            UpdateProperty(ref status, SelectedStatuscolor ?? "none", "cell");
            UpdateProperty(ref qty, SelectedQtycolor ?? "none", "cell");
            UpdateProperty(ref barCodes, SelectedBarCodescolor ?? "none", "cell");
            UpdateProperty(ref pin, SelectedPincolor ?? "none", "cell");
            UpdateProperty(ref fiber, SelectedFibercolor ?? "none", "cell");
            UpdateProperty(ref ieee, SelectedIEEEcolor ?? "none", "cell");
            UpdateProperty(ref modisk, SelectedModiskcolor ?? "none", "cell");
            UpdateProperty(ref sasRaid, SelectedSASRaidcolor ?? "none", "cell");
            UpdateProperty(ref processors, SelectedProcessorscolor ?? "none", "cell");
            UpdateProperty(ref monitor, SelectedMonitorcolor ?? "none", "cell");
            UpdateProperty(ref windows, SelectedWindowsrcolor ?? "none", "cell");
            UpdateProperty(ref software, SelectedSoftwarecolor ?? "none", "cell");
            UpdateProperty(ref version, SelectedVersioncolor ?? "none", "cell");
            UpdateProperty(ref usb, SelectedUsbcolor ?? "none", "cell");
            UpdateProperty(ref comm, SelectedCommcolor ?? "none", "cell");
            UpdateProperty(ref comm, SelectedFMScolor ?? "none", "cell");



            ComputerName = computerName;
            InventoryNumber = inventorynumber;
            Location = location;
            Serial = serial;
            HardDrive = hardDrive;
            Memory = memory;
            Player = player;
            Network = network;
            SCSI = scsi;
            MultipleCom = multipleCom;
            Video = video;
            Status = status;
            Qty = qty;
            BarCodes = barCodes;
            Pin = pin;
            Fiber = fiber;
            IEEE = ieee;
            Modisk = modisk;
            SASRaid = sasRaid;
            Processors = processors;
            Monitor = monitor;
            Windows = windows;
            Software = software;
            Version = version;
            Usb = usb;
            Comm = comm;
            FMS = fms;
        }

        // Computed properties for each field
        public string ComputerNameFirstPart
        {
            get => ComputerName?.Split(',')[0];
            set
            {
                if (ComputerName != null)
                {
                    var parts = ComputerName.Split(',');
                    parts[0] = value;
                    ComputerName = string.Join(",", parts);
                }
                else
                {
                    ComputerName = value;
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
        public string ProcessorsFirstPart
        {
            get => Processors?.Split(',')[0];
            set
            {
                if (Processors != null)
                {
                    var parts = Processors.Split(',');
                    parts[0] = value;
                    Processors = string.Join(",", parts);
                }
                else
                {
                    Processors = value;
                }
            }
        }

        public string MonitorFirstPart
        {
            get => Monitor?.Split(',')[0];
            set
            {
                if (Monitor != null)
                {
                    var parts = Monitor.Split(',');
                    parts[0] = value;
                    Monitor = string.Join(",", parts);
                }
                else
                {
                    Monitor = value;
                }
            }
        }

        public string LocationFirstPart
        {
            get => Location?.Split(separator: ',')[0];
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

        public string SerialFirstPart
        {
            get => Serial?.Split(',')[0];
            set
            {
                if (Serial != null)
                {
                    var parts = Serial.Split(',');
                    parts[0] = value;
                    Serial = string.Join(",", parts);
                }
                else
                {
                    Serial = value;
                }
            }
        }

        public string HardDriveFirstPart
        {
            get => HardDrive?.Split(',')[0];
            set
            {
                if (HardDrive != null)
                {
                    var parts = HardDrive.Split(',');
                    parts[0] = value;
                    HardDrive = string.Join(",", parts);
                }
                else
                {
                    HardDrive = value;
                }
            }
        }

        public string MemoryFirstPart
        {
            get => Memory?.Split(',')[0];
            set
            {
                if (Memory != null)
                {
                    var parts = Memory.Split(',');
                    parts[0] = value;
                    Memory = string.Join(",", parts);
                }
                else
                {
                    Memory = value;
                }
            }
        }

        public string PlayerFirstPart
        {
            get => Player?.Split(',')[0];
            set
            {
                if (Player != null)
                {
                    var parts = Player.Split(',');
                    parts[0] = value;
                    Player = string.Join(",", parts);
                }
                else
                {
                    Player = value;
                }
            }
        }

        public string NetworkFirstPart
        {
            get => Network?.Split(',')[0];
            set
            {
                if (Network != null)
                {
                    var parts = Network.Split(',');
                    parts[0] = value;
                    Network = string.Join(",", parts);
                }
                else
                {
                    Network = value;
                }
            }
        }

        public string SCSIFirstPart
        {
            get => SCSI?.Split(',')[0];
            set
            {
                if (SCSI != null)
                {
                    var parts = SCSI.Split(',');
                    parts[0] = value;
                    SCSI = string.Join(",", parts);
                }
                else
                {
                    SCSI = value;
                }
            }
        }

        public string MultipleComFirstPart
        {
            get => MultipleCom?.Split(',')[0];
            set
            {
                if (MultipleCom != null)
                {
                    var parts = MultipleCom.Split(',');
                    parts[0] = value;
                    MultipleCom = string.Join(",", parts);
                }
                else
                {
                    MultipleCom = value;
                }
            }
        }

        public string VideoFirstPart
        {
            get => Video?.Split(',')[0];
            set
            {
                if (Video != null)
                {
                    var parts = Video.Split(',');
                    parts[0] = value;
                    Video = string.Join(",", parts);
                }
                else
                {
                    Video = value;
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

        public string BarCodesFirstPart
        {
            get => BarCodes?.Split(',')[0];
            set
            {
                if (BarCodes != null)
                {
                    var parts = BarCodes.Split(',');
                    parts[0] = value;
                    BarCodes = string.Join(",", parts);
                }
                else
                {
                    BarCodes = value;
                }
            }
        }

        public string FiberFirstPart
        {
            get => Fiber?.Split(',')[0];
            set
            {
                if (Fiber != null)
                {
                    var parts = Fiber.Split(',');
                    parts[0] = value;
                    Fiber = string.Join(",", parts);
                }
                else
                {
                    Fiber = value;
                }
            }
        }

        public string IEEEFirstPart
        {
            get => IEEE?.Split(',')[0];
            set
            {
                if (IEEE != null)
                {
                    var parts = IEEE.Split(',');
                    parts[0] = value;
                    IEEE = string.Join(",", parts);
                }
                else
                {
                    IEEE = value;
                }
            }
        }

        public string ModiskFirstPart
        {
            get => Modisk?.Split(',')[0];
            set
            {
                if (Modisk != null)
                {
                    var parts = Modisk.Split(',');
                    parts[0] = value;
                    Modisk = string.Join(",", parts);
                }
                else
                {
                    Modisk = value;
                }
            }
        }

        public string SASRaidFirstPart
        {
            get => SASRaid?.Split(',')[0];
            set
            {
                if (SASRaid != null)
                {
                    var parts = SASRaid.Split(',');
                    parts[0] = value;
                    SASRaid = string.Join(",", parts);
                }
                else
                {
                    SASRaid = value;
                }
            }
        }

        public string WindowsFirstPart
        {
            get => Windows?.Split(',')[0];
            set
            {
                if (Windows != null)
                {
                    var parts = Windows.Split(',');
                    parts[0] = value;
                    Windows = string.Join(",", parts);
                }
                else
                {
                    Windows = value;
                }
            }
        }

        public string VersionFirstPart
        {
            get => Software?.Split(',')[0];
            set
            {
                if (Software != null)
                {
                    var parts = Software.Split(',');
                    parts[0] = value;
                    Software = string.Join(",", parts);
                }
                else
                {
                    Software = value;
                }
            }
        }
        public string SoftwareFirstPart
        {
            get => Version?.Split(',')[0];
            set
            {
                if (Version != null)
                {
                    var parts = Version.Split(',');
                    parts[0] = value;
                    Version = string.Join(",", parts);
                }
                else
                {
                    Version = value;
                }
            }
        }
        public string UsbFirstPart
        {
            get => Usb?.Split(',')[0];
            set
            {
                if (Usb != null)
                {
                    var parts = Usb.Split(',');
                    parts[0] = value;
                    Usb = string.Join(",", parts);
                }
                else
                {
                    Usb = value;
                }
            }
        }
        public string CommFirstPart
        {
            get => Comm?.Split(',')[0];
            set
            {
                if (Comm != null)
                {
                    var parts = Comm.Split(',');
                    parts[0] = value;
                    Comm = string.Join(",", parts);
                }
                else
                {
                    Comm = value;
                }
            }
        }
        public string FMSFirstPart
        {
            get => FMS?.Split(',')[0];
            set
            {
                if (FMS != null)
                {
                    var parts = FMS.Split(',');
                    parts[0] = value;
                    FMS = string.Join(",", parts);
                }
                else
                {
                    FMS = value;
                }
            }
        }


        // Method to parse all properties and set the corresponding color properties
        public void ParseProperties()
        {
            ParseImages();
            ParseTag();
            var properties = new (string Property, Action<string> SetColor)[]
            {
                (ComputerName, color => SelectedComputerNamecolor = color),
                (Location, color =>SelectedLocationcolor = color),
                (Serial, color => SelectedSerialcolor = color),
                (HardDrive, color => SelectedHardDrivecolor = color),
                (Memory, color => SelectedMemorycolor = color),
                (Player, color => SelectedPlayercolor = color),
                (Network, color => SelectedNetworkcolor = color),
                (SCSI, color => SelectedSCSIcolor = color),
                (MultipleCom, color => SelectedMultipleComcolor = color),
                (Video, color => SelectedVideocolor = color),
                (Status, color => SelectedStatuscolor = color),
                (Qty, color => SelectedQtycolor = color),
                (BarCodes, color => SelectedBarCodescolor = color),
               (Pin, color => SelectedPincolor = color),
              (Fiber, color => SelectedFibercolor = color),
             (IEEE, color => SelectedIEEEcolor = color),
             (Modisk, color => SelectedModiskcolor = color),
             (SASRaid, color => SelectedSASRaidcolor = color),
              (Processors, color => SelectedProcessorscolor = color),
                 (Windows, color => SelectedProcessorscolor = color),
                  (Software, color => SelectedSoftwarecolor = color),
              (Version, color => SelectedVersioncolor = color),
              (Usb, color => SelectedUsbcolor = color),
                 (Comm, color => SelectedCommcolor = color),
                    (FMS, color => SelectedFMScolor = color),

            };

            foreach (var (property, setColor) in properties)
            {
                var parts = property?.Split(',');
                if (parts != null && parts.Length > 1)
                {
                    setColor(parts[1]);
                }
            }
        }
    }
}