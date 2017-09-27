using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JayZeeTool.Models;
using Newtonsoft.Json;
using System.Text;

namespace JayZeeTool.Controllers
{
    public class APIDebugController : Controller
    {
        // GET: APIDebug
        public ActionResult Index()
        {
            PostModel p = new PostModel();
            p.requestURL = "http://localhost/TestAPI/api/values/GetMyTest";
            Dictionary<string, Object> requireParams = new Dictionary<string, object>();
            requireParams.Add("Customer_Id", "5296");//客户id
            requireParams.Add("Loan_Guid", "76882");//借款id
            string postData = JsonConvert.SerializeObject(requireParams);
            p.postJson = postData;
            return View(p);
        }
        [HttpPost]
        public ActionResult Index(PostModel model)
        {
            if (string.IsNullOrEmpty(model.requestURL))
            {
                model.responseContent = "url不能为空";
            }
            if (string.IsNullOrEmpty(model.postJson))
            {
                model.responseContent = "json数据不能为空";
            }
            string response = CommonHelpr.PostDataToUrl(Encoding.UTF8.GetBytes(model.postJson), model.requestURL);
            model.responseContent = response;
            return View(model);
        }

    }
}