using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TimeSheetWeb.Models;
using System.Security.Cryptography;
using System.Web.Configuration;
using Newtonsoft.Json;
using WebGrease.Css.Ast.Selectors;


namespace TimeSheetWeb.Controllers
{
    public class LoginData
    {
        public string user;
        public string password;

        public LoginData()
        {
            user = "";
            password = "";
        }
    }
    public class ClientData
    {
        //private TimeSheetWebContext db = new TimeSheetWebContext();

        public List<Client_DTO> Clients;
        public List<BusinessLine> BusinessLines;
        public List<BusinessFocus> BusinessFocuces;
        public CalendarData Calendar;
        public List<EmpProjectDaily> DailyProjects;
        public List<DateTime> WeekEndProjectDates;

        public ClientData()
        {
            WeekEndProjectDates = new List<DateTime>();
        }
        public EmpHR_DTO EmpHR;
    }
    public class CalendarData
    {
        //private TimeSheetWebContext db = new TimeSheetWebContext();

        public List<Calendar> Data;
        public EmpCalendar details;
    }
    public class ClientsController : ApiController
    {
        //System.Configuration.Configuration rootWebConfig =
        //        System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(null);
        private TimeSheetWebContext db = new TimeSheetWebContext();


        //private static string domain = "TAKE5";

        public bool LogonValid(string userName, string password)
        {
            string domain = WebConfigurationManager.AppSettings["domainname"];
            //if (WebConfigurationManager.AppSettings["domainname"] != 0)
            //{
            //    System.Configuration.KeyValueConfigurationElement domainName =
            //        rootWebConfig.AppSettings.Settings["domainname"];
            //    if (domainName != null)
            //        domain = domainName.ToString();
            //    else
            //        domain = "TAKE5";
            //}
            DirectoryEntry de = new DirectoryEntry(null, domain +
              "\\" + userName, password);
            try
            {
                object o = de.NativeObject;
                DirectorySearcher ds = new DirectorySearcher(de);
                ds.Filter = "samaccountname=" + userName;
                ds.PropertiesToLoad.Add("cn");
                SearchResult sr = ds.FindOne();
                if (sr == null) throw new Exception();
                return true;
            }
            catch
            {
                return false;
            }
        }
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash. 
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes 
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data  
            // and format each one as a hexadecimal string. 
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string. 
            return sBuilder.ToString();
        }

        [HttpPost]
        [ActionName("update")]
        public async Task<IHttpActionResult> UpdateUser(string user, string password)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                string hash = GetMd5Hash(md5Hash, password);
                User theUser = await db.Users.Where(b => b.usercode == user).FirstOrDefaultAsync();
                if (theUser == null)
                {
                    return NotFound();
                }
                if (WebConfigurationManager.AppSettings["authentication"].ToLower().CompareTo("domain") != 0)
                {
                    if (theUser.userpassword.CompareTo(hash) != 0)
                    {
                        return StatusCode(HttpStatusCode.Unauthorized);
                    }

                }
                else
                {
                    if (!LogonValid(user, password))
                    {
                        if (!LogonValid(user, password))
                        {
                            byte[] data = Convert.FromBase64String(password);
                            string decodedString = Encoding.UTF8.GetString(data);
                            if (!LogonValid(user, decodedString))
                            {
                                return StatusCode(HttpStatusCode.Unauthorized);
                            }
                        }
                    }
                }
            }
            string jsonContent = Request.Content.ReadAsStringAsync().Result;
            List<ProjectTran> theProjects = JsonConvert.DeserializeObject<List<ProjectTran>>(jsonContent);
            //ProjectTran theProjects = JsonConvert.DeserializeObject<ProjectTran>(jsonContent);

            foreach (var project in theProjects)
            {
                if (project.moduser == 0)
                {
                    project.moduser = project.empid;
                }
                if (project.createuser == 0)
                {
                    project.createuser = project.empid;
                }
                db.ProjectTrans.Add(project);
            }

            await db.SaveChangesAsync();
            return Ok(theProjects);
        }

        [HttpPost]
        [ActionName("login")]
        public async Task<IHttpActionResult> Login()
        {
            string jsonContent = Request.Content.ReadAsStringAsync().Result;
            LoginData loginData = JsonConvert.DeserializeObject<LoginData>(jsonContent);
            
            IHttpActionResult theResult = await LoginProcedure(loginData.user, loginData.password);
                
            return theResult;
        }


        public async Task<IHttpActionResult> LoginProcedure(string user, string password)
        {
            PropertyInfo[] properties1;
            PropertyInfo[] properties2;
            using (MD5 md5Hash = MD5.Create())
            {
                string hash = GetMd5Hash(md5Hash, password);
                User theUser = await db.Users.Where(b => b.usercode == user).FirstOrDefaultAsync();
                if (theUser == null)
                {
                    return NotFound();
                }
                if (WebConfigurationManager.AppSettings["authentication"].ToLower().CompareTo("domain") != 0)
                {
                    if (theUser.userpassword.CompareTo(hash) != 0)
                    {
                        return StatusCode(HttpStatusCode.Unauthorized);
                    }

                }
                else
                {
                    if (!LogonValid(user, password))
                    {
                        byte[] data = Convert.FromBase64String(password);
                        string decodedString = Encoding.UTF8.GetString(data);
                        if (!LogonValid(user, decodedString))
                        {
                            return StatusCode(HttpStatusCode.Unauthorized);    
                        }
                        
                    }
                }


                List<UserClient> theUserClients = await db.UserClients.Where(i => i.empid == theUser.empid).ToListAsync();
                if (theUserClients == null)
                {
                    return NotFound();
                }


                int j = 0;
                string[] sClients = new string[theUserClients.Count];
                foreach (var theClient in theUserClients)
                {
                    sClients[j] = theClient.seg1code;
                    j++;
                }
                List<Client> theClients = await db.Clients.Where(e => sClients.Contains(e.seg1code)).ToListAsync();
                if (theClients == null)
                    return NotFound();

                List<Client_DTO> theClientDatas = new List<Client_DTO>();
                foreach (var theClient in theClients)
                {
                    List<ClientProduct> theClientProducts = await db.ClientProducts.Where(
                    e => (e.seg1code == theClient.seg1code)
                        ).ToListAsync();
                    string[] sProducts = new string[theClientProducts.Count];
                    j = 0;
                    foreach (var theProduct in theClientProducts)
                    {
                        sProducts[j] = theProduct.seg2code;
                        j++;
                    }
                    List<Product> theProducts = await db.Products.Where(e => sProducts.Contains(e.seg2code)).ToListAsync();


                    List<ClientTask> theClientTasks = await db.ClientTasks.Where(
                    e => (e.seg1code == theClient.seg1code)
                        ).ToListAsync();
                    string[] sTasks = new string[theClientTasks.Count];
                    j = 0;
                    foreach (var theTask in theClientTasks)
                    {
                        sTasks[j] = theTask.seg3code;
                        j++;
                    }
                    List<Duty> theTasks = await db.Duties.Where(e => sTasks.Contains(e.seg3code)).ToListAsync();

                    Client_DTO theData = new Client_DTO();

                    properties1 = typeof(Client_DTO).GetProperties();
                    properties2 = typeof(Client).GetProperties();


                    foreach (PropertyInfo property1 in properties1)
                    {
                        PropertyInfo theProperty = Array.Find(properties2, p => p.Name.CompareTo(property1.Name) == 0);
                        var value = theProperty.GetValue(theClient);
                        property1.SetValue(theData, value);
                    }

                    theData.theTasks = theTasks;
                    theData.theProducts = theProducts;




                    theClientDatas.Add(theData);
                }

                List<BusinessFocus> theBusinessFocuses = await db.BusinessFocuces.ToListAsync();
                List<BusinessLine> theBusinessLines = await db.BusinessLines.ToListAsync();

                CalendarData theCalendarData = new CalendarData();
                EmpHR theEmpHR = await db.EmpHRs.Where(e => e.empid == theUser.empid).FirstOrDefaultAsync();
                if (theEmpHR != null)
                {
                    EmpCalendar theCalendar = await db.EmpCalendars.Where(e => e.calendarcode == theEmpHR.calendarcode).FirstOrDefaultAsync();
                    if (theCalendar != null)
                    {
                        theCalendarData.Data =
                            await db.CalendarData.Where(e => e.calendarcode == theCalendar.calendarcode).ToListAsync();
                        theCalendarData.details = theCalendar;

                    }
                }
                List<EmpProjectDaily> DailyProjects = await db.EmpDayProjects.Where(e => e.empid == theUser.empid).ToListAsync();
                ClientData theAllData = new ClientData();
                theAllData.Clients = theClientDatas;
                theAllData.BusinessFocuces = theBusinessFocuses;
                theAllData.BusinessLines = theBusinessLines;
                theAllData.Calendar = theCalendarData;
                theAllData.DailyProjects = DailyProjects;




                properties1 = typeof(EmpHR_DTO).GetProperties();
                properties2 = typeof(EmpHR).GetProperties();
                EmpHR_DTO theEmphrDto = new EmpHR_DTO();

                foreach (PropertyInfo property1 in properties1)
                {
                    PropertyInfo theProperty = Array.Find(properties2, p => p.Name.CompareTo(property1.Name) == 0);
                    var value = theProperty.GetValue(theEmpHR);
                    if (value == null)
                    {
                        if (theProperty.Name == "contractenddate")
                        {
                            value = new DateTime(9999, 1, 1);
                        }
                        if (theProperty.Name == "contractstartdate")
                        {
                            value = new DateTime(9999, 1, 1);
                        }
                    }
                    property1.SetValue(theEmphrDto, value);
                }

                theAllData.EmpHR = theEmphrDto;
                DateTime dtNow = DateTime.Now;
                DateTime dt1 = dtNow.AddDays(-365);
                DateTime dt2 = dtNow.AddDays(365);
                List<ProjectTran> tmpProjectTrans = await db.ProjectTrans.Where(e => (e.createdate >= dt1 &
                      e.createdate < dt2
                      & e.empid == theUser.empid
                      )
                      ).ToListAsync();
                List<ProjectTran> theProjectTrans = tmpProjectTrans;//.Where(e =>
                //    (e.createdate.DayOfWeek == DayOfWeek.Saturday || e.createdate.DayOfWeek == DayOfWeek.Sunday)
                //    ).ToList();
                foreach (var project in theProjectTrans)
                {
                    theAllData.WeekEndProjectDates.Add(project.createdate);
                }

                return Ok(theAllData);
            }
            //return StatusCode(HttpStatusCode.BadRequest);
            
        }

        [HttpPost]
        [ActionName("login")]
        public async Task<IHttpActionResult> Login(string user, string password)
        {
            IHttpActionResult theResult = await LoginProcedure(user, password);

            return theResult;
        }
        // GET api/Clients
        public IQueryable<Client> GetClients()
        {
            return db.Clients;
        }

        // GET api/Clients/5
        [ResponseType(typeof(Client))]
        public async Task<IHttpActionResult> GetClient(string id)
        {
            Client theClient = null;// await db.Clients.Include(b => b.ClientDuties).Include(b => b.ClientProducts).SingleOrDefaultAsync(b => b.ClientCode == id);
            if (theClient == null)
            {
                return NotFound();
            }

            //Client client = await db.Clients.FindAsync(id);
            //if (client == null)
            //{
            //    return NotFound();
            //}

            return Ok(theClient);
        }

        // PUT api/Clients/5
        public async Task<IHttpActionResult> PutClient(string id, Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.seg1code)
            {
                return BadRequest();
            }

            db.Entry(client).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Clients
        //[ResponseType(typeof(Client))]
        //public async Task<IHttpActionResult> PostClient(Client client)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Clients.Add(client);

        //    try
        //    {
        //        await db.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (ClientExists(client.seg1code))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtRoute("DefaultApi", new { id = client.seg1code }, client);
        //}

        // DELETE api/Clients/5
        [ResponseType(typeof(Client))]
        public async Task<IHttpActionResult> DeleteClient(string id)
        {
            Client client = await db.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            db.Clients.Remove(client);
            await db.SaveChangesAsync();

            return Ok(client);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientExists(string id)
        {
            return db.Clients.Count(e => e.seg1code == id) > 0;
        }
    }
}