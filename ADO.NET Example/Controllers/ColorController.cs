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
using System.Web.Http.Cors;
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

    [EnableCorsAttribute("*", "*", "*")]
    [RoutePrefix("api/Color")]
    public class ColorController : ApiController
    {
        ColorAccess access = new ColorAccess();

        /// <summary>
        /// Booty Rockin Everywhere
        /// </summary>
        [HttpGet]
        [Route("IActionGet")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult IActionGet()
        {
            return new TextResult("Hello", Request);
        }

        [BasicAuthentication]
        [HttpGet]
        [Route("")]
        // GET: api/Color
        public HttpResponseMessage Get(string id = "All")
        {
            string username = Thread.CurrentPrincipal.Identity.Name;
            switch (username.ToLower()) 
            {
                case "admin":
                    var r = access.Get();
                    if (id.ToLower() == "all")
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, r);
                    }
                    else {
                        int target = Convert.ToInt32(id.Substring(1));
                        switch (id.Substring(0, 1))
                        {
                            case ">":
                                return Request.CreateResponse(HttpStatusCode.OK, r.Where(color => color.ColorID >= target));
                            case "<":
                                return Request.CreateResponse(HttpStatusCode.OK, r.Where(color => color.ColorID <= target));
                            default:
                                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"Value for id should be All, <5, or >5. {id} is invalid.");
                        }
                    }
                case "guest":
                    r = access.Get();
                    if (id.ToLower() == "all")
                        return Request.CreateResponse(HttpStatusCode.OK, r);
                    else
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, $"id must be default, or All. api/Color?{id} is an unauthorized privilege.");
                default:
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // GET: api/Color/5
        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public HttpResponseMessage Get(int id)
        {
            Color color;
            if (!access.TryGet(id, out color))
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"ID: {id} Not Found"); //throw new HttpResponseException(HttpStatusCode.NotFound);
            return Request.CreateResponse(HttpStatusCode.OK, color);
        }

        // POST: api/Color
        [HttpPost]
        [Route("")]
        public HttpResponseMessage Post([FromBody] Color value)
        {
            try
            {
                Color color = access.Add(value);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, color);
                //response.Content = new StringContent("Hello", Encoding.Unicode);
                response.Headers.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue()
                {
                    MaxAge = TimeSpan.FromMinutes(20)
                };
                response.Headers.Location = new Uri(Request.RequestUri + "/" + color.ColorID.ToString());
                return response;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // PUT: api/Color/5
        [Route("{id}")]
        public HttpResponseMessage Put(int id, [FromBody]Color value)
        {
            if (!access.Update(value, id))
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"{id} Not Found");
            Color color = new Color(id) { nm = value.nm, hex = value.hex };
            return Request.CreateResponse(HttpStatusCode.Created, color);
        }

        // DELETE: api/Color/5
        [Route("{id}")]
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                if (!access.Delete(id))
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"{id} Not Found");
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
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
