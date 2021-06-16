using ADO.NET_Class_Library_Ex;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace ADO.NET_Example.Controllers
{
    public class ColorAccess
    {
        SqlDatabaseUtility utility = new SqlDatabaseUtility();
        string constr = ConfigurationManager.ConnectionStrings["TestDBConnectionString"].ConnectionString;
        DataTable table;

        public IEnumerable<Color> Get()
        {
            DataSet ds = utility.ExecuteQuery("TestDBConnectionString", "sp_Color_Get", new Dictionary<string, System.Data.SqlClient.SqlParameter>());
            table = ds.Tables[0];

            return table.AsEnumerable().Select(row => new Color((int)row["colorId"])
            {
                nm = row["name"].ToString(),
                hex = row["hex"].ToString()
            });
        }

        public bool TryGet(int id, out Color color)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select * From Color Where colorId = @id", con);
                SqlParameter idParameter = new SqlParameter("@id", id);
                idParameter.SqlDbType = SqlDbType.Int;
                idParameter.Size = 0;
                idParameter.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(idParameter);

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.HasRows)
                    {
                        rdr.Read();
                        color = new Color(id);
                        color.nm = (string)rdr.GetValue(0);
                        color.hex = rdr.GetString(1);
                        return true;
                    }
                    else
                        color = null;
                        return false;
                }
            }
        }

        public Color Add(Color color)
        {
            int id = utility.ExecuteCommand("TestDBConnectionString", "sp_Color_AddWithIdentity", new Dictionary<string, SqlParameter>
            {
                ["name"] = new SqlParameter("@name", color.nm),
                ["hex"] = new SqlParameter("@hex", color.hex),
                ["colorId"] = new SqlParameter("@Identity", SqlDbType.Int, 0, "colorId") { Direction = ParameterDirection.Output}
            });

            Color IDedColor = new Color(id) { nm = color.nm, hex = color.hex };
            return IDedColor;
        }

        public bool Update(Color color, int id)
        {
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Update Color Set [name] = @nm, hex = @hex Where colorId = @id", con);
                SqlParameter nmParam = new SqlParameter("@nm", color.nm) { SqlDbType = SqlDbType.NVarChar, Size = 50 };
                SqlParameter hexParam = new SqlParameter("@hex", color.hex) { SqlDbType = SqlDbType.NChar, Size = 6 };
                SqlParameter idParameter = new SqlParameter("@id", id);
                idParameter.SqlDbType = SqlDbType.Int;
                idParameter.Size = 0;
                idParameter.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(nmParam);
                cmd.Parameters.Add(hexParam);
                cmd.Parameters.Add(idParameter);

                int rAff = cmd.ExecuteNonQuery();
                if (rAff != 0)
                    return true;
                else
                    return false;
            }
        }

        public bool Delete(int id)
        {
            using(SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Delete From Color Where colorId = @id", con);
                SqlParameter idParam = new SqlParameter("@id", id) { SqlDbType = SqlDbType.Int, Size = 0 };
                cmd.Parameters.Add(idParam);

                int rAff = cmd.ExecuteNonQuery();
                if (rAff != 0)
                    return true;
                else
                    return false;
            }
        }
    }

    [RoutePrefix("api/Color")]
    public class ColorController : ApiController
    {
        ColorAccess access = new ColorAccess();

        /// <summary>
        /// Booty Rockin Everywhere
        /// </summary>
        [HttpGet]
        [Route("IActionGet")]
        [ApiExplorerSettings(IgnoreApi=true)]
        public IHttpActionResult IActionGet()
        {
            return new TextResult("Hello", Request);
        }

        [HttpGet]
        [Route("")]
        // GET: api/Color
        public IEnumerable<Color> Get()
        {
            var r = access.Get();
            return r;
        }

        // GET: api/Color/5
        public IHttpActionResult Get(int id)
        {
            Color color;
            if (!access.TryGet(id, out color))
                return NotFound(); //throw new HttpResponseException(HttpStatusCode.NotFound);
            return Ok(color);
        }

        // POST: api/Color
        public HttpResponseMessage Post([FromBody] Color value)
        {
            Color color = access.Add(value);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, color);
            //response.Content = new StringContent("Hello", Encoding.Unicode);
            response.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(20)
            };
            return response;
        }

        // PUT: api/Color/5
        public void Put(int id, [FromBody]Color value)
        {
            if (!access.Update(value, id))
                throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        // DELETE: api/Color/5
        public void Delete(int id)
        {
            if (!access.Delete(id))
                throw new HttpResponseException(HttpStatusCode.NotFound);
        }
    }

    public class TextResult : IHttpActionResult
    {
        string value;
        HttpRequestMessage request;

        public TextResult(string value, HttpRequestMessage request)
        {
            this.value = value;
            this.request = request;
        }
        public  Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(value),
                RequestMessage = request
            };
            return Task.FromResult(response);
        }
    }
}
