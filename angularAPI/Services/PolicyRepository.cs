using angularAPI.Model;
using angularAPI.Model.Dto;
using angularAPI.Services.Interface;
using AutoPortal.Model;
using Microsoft.EntityFrameworkCore;

namespace angularAPI.Services
{
    public class PolicyRepository : IPolicy
    {
        private readonly DemoContext _demoContext;

        private readonly AutoPortalDbContext _AutoPortalDbContext;

        public PolicyRepository(DemoContext demoContext,AutoPortalDbContext autoPortalDbContext)
        {
            _demoContext = demoContext;
            _AutoPortalDbContext = autoPortalDbContext;
        }
        public async Task<bool> validatePolicyChassisNumber(PolicyDto policydto)
        {
            var policy = await _demoContext.Policies.Where(u => u.PolicyNumber == policydto.PolicyNumber).FirstOrDefaultAsync();

            if (policy != null)
            {
                var chassis_valid = await _demoContext.Vehicles.Where(c=>c.ChasisNumber==policydto.ChasisNumber).FirstOrDefaultAsync();
                string policyNumberAsString = policydto.PolicyNumber.ToString();

                if (chassis_valid != null && policyNumberAsString.Length<10)
                {

                    
                    return true;
                }
                return false;
            }
            return false;

        }
        public async Task<bool> addUserPolicyDetails(PolicyListDto policyListDto)
        {
            var user = await _AutoPortalDbContext.portalusers.FirstOrDefaultAsync(o=>o.UserName==policyListDto.UserName);
            if (user != null)
            {
                var result = new userpolicylist
                {
                    UserPolicyId = Guid.NewGuid(),
                    UserId = user.UserId,
                    PolicyNumber = policyListDto.PolicyNumber
                    
                };
                await _AutoPortalDbContext.userpolicylists.AddAsync(result);
                await _AutoPortalDbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<IEnumerable<int>> getPolicyNumber()
        {
            var user_policy = await _AutoPortalDbContext.userpolicylists.Select(o=>o.PolicyNumber).ToListAsync();
            if(user_policy.Any())
            {
                return user_policy;

            }
            return null;

        }
        public async Task<object> getPolicyDetailsByPolicyNumber(int policynumber )
        {
            var policy_details=await _demoContext.Policies.FirstOrDefaultAsync(o=>o.PolicyNumber==policynumber);
            if(policy_details==null)
            {
                return null;
            }
            else
            {
                var required_policy_details = new PolicyDetailsDto
                {
                    PolicyNumber = policy_details.PolicyNumber,
                    PolicyEffectiveDt = policy_details.PolicyEffectiveDt,
                    PolicyExpirationDt = policy_details.PolicyExpirationDt,
                    Term = policy_details.Term,
                    Status = policy_details.Status,
                    TotalPremium = policy_details.TotalPremium,
                };
                return required_policy_details;
            }
            
        }
        public async Task<bool> removePolicyDetails(int policynumber)
        {
            var policy_details = await _AutoPortalDbContext.userpolicylists.FirstOrDefaultAsync(o=>o.PolicyNumber==policynumber);
            if(policy_details==null)
            {
                return false;
            }
            else
            {
                _AutoPortalDbContext.userpolicylists.Remove(policy_details);
                await _AutoPortalDbContext.SaveChangesAsync();
                return true;
            }
            
        }
        public async Task<bool> login(portaluser portaluser)
        {
            var portal_user = await _AutoPortalDbContext.portalusers.Where(u => u.UserName == portaluser.UserName && u.Password == portaluser.Password).FirstOrDefaultAsync();
            if (portal_user != null)
            {
                return true;
            }
            return false;

        }
        public async Task<portaluser> getId()
        {
            var portal_user = await _AutoPortalDbContext.portalusers.FirstOrDefaultAsync();
            if (portal_user != null)
            {
                return portal_user;
            }
            return null;
        }
       
    }
}
