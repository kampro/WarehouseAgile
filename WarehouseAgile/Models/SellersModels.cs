using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using WarehouseAgile;
using System.ComponentModel.DataAnnotations;

namespace WarehouseAgile.Models
{
    public class SellerModel
    {
        public Guid UserId { get; set; }
        [Required]
        [MinLength(3)]
        public string Username { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
    }

    public class SellersModel
    {
        #region Fields

        private Guid applicationId = new Guid("bbc0b685-447b-4e93-8c8a-9e888cb7a279");
        private List<aspnet_Users> users;

        #endregion

        #region Properties

        public List<aspnet_Users> UsersList
        {
            get { return this.users; }
        }

        #endregion

        #region Methods

        public SellersModel()
        {
            this.FillUsersList();
        }

        private void FillUsersList()
        {
            using (MembershipDBEntities context = new MembershipDBEntities())
            {
                this.users = context.aspnet_Users.OrderBy(x => x.UserName).ToList();
            }
        }

        #endregion
    }

    public class BranchesModel
    {
        #region Fields

        private List<Branch> branches;

        #endregion

        #region Properties

        public List<Branch> BranchesList
        {
            get { return this.branches; }
        }

        #endregion

        #region Methods

        public BranchesModel()
        {
            this.FillBranchesList();
        }

        private void FillBranchesList()
        {
            using (AppDBEntities context = new AppDBEntities())
            {
                this.branches = context.Branches.OrderBy(x => x.Name).ToList();
            }
        }

        #endregion
    }

    public class RolesModel
    {
        #region Fields

        private List<aspnet_Roles> roles;

        #endregion

        #region Properties

        public List<aspnet_Roles> RolesList
        {
            get { return this.roles; }
        }

        #endregion

        #region Methods

        public RolesModel()
        {
            this.FillRolesList();
        }

        private void FillRolesList()
        {
            using (MembershipDBEntities context = new MembershipDBEntities())
            {
                this.roles = context.aspnet_Roles.OrderBy(x => x.RoleName).ToList();
            }
        }

        #endregion
    }
}