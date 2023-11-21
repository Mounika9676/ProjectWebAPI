using Microsoft.EntityFrameworkCore;
using ProjectWebAPI.DTO;
using ProjectWebAPI.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectWebAPI.Repo
{
    public class OrganizationRepoImpl : IRepo<OrganizationDTO>
    {
        private readonly oExamDbContext context;

        public OrganizationRepoImpl(oExamDbContext ctx)
        {
            context = ctx;
        }

        public bool Add(OrganizationDTO item)
        {
            Organization organization = new Organization
            {
                OrgName = item.Name
                // Add other properties as needed
            };

            context.Organizations.Add(organization);
            context.SaveChanges();
            return true;
        }

        public List<OrganizationDTO> GetAll()
        {
            var result = context.Organizations
                .Select(o => new OrganizationDTO
                {
                    Id = o.OrgID,
                    Name = o.OrgName
                    // Add other properties as needed
                })
                .ToList();

            return result;
        }

        public OrganizationDTO GetById(int orgId)
        {
            var organization = context.Organizations
                .FirstOrDefault(o => o.OrgID == orgId);

            if (organization == null)
            {
                return null;
            }

            return new OrganizationDTO
            {
                Id = organization.OrgID,
                Name = organization.OrgName
                // Add other properties as needed
            };
        }

        public bool Update(int orgId, OrganizationDTO updatedItem)
        {
            var organization = context.Organizations.Find(orgId);

            if (organization == null)
            {
                return false; // Organization not found
            }

            organization.OrgName = updatedItem.Name;
            // Update other properties as needed

            context.SaveChanges();
            return true;
        }

        public bool Delete(int orgId)
        {
            var organization = context.Organizations.Find(orgId);

            if (organization == null)
            {
                return false; // Organization not found
            }

            context.Organizations.Remove(organization);
            context.SaveChanges();
            return true;
        }
    }
}
