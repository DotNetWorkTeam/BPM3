using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Xml;
using BPM.Models;
using BPM.Repository;
using BPM.Repository.SystemManage;

namespace BPM1.Areas.SystemManage.Controllers
{
    [Area("SystemManage")]
    public class AreaController : Controller
    {
        AreaReponsitory _areaReponsitory;
        DataContext _db;
        public AreaController(DataContext db, AreaReponsitory areaReponsitory)
        {
            _db = db;
            _areaReponsitory = areaReponsitory;
        }


        /// <summary>
        /// 主界面
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 区域管理主数据
        /// </summary>
        /// <returns></returns>
        public string GetAllAreas(string ParameterJson, JqSortParam jqsortparam)
        {
            //string f = JsonConvert.SerializeObject(jqsortparam);
            var list = _db.Power_Area.ToList();
            return JsonConvert.SerializeObject(list);
        }


        /// <summary>
        /// 获取数据表有效的json数据
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public string GetAllAreasJsons()
        {
            //string areaJson = _areabll.GetListJson();
            //List<Power_AreaView> list = JsonConvert.DeserializeObject<List<Power_AreaView>>(areaJson);

            //var areaList = list.Where(p => p.IsEnable != false).OrderBy(d => d.U_SortNo).ToList();
            //return areaList.ToJson();

            IEnumerable<Power_Area> areaList = _areaReponsitory.List(x => x.IsEnable == true);
            return JsonConvert.SerializeObject(areaList);
            //return areaJson;
        }

        /// <summary>
        /// 根据id获取ViewModel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetModel(string id)
        {
            Power_Area entity  = _areaReponsitory.GetById(Guid.Parse(id));
            return JsonConvert.SerializeObject(entity);
        }

        ///// <summary>
        ///// 带分页数据列表
        ///// </summary>
        ///// <param name="ParameterJson"></param>
        ///// <param name="jqgridparam"></param>
        ///// <returns></returns>
        //public String GetPagerJons(string ParameterJson, JqGridParam jqgridparam)
        //{
        //    string sAreaJson = _areabll.FindListJsonPage(ParameterJson, ref jqgridparam);
        //    return JsonManager.ToPageJson(jqgridparam, sAreaJson);
        //}






        ///// <summary>
        ///// 初始化区域默认值
        ///// <param name="AreaCode">区域code</param>
        ///// <param name="AreaCodeName">区域名字</param>
        ///// </summary>
        ///// <returns></returns>
        //public int AreaInit(string AreaCode, string AreaCodeName)
        //{
        //    return _areabll.AreaInit(AreaCode, AreaCodeName);
        //}

        ///// <summary>
        ///// 超级管理员区域初始化默认值
        ///// <param name="AreaCode">区域code</param>
        ///// <param name="AreaCodeName">区域名字</param>
        ///// </summary>
        ///// <returns></returns>
        //public int AreaSystemInit(string AreaCode, string AreaCodeName)
        //{
        //    return _areabll.AreaSystemInit(AreaCode, AreaCodeName);
        //}


        //#region 读取XML文件内容
        //public static string GetNodeInnerText(XmlNode node, string sName, string sDefaultValue = "")
        //{
        //    try
        //    {
        //        XmlAttribute attr = node.Attributes[sName];

        //        if (null != attr)
        //        {
        //            return attr.Value;
        //        }
        //        else
        //            return sDefaultValue;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        //#endregion


        ///// <summary>
        ///// 启用、禁用 区域
        ///// </summary>
        ///// <param name="ID"></param>
        ///// <param name="modifyType"></param>
        ///// <param name="areaCode"></param>
        ///// isOpen  是否开启三元
        ///// <returns></returns>
        //public Boolean ModifyIsEnable(String ID, int modifyType, string areaCode, int isOpen)
        //{
        //    Boolean enableFlag = false;

        //    //创建三元与管理员 账号
        //    ThreeMemberSettings model = XmlHelperRoleUser.GetTableInfo();
        //    //启用区域
        //    if (modifyType == 1)
        //    {
        //        if (_areabll.ModifyIsEnable(ID, true) > 0)
        //        {
        //            _appinitbll.OpenArea(areaCode);
        //            if (isOpen == 1)
        //                _areabll.OpenSanYuan(areaCode);
        //            else
        //                _areabll.CloseSanYuan(areaCode);
        //        }
        //    }
        //    //禁用区域
        //    else
        //    {
        //        if (_areabll.ModifyIsEnable(ID, false) > 0) { enableFlag = true; }
        //    }
        //    return enableFlag;
        //}

        ///// <summary>
        ///// 关闭三员管理
        ///// </summary>
        ///// <param name="areaCode"></param>
        //private void CloseSanYuan(string areaCode)
        //{
        //    _areabll.CloseSanYuan(areaCode);
        //}

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Del(Guid id)
        {
            Power_Area power_AreaView = new Power_Area();
            try
            {
                if (id != null)
                {

                    power_AreaView = _areaReponsitory.GetById(id);
                    _areaReponsitory.Delete(power_AreaView);
                    return true;
                }

            }
            catch (Exception ex)
            {

            }
            return false;


            //try
            //{
            //    if (!string.IsNullOrEmpty(id))
            //    {
            //        power_AreaView = _areaReponsitory.GetById(id);
            //        _areaReponsitory.Delete(Guid.Parse(id));
            //        return true;
            //    }
            //    logCache.AddServiceLogForDelete(power_AreaView, CurrentMenuID, "管档单位维护删除", "Power_Area");
            //}
            //catch (Exception ex)
            //{
            //    logCache.AddServiceLogForDelete(power_AreaView, CurrentMenuID, "管档单位维护删除", "Power_Area", HttpContext.Request.Path, ex.Message, ex.StackTrace);
            //}

            //return false;
        }


        /// <summary>
        /// 验证输入的值是否唯一
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="filed">字段名</param>
        /// <param name="val">字段值</param>
        /// <returns></returns>
        public bool FindOnly(string code)
        {
            int num = _db.Power_Area.Where(x => x.Code == code).Count();
            if (num > 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 编辑时验证
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool FindOnlyEdit(string Code, string ID)
        {
            #region 条件组合
            //List<ParameterJson> list = new List<ParameterJson>();

            //list.Add(new ParameterJson("Code", ConditionOperate.Equal.ToString(), Code));
            //list.Add(new ParameterJson("ID", ConditionOperate.NotEqual.ToString(), ID));
            //list.Add(new ParameterJson("U_IsValid", ConditionOperate.Equal.ToString(), "1"));
            //string sCondition = JsonConvert.SerializeObject(list);
            #endregion
            int num = _db.Power_Area.Where(x => x.Code == Code && x.ID != Guid.Parse(ID)).Count();
            return num == 0;
        }




        public IActionResult AddView()
        {
            return View();
        }



        /// <summary>
        /// 新增或编辑数据
        /// </summary>
        /// <param name="model"></param>
        public void InsertOrUpdate(Power_Area model)
        {
            Guid Id = model.ID;
            if (Id == Guid.Empty)
            {
                model.IsEnable = true;
                model.IsOpenTrilateral = false;
                model.U_CreateDate = DateTime.Now;
                model.ID = Guid.NewGuid();
                model.U_IsValid = true;
                model.IsEnable = true;
                _areaReponsitory.Add(model);

            }
            else//
            {
                Power_Area model1 = _areaReponsitory.GetById(model.ID);
                model1.Code = model.Code;
                model1.IsEnable = model.IsEnable;
                model1.Level = model.Level;
                model1.ParentID = model.ParentID;
                model1.Title = model.Title;
                model1.U_IsSystem = model.U_IsSystem;
                model1.U_IsValid = model.U_IsValid;
                _areaReponsitory.Edit(model1);
                
            }

        }
    }


}