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

                using MySqlCommand objCmd = new(@"CALL he_issue_voucher(@duration);", sqlConn);
                objCmd.Parameters.Add("@duration", MySqlDbType.Int32).Value = duration.Value;

                using MySqlDataReader dtReader = objCmd.ExecuteReader();

                if (dtReader.Read())
                {
                    string voucherCode = dtReader["voucher_code"]?.ToString()?.Trim() ?? string.Empty;
                    string voucherDurationStr = dtReader["voucher_duration"]?.ToString()?.Trim() ?? "0";

                    // 🔹 Convert MySQL string minutes → hours/minutes
                    if (int.TryParse(voucherDurationStr, out int minutes))
                    {
                        string formattedDuration;

                        if (minutes < 60)
                        {
                            formattedDuration = $"{minutes} Minute{(minutes == 1 ? "" : "s")}";
                        }
                        else
                        {
                            double hours = minutes / 60.0;
                            if (hours % 1 == 0)
                            {
                                // Whole number of hours
                                formattedDuration = $"{(int)hours} Hour{(hours == 1 ? "" : "s")}";
                            }
                            else
                            {
                                // Fractional hour (e.g. 1.5 hrs)
                                formattedDuration = $"{hours:0.##} Hours";
                            }
                        }

                        return Ok(new
                        {
                            voucher_code = voucherCode,
                            voucher_duration = formattedDuration
                        });
                    }
                    else
                    {
                        return Ok(new
                        {
                            voucher_code = voucherCode,
                            voucher_duration = "Invalid duration"
                        });
                    }
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
                return BadRequest(new { message = $"Unexpected error: {ex.Message}" });
            }
        }




    }
}
