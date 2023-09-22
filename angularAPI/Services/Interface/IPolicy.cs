using angularAPI.Model.Dto;
using AutoPortal.Model;

namespace angularAPI.Services.Interface
{
    public interface IPolicy
    {
        public  Task<bool> validatePolicyChassisNumber(PolicyDto policydto);
        public Task<bool> addUserPolicyDetails(PolicyListDto policyListDto);
        public Task<IEnumerable<int>> getPolicyNumber();
        public Task<object> getPolicyDetailsByPolicyNumber(int policynumber);
        public Task<bool> removePolicyDetails(int policynumber);
        public Task<bool> login(portaluser portaluser);
        public  Task<portaluser> getId();





    }
}
