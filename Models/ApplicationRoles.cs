using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace FieldMRIServices.Model
{
    public class ApplicationRoles : IdentityRole
    {
        public ApplicationRoles()
        {
            Permissions.Add("Inventory View");
        }

        [NotMapped]
        public List<string> Permissions { get; set; } = new List<string>();

        public string PermissionsString
        {
            get => string.Join(",", Permissions);
            set => Permissions = value?.Split(',').ToList() ?? new List<string>();
        }
    }
}