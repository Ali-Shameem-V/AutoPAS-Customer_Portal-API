using angularAPI.Model.Dto;
using angularAPI.Services.Interface;
using AutoPortal.Model;
using Microsoft.AspNetCore.Mvc;

namespace angularAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolicyController : ControllerBase
    {
        private readonly IPolicy _policy;

        public PolicyController(IPolicy policy)
        {
            _policy = policy;
        }
        [HttpGet]
        public async Task<IActionResult> GetPolicyNumber()
        {
            try
            {
                var policy_number = await _policy.getPolicyNumber();
                if (policy_number == null)
                {
                    return Ok(new { message = "Empty" });
                }
                return Ok(policy_number);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("policynumber")]
        public async Task<ActionResult> GetPolicyDetailsByPolicyNumber(int policynumber)
        {
            try
            {
                var policyDetails = await _policy.getPolicyDetailsByPolicyNumber(policynumber);
                if (policyDetails == null)
                {
                    return NotFound(); 
                }

                return Ok(policyDetails); 
            }
            catch 
            {
                return StatusCode(500);
            }
        }

        [HttpGet("user/details")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var portal_user = await _policy.getId();
                if (portal_user != null)
                {
                    return Ok(portal_user);
                }
                return NotFound();
            }
            catch 
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("validate")]
        public async Task<IActionResult> ValidateChassisPolicyNumber(PolicyDto policyDTO)
        {
            try
            {
                var portal_user = await _policy.validatePolicyChassisNumber(policyDTO);

                if (portal_user == false)
                {
                    return Ok(false);
                }

                return Ok(true);
            }
            catch 
            {
                return StatusCode(500);
            }
        }


        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddUserPolicyDetails(PolicyListDto policyListDto)
        {
            try
            {
                var user_policy = await _policy.addUserPolicyDetails(policyListDto);
                if (user_policy == false)
                {
                    return BadRequest();
                }
                return Ok(user_policy);
            }
            catch 
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("policy")]
        public async Task<IActionResult> RemovePolicyDetails(int policynumber)
        {
            try
            {
                var policy_details = await _policy.removePolicyDetails(policynumber);
                if (policy_details == false)
                {
                    return Ok(false);
                }
                return Ok(true);
            }
            catch 
            {
                return StatusCode(500);
            }
        }



        [HttpPost]
        public async Task<IActionResult> AddUser(portaluser portaluser)
        {
            try
            {
                var portal_user = await _policy.login(portaluser);
                if (portal_user == false)
                {
                    return Ok(false);
                }
                return Ok(true);
            }
            catch
            {
                return StatusCode(500);
            }
        }

    }
}
