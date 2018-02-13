using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using Microsoft.AspNetCore.Authorization;
using BPM.Repository.PowerManage;
using BPM.Models;
using BPM1;

namespace Hasng.CadreFile.WebApp.Areas.PowerManage.Controllers
{

    [Area("PowerManage")]
    public class UsersOrganizationController : Controller
    {

        PowerUserOrgRepository _userOrgRepository;
        PowerUserOrganizationRepository _userOrganizationRepository;
        public UsersOrganizationController(PowerUserOrgRepository userOrgRepository, PowerUserOrganizationRepository userOrganizationRepository)
        {
            _userOrgRepository = userOrgRepository;
            _userOrganizationRepository = userOrganizationRepository;
        }


        /// <summary>
        /// 主页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 新增、编辑
        /// </summary>
        /// <returns></returns>
        public IActionResult AddUsersOrgView()
        {
            return View();
        }

        /// <summary>
        /// 组织机构关联
        /// </summary>
        /// <returns></returns>
        public IActionResult Mapping()
        {
            return View();
        }

        /// <summary>
        /// 新增或编辑
        /// </summary>
        /// <param name="model"></param>
        public void AddOrUpdate(Power_UserOrganization data)
        {
            if (data.ID == Guid.Empty)
            {
                try
                {
                    data.ID = Guid.NewGuid();
                    data.U_IsSystem = false;
                    data.U_IsValid = true;
                    data.U_AreaCode = ManageProvider.AreaCode;

                    _userOrganizationRepository.Add(data);
                    //data = _uorbll.Insert(data);
                    //记录日志
                }
                catch (Exception ex)
                {
                    //记录日志
                }
            }
            else
            {
                Power_UserOrganization model = _userOrganizationRepository.GetById(data.ID);
                //data = _uorbll.GetViewModel(model.ID.ToString());
                //Power_UserOrganizationView oldData = ObjectClone.ToClone<Power_UserOrganizationView>(data); ;

                try
                {
                    model.Name = data.Name;
                    model.ParentID = data.ParentID;
                    model.IsEnable = data.IsEnable;
                    model.Remarks = data.Remarks;

                    //data = _uorbll.Update(data);
                    _userOrganizationRepository.Edit(model);
                    //记录日志
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// 获取对象，AreaCode
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //public string GetEntity(string id)
        //{
        //    Power_UserOrganizationView model = _uorbll.GetViewModel(id);
        //    return JsonConvert.SerializeObject(model);
        //}

        /// <summary>
        /// 获取表记录模型  AreaId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetModel(string id)
        {
            //Power_UserOrganizationView entity = _uorbll.GetViewModel(id);

            //Power_AreaView viewmodel = _areabll.GetAreaViewByCode(entity.U_AreaCode);
            //if (viewmodel != null && viewmodel.ID != Guid.Empty)
            //    entity.U_AreaCode = viewmodel.ID.ToString();
            //return entity.ToJson();



            Power_UserOrganization entity = _userOrganizationRepository.GetById(Guid.Parse(id));
            return JsonConvert.SerializeObject(entity);

        }

        /// <summary>
        /// 列表分页
        /// </summary>
        /// <param name="ParameterJson"></param>
        /// <param name="jqgridparam"></param>
        /// <returns></returns>
        //public string GetPagerJson(string ParameterJson, JqGridParam jqgridparam)
        //{
        //    if (string.IsNullOrEmpty(jqgridparam.sortField))
        //        jqgridparam.sortField = "U_SortNo";
        //    string strJson = _uorbll.FindListJsonPage(ParameterJson, ref jqgridparam);
        //    return JsonManager.ToPageJson(jqgridparam, strJson);
        //}

        /// <summary>
        /// 获取json  所有数据
        /// </summary>
        /// <returns></returns>
        public string GetJson()
        {
            //string sCondition = JqConditionParam.ToJson();
            //string strSort = new JqSortParam().ToJson();
            //string strJson = _uorbll.GetListJson(sCondition, strSort);
            //return strJson;

            IEnumerable<Power_UserOrganization> list = _userOrganizationRepository.List();
            return JsonConvert.SerializeObject(list);

        }

        /// <summary>
        /// 通用获取组织机构 树形结构
        /// </summary>
        /// <returns></returns>
        //public string GetOrganizationTreeJsons()
        //{
        //    string userOrgJson = _uorbll.GetOrganizationTreeJson();
        //    return userOrgJson;
        //}

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                Power_UserOrganization model = _userOrganizationRepository.GetById(Guid.Parse(id));
                //Power_UserOrganizationView model = _uorbll.GetViewModel(id);
                try
                {
                    _userOrganizationRepository.Delete(model);
                    //_uorbll.Delete(Guid.Parse(id));
                    //写入日志
                    //logcache.AddServiceLogForDelete<Power_UserOrganizationView>(model, CurrentMenuID, "删除组织机构", "Power_UserOrganization");
                    return true;
                }
                catch (Exception ex)
                {
                    //logcache.AddServiceLogForDelete<Power_UserOrganizationView>(model, CurrentMenuID, "删除组织机构异常", "Power_UserOrganization", Request.Path, ex.Message, ex.StackTrace);
                }
            }
            return false;
        }

    }
}