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
        [HttpGet("{username}")]
        public async Task<IActionResult> GetPolicyNumber(string username)
        {
            try
            {
                var policy_number = await _policy.GetPolicyNumberByUserId(username);
                if (policy_number == null)
                {
                    return Ok(null);
                }
                return Ok(policy_number);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("policyDetails/{policynumber}")]
        public async Task<ActionResult> GetPolicyDetailsByPolicyNumber([FromRoute] int policynumber)
        {
            try
            {
                var policyDetails = await _policy.GetPolicyDetailsByPolicyNumber(policynumber);
                if (policyDetails == null)
                {
                    return NotFound(); 
                }

                return Ok(policyDetails); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
     
        [HttpPost]
        [Route("validate")]
        public async Task<IActionResult> ValidateChassisPolicyNumber([FromBody] PolicyDto policyDTO)
        {
            try
            {
                var portal_user = await _policy.ValidatePolicyChassisNumber(policyDTO);

                if (portal_user == "Invalid Chassis Number")
                {
                    return Ok(new { message = "Invalid Chassis" });
                }
                else if (portal_user == "Invalid Policy Number")
                {
                    return Ok(new { message = "Invalid Policy" });
                }
                else if (portal_user == "Invalid Policy & Chassis Number")
                {
                    return Ok(new { message = "Invalid Policy & Chassis" });

                }
                else
                {
                    return Ok(new { message = "Valid"});

                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddUserPolicyDetails([FromBody] PolicyListDto policyListDto)
        {
            try
            {
                var user_policy = await _policy.AddUserPolicyDetails(policyListDto);
                if (user_policy == false)
                {
                    return BadRequest();
                }
                return Ok(user_policy);
            }
            catch (Exception  ex) 
            {
                return BadRequest(ex);
            }
        }


        [HttpDelete("policy/{policynumber}")]
        public async Task<IActionResult> RemovePolicyDetails([FromRoute] int policynumber)
        {
            try
            {
                var policy_details = await _policy.RemovePolicyDetails(policynumber);
                if (policy_details == false)
                {
                    return Ok(false);
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }



        [HttpPost]
        public async Task<IActionResult> AddUser( [FromBody] portaluser portaluser)
        {
            try
            {
                var portal_user = await _policy.Login(portaluser);
                if (portal_user == false)
                {
                    return Ok(false);
                }
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
