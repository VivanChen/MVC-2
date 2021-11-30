using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using MVC_2.Models;//引用Models
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MVC_2.Controllers
{
    public class HomeController : Controller //Home
    {
        IList<Employee> employees = new List<Employee>();
        public IActionResult Index() 
        {
            employees.Add(new Employee { Id = "A01", Name = "Vivan", Salary = 32000 });
            employees.Add(new Employee { Id = "A02", Name = "Sally", Salary = 50000 });
            employees.Add(new Employee { Id = "A03", Name = "Jerome", Salary = 62500 });
            employees.Add(new Employee { Id = "E04", Name = "Jacle", Salary = 95000 });
            employees.Add(new Employee { Id = "E05", Name = "Jerry", Salary = 150000 });
            ViewBag.connectionString = ReadAppSetting();
            ViewData["Test"] = "我是ViewData";
            TempData["Yest2"] = 3;
            if ((int)TempData["Yest2"] == 3)
            {
                return RedirectToAction("About");
            }
            TempData.Keep();

            return View(employees);
        }
        public IActionResult About()
        {
            return View();
        }
        /// <summary>
        /// 讀取AppSetting
        /// </summary>
        /// <returns></returns>
        public static string[]  ReadAppSetting()
        {
            var buidler = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var config = buidler.Build();
            var connectionString = $"AppId = {config["Player:AppId"]}";
            string[] conny = new string[] { $"AppId = {config["Player:AppId"]}", $"Key = {config["Player:Key"]}", $"Connection String = {config["ConnectionStrings:TestConnectionStrings"]}" };
            return conny;
        }

        public static bool apiTest()
        {
            bool ca = false;
            List<Task<string>> tasks = new List<Task<string>>();
            UsersClient usersClient = new UsersClient();
            tasks.Add(usersClient.BatchPostAsync("api的網址", "組好的JSON物件"));
            //等待分批送出全部完成
            Task taskResult = Task.WhenAll(tasks);

            taskResult.Wait();

            ApiResult apiResults = new ApiResult();

            foreach (Task<string> tData in tasks)
            {
                apiResults = (JsonConvert.DeserializeObject<ApiResult>(tData.Result));
            }
            InsuranceCalcuResult insuranceCalcuResult = JsonConvert.DeserializeObject<InsuranceCalcuResult>(apiResults.Content.ToString());

            return ca;
        }
        /// <summary>
        /// 回傳物件範例
        /// </summary>
        public class ApiResult
        {
            public Object Content { get; set; }
            public String Identity { get; set; }
            public String ReturnCode { get; set; }

            public String ReturnMsg { get; set; }

        }
        /// <summary>
        /// 回傳物件範例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class InsuranceCalcuResult
        {            
            public string SumPremiumHGDiscountPlusPeriod { get; set; }
            public List<IdentityCalcuData> IdentityPremium { get; set; }
        }
        /// <summary>
        /// 回傳物件範例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class IdentityCalcuData
        {
            /// <summary>
            /// 回傳物件範例
            /// </summary>
            public string Identity { get; set; }
            public List<ProductPremium> ProductPremium { get; set; }
        }
        /// <summary>
        /// 回傳物件範例
        /// </summary>
        public class ProductPremium
        {
            public string plusMonthly { get; set; }
        }

    }
}
