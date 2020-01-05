using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication15.Controllers
{
    public class SQLTestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //////////////working Native SQL statement  - SELECT/////////////
        //var Posts = _context.Posts.FromSql("SELECT * FROM Posts").ToList();
        //StringBuilder sb = new StringBuilder();

        //foreach (var Post in Posts)
        //{
        //    sb.Append(Post.Subject + "\n");
        //}
        //return sb.ToString();


        ///////////////////working Native SQL statement - DELETE///////////////
        //int result = _context.Database.ExecuteSqlCommand("DELETE FROM categories WHERE Id = 4");

        //if (result > 0)
        //    return "Deleted";

        //else
        //    return "Not found";


        //working Native SQL statement - Select//
        //DbConnection con = _context.Database.GetDbConnection();
        //con.Open();
        //DbCommand com = con.CreateCommand();
        //com.CommandText = "SELECT Name FROM Categories";
        //com.CommandType = System.Data.CommandType.Text;
        //DbDataReader dr = com.ExecuteReader();
        //StringBuilder data = new StringBuilder();

        //while (dr.Read())
        //{
        //    data.AppendLine(dr.GetString(0));
        //}

        //return data.ToString();

    }
}