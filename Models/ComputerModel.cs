#nullable enable
using System.ComponentModel.DataAnnotations;

namespace FieldMRIServices.Model
{
    public class ComputerModel
    {
        public int Id { get; set; }
        public string Images { get; set; }


        public string ComputerName { get; set; } = ",none,cell";
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
        public string? Pin { get; set; } = ",none,cell";
        public string? Fiber { get; set; } = ",none,cell";
        public string? IEEE { get; set; } = ",none,cell";
        public string? Modisk { get; set; } = ",none,cell";
        public string? SASRaid { get; set; } = ",none,cell";

        // New properties for selected colour and scope
        public string? SelectedTagScope { get; set; } = "none"; // Default value
        public string? SelectedTagColour { get; set; } = "none"; // Default value

        // Individual color properties
        public string? SelectedComputerNameColour { get; set; } = "none";
        public string? SelectedLocationColour { get; set; } = "none";
        public string? SelectedSerialColour { get; set; } = "none";
        public string? SelectedHardDriveColour { get; set; } = "none";
        public string? SelectedMemoryColour { get; set; } = "none";
        public string? SelectedPlayerColour { get; set; } = "none";
        public string? SelectedNetworkColour { get; set; } = "none";
        public string? SelectedSCSIColour { get; set; } = "none";
        public string? SelectedMultipleComColour { get; set; } = "none";
        public string? SelectedVideoColour { get; set; } = "none";
        public string? SelectedStatusColour { get; set; } = "none";
        public string? SelectedQtyColour { get; set; } = "none";
        public string? SelectedBarCodesColour { get; set; } = "none";
        public string? SelectedPinColour { get; set; } = "none";
        public string? SelectedFiberColour { get; set; } = "none";
        public string? SelectedIEEEColour { get; set; } = "none";
        public string? SelectedModiskColour { get; set; } = "none";
        public string? SelectedSASRaidColour { get; set; } = "none";

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

        // Method to parse the Tag and set SelectedTagColour and SelectedTagScope
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

        // Methods to update individual properties
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
            var computerName = ComputerName;
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

            UpdateProperty(ref computerName, SelectedComputerNameColour ?? "none", "cell");
            UpdateProperty(ref location, SelectedLocationColour ?? "none", "cell");
            UpdateProperty(ref serial, SelectedSerialColour ?? "none", "cell");
            UpdateProperty(ref hardDrive, SelectedHardDriveColour ?? "none", "cell");
            UpdateProperty(ref memory, SelectedMemoryColour ?? "none", "cell");
            UpdateProperty(ref player, SelectedPlayerColour ?? "none", "cell");
            UpdateProperty(ref network, SelectedNetworkColour ?? "none", "cell");
            UpdateProperty(ref scsi, SelectedSCSIColour ?? "none", "cell");
            UpdateProperty(ref multipleCom, SelectedMultipleComColour ?? "none", "cell");
            UpdateProperty(ref video, SelectedVideoColour ?? "none", "cell");
            UpdateProperty(ref status, SelectedStatusColour ?? "none", "cell");
            UpdateProperty(ref qty, SelectedQtyColour ?? "none", "cell");
            UpdateProperty(ref barCodes, SelectedBarCodesColour ?? "none", "cell");
            UpdateProperty(ref pin, SelectedPinColour ?? "none", "cell");
            UpdateProperty(ref fiber, SelectedFiberColour ?? "none", "cell");
            UpdateProperty(ref ieee, SelectedIEEEColour ?? "none", "cell");
            UpdateProperty(ref modisk, SelectedModiskColour ?? "none", "cell");
            UpdateProperty(ref sasRaid, SelectedSASRaidColour ?? "none", "cell");

            ComputerName = computerName;
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

        // Method to parse all properties and set the corresponding color properties
        public void ParseProperties()
        {
            var properties = new (string Property, Action<string> SetColor)[]
            {
                (ComputerName, color => SelectedComputerNameColour = color),
                (Location, color => SelectedLocationColour = color),
                (Serial, color => SelectedSerialColour = color),
                (HardDrive, color => SelectedHardDriveColour = color),
                (Memory, color => SelectedMemoryColour = color),
                (Player, color => SelectedPlayerColour = color),
                (Network, color => SelectedNetworkColour = color),
                (SCSI, color => SelectedSCSIColour = color),
                (MultipleCom, color => SelectedMultipleComColour = color),
                (Video, color => SelectedVideoColour = color),
                (Status, color => SelectedStatusColour = color),
                (Qty, color => SelectedQtyColour = color),
                (BarCodes, color => SelectedBarCodesColour = color),
                (Pin, color => SelectedPinColour = color),
                (Fiber, color => SelectedFiberColour = color),
                (IEEE, color => SelectedIEEEColour = color),
                (Modisk, color => SelectedModiskColour = color),
                (SASRaid, color => SelectedSASRaidColour = color)
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