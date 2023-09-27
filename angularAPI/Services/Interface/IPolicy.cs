using angularAPI.Model.Dto;
using AutoPortal.Model;

namespace angularAPI.Services.Interface
{
    public interface IPolicy
    {
        public  Task<String> ValidatePolicyChassisNumber(PolicyDto policydto);
        public Task<bool> AddUserPolicyDetails(PolicyListDto policyListDto);
        public  Task<List<int>> GetPolicyNumberByUserId(string username);
        public Task<object> GetPolicyDetailsByPolicyNumber(int policynumber);
        public Task<bool> RemovePolicyDetails(int policynumber);
        public Task<bool> Login(portaluser portaluser);
    }
}
