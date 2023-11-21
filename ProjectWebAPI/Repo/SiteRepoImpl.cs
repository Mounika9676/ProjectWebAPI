using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectWebAPI.DTO;
using ProjectWebAPI.Entity;

namespace ProjectWebAPI.Repo
{

    public class SiteRepoImpl : IRepo<SiteDTO>
    {
        private readonly oExamDbContext context;

        public SiteRepoImpl(oExamDbContext ctx)
        {
            context = ctx;
        }

        public bool Add(SiteDTO item)
        {
            Site site = new Site
            {
                OrgID = item.OrgID,
                SiteName = item.SiteName
            };

            context.Sites.Add(site);
            context.SaveChanges();
            return true;
        }

        public List<SiteDTO> GetAll()
        {
            var result = context.Sites
                .Select(s => new SiteDTO
                {
                    SiteID = s.SiteID,
                    OrgID = s.OrgID,
                    SiteName = s.SiteName
                })
                .ToList();

            return result;
        }

        public SiteDTO GetById(int siteId)
        {
            var site = context.Sites
                .Include(s => s.Organization) // Include related Organization data if needed
                .FirstOrDefault(s => s.SiteID == siteId);

            if (site == null)
            {
                return null;
            }

            return new SiteDTO
            {
                SiteID = site.SiteID,
                OrgID = site.OrgID,
                SiteName = site.SiteName
            };
        }
        public Site GetByName(string name)
        {
            return context.Sites.FirstOrDefault(s => s.SiteName == name);
        }

        public bool Update(int siteId, SiteDTO updatedItem)
        {
            var site = context.Sites.Find(siteId);

            if (site == null)
            {
                return false; // Site not found
            }

            site.OrgID = updatedItem.OrgID;
            site.SiteName = updatedItem.SiteName;

            context.SaveChanges();
            return true;
        }

        public bool Delete(int siteId)
        {
            var site = context.Sites.Find(siteId);

            if (site == null)
            {
                return false; // Site not found
            }

            context.Sites.Remove(site);
            context.SaveChanges();
            return true;
        }
    }
}
