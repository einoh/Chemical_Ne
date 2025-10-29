using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace Chemical_Ne_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController(IConfiguration configuration) : ControllerBase
    {
        [HttpGet("status")]
        public IActionResult GetAPIstatus()
        {
            return Ok();
        }

        [HttpPost("issue/{duration}")]
        public IActionResult IssueVoucherAsync([FromRoute] int? duration)
        {
            if (!duration.HasValue || duration.Value <= 0)
            {
                return BadRequest(new { message = "Invalid or missing duration parameter." });
            }

            try
            {
                using MySqlConnection sqlConn = new(configuration.GetConnectionString("constr"));
                sqlConn.Open();

                using MySqlCommand objCmd = new(@"SELECT he_issue_voucher(@duration) AS voucher_code;", sqlConn);
                objCmd.Parameters.Add("@duration", MySqlDbType.Int32).Value = duration.Value;

                using MySqlDataReader dtReader = objCmd.ExecuteReader();

                if (dtReader.Read())
                {
                    string voucherCode = dtReader["voucher_code"]?.ToString()?.Trim() ?? string.Empty;
                    string voucherDuration = "weeeeee";
                    return Ok(new { voucher_code = voucherCode, voucher_duration = voucherDuration });
                }
                else
                {
                    return NotFound(new { message = "No voucher generated." });
                }
            }
            catch (MySqlException sqlEx)
            {
                return StatusCode(500, new { message = $"Database error: {sqlEx.Message}" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Unexpected error: {ex.StackTrace}" });
            }
        }



    }
}
