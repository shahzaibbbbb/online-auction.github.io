using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using eBidWEBSITE.Models;

namespace eBidWEBSITE.Models
{
    public class MyRoleProvider:RoleProvider
    {
        ebidEntities db = new ebidEntities();
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            var obj = db.tblUsers.FirstOrDefault(x => x.Email == username);
            if(obj!=null)
            {

                string role = obj.RoleType.ToString();
                string[] roleAry = { role };

                return roleAry;
            }
            else
            {
                throw new NotImplementedException();
            }
            
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}