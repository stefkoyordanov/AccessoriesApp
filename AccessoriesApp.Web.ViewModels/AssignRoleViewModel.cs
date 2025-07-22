using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace AccessoriesApp.Web.ViewModels
{
    public class AssignRoleViewModel
    {
        public string SelectedUserId { get; set; }
        public string SelectedRole { get; set; }

        public List<SelectListItem> Users { get; set; }
        public List<SelectListItem> Roles { get; set; }
    }
}
