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
        //2
        public async Task<String> ValidatePolicyChassisNumber(PolicyDto policydto)
        {
            
                var policy_detail = await _demoContext.Policies.Where(u => u.PolicyNumber == policydto.PolicyNumber).FirstOrDefaultAsync();
                var vehicle_detail = await _demoContext.Vehicles.Where(c => c.ChasisNumber == policydto.ChasisNumber).FirstOrDefaultAsync();

                if (policy_detail != null)
                {
                    //string policyNumberAsString = policydto.PolicyNumber.ToString();

                    if (vehicle_detail != null)
                    {
                        return "Valid";
                    }
                    return "Invalid Chassis Number";
                }
                else if(policy_detail==null && vehicle_detail==null)
                     {
                        return "Invalid Policy & Chassis Number";
                     }
                else
                     {
                     return "Invalid Policy Number";
                     }


        }
        //3
        public async Task<bool> AddUserPolicyDetails(PolicyListDto policyListDto)
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
        //6npass user ID
        /*public async Task<IEnumerable<int>> GetPolicyNumber()
        {
            var user_policy = await _AutoPortalDbContext.userpolicylists.Select(o=>o.PolicyNumber).ToListAsync();
            if(user_policy.Any())
            {
                return user_policy;

            }
            return null;

        }*/
        public async Task<List<int>> GetPolicyNumberByUserId(string username)
        {
            var policy_numbers = await _AutoPortalDbContext.portalusers
                .Where(pol => pol.UserName == username)
                .Join(
                    _AutoPortalDbContext.userpolicylists,
                    pol => pol.UserId,
                    polins => polins.UserId,
                    (pol, polins) => polins.PolicyNumber)
                    .ToListAsync();


            if (policy_numbers.Any())
            {
                return policy_numbers;
            }
            return null;

        }
        //4
        public async Task<object> GetPolicyDetailsByPolicyNumber(int policynumber)
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
        //5
        public async Task<bool> RemovePolicyDetails(int policynumber)
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
        //1
        public async Task<bool> Login(portaluser portaluser)
        {
            var portal_user = await _AutoPortalDbContext.portalusers.Where(u => u.UserName == portaluser.UserName && u.Password == portaluser.Password).FirstOrDefaultAsync();
            if (portal_user != null)
            {
                return true;
            }
            return false;

        }       
    }
}
