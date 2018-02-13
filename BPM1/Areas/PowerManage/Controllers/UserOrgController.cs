
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;

using System.Linq;
using Microsoft.AspNetCore.Authorization;
using BPM1;
using BPM.Repository.PowerManage;
using BPM.Repository;
using BPM.Models;

namespace Hasng.CadreFile.WebApp.Areas.PowerManage.Controllers
{
    [Area("PowerManage")]
    public class UserOrgController : Controller
    {
        DataContext _db;
        PowerUserOrgRepository _userOrgRepository;
        PowerUserOrganizationRepository _userOrganizationRepository;
        public UserOrgController(DataContext db,PowerUserOrgRepository userOrgRepository, PowerUserOrganizationRepository userOrganizationRepository)
        {
            _db = db;
            _userOrgRepository = userOrgRepository;
            _userOrganizationRepository = userOrganizationRepository;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddUsers()
        {
            return View();
        }

        /// <summary>
        /// 列表分页
        /// </summary>
        /// <param name="ParameterJson"></param>
        /// <param name="jqgridparam"></param>
        /// <returns></returns>
        public string GetPagerJson(string UserOrg, JqGridParam jqgridparam)
        {
            //jqgridparam.sortField = "Pxh";
            //string sCondition = JqConditionParam.CombinationCon(ParameterJson, JqConditionParam.ToJson());
            //JsonConvert.DeserializeObject<>(ParameterJson);
            var list = (from a in _db.Power_User
                       join c in _db.Power_UserOrg on a.ID equals c.UserID
                       where c.OrgID == Guid.Parse(UserOrg)
                       select a).Skip(jqgridparam.pageSize * (jqgridparam.pageIndex - 1))
                            .Take(jqgridparam.pageSize).AsEnumerable();

            

            //string strJson = _userbll.GetListJsonByOrg(sCondition, ref jqgridparam);
            return JsonManager.ToPageJson(jqgridparam, JsonConvert.SerializeObject(list));
        }

        /// <summary>
        /// 新增机构用户关系
        /// </summary>
        /// <param name="Userlist"></param>
        /// <param name="OrgID"></param>
        [HttpPost]
        public void InsertUserOrg(List<Power_User> Userlist, Guid OrgID)
        {
            foreach (var item in Userlist)
            {
                Power_UserOrg info = new Power_UserOrg();
                info.ID = Guid.NewGuid();
                info.OrgID = OrgID;
                info.UserID = item.ID;
                _userOrgRepository.Add(info);
            }
        }

        //    /// <summary>
        //    /// 删除
        //    /// </summary>
        //    /// <param name="id"></param>
        //    /// <returns></returns>
        //    public bool Del(string id)
        //    {
        //        Power_UserOrgView userOrgView = new Power_UserOrgView();
        //        try
        //        {
        //            if (!string.IsNullOrEmpty(id))
        //            {
        //                userOrgView = _userOrgBll.GetViewModel(id);
        //                _userOrgBll.Delete(Guid.Parse(id));
        //                logCache.AddServiceLogForDelete(userOrgView, CurrentMenuID, "用户机构组删除", "Power_UserOrg");
        //                return true;
        //            }

        //        }
        //        catch (Exception ex)
        //        {
        //            logCache.AddServiceLogForDelete(userOrgView, CurrentMenuID, "用户机构组删除异常", "Power_UserOrg", HttpContext.Request.Path, ex.Message, ex.StackTrace);
        //        }

        //        return false;
        //    }
    }
}
